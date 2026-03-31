using System.Globalization;
using System.Windows;

namespace RailwayFareCalculator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(DistanceTextBox.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int distanceKm))
            {
                ShowInputError("Введите корректное целое число в поле 'Расстояние (км)'.");
                return;
            }

            if (!int.TryParse(TicketsTextBox.Text.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int ticketCount))
            {
                ShowInputError("Введите корректное целое число в поле 'Количество билетов'.");
                return;
            }

            var comfortClass = GetSelectedComfortClass();
            var result = FareCalculator.Calculate(distanceKm, ticketCount, comfortClass);

            ResultTextBlock.Text =
                $"Базовая стоимость: {result.BaseAmount:0.00} руб.\n" +
                $"Коэффициент комфорта: x{result.ComfortMultiplier:0.0}\n" +
                $"Итого к оплате: {result.TotalAmount:0.00} руб.";
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            ShowInputError(ex.Message);
        }
    }

    private ComfortClass GetSelectedComfortClass()
    {
        if (CoupeRadioButton.IsChecked == true)
            return ComfortClass.Coupe;
        if (PolulyuksRadioButton.IsChecked == true)
            return ComfortClass.Polulyuks;
        if (LyuksRadioButton.IsChecked == true)
            return ComfortClass.Lyuks;

        return ComfortClass.Platskart;
    }

    private void ShowInputError(string message)
    {
        MessageBox.Show(message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
        ResultTextBlock.Text = "Проверьте введенные данные.";
    }
}
