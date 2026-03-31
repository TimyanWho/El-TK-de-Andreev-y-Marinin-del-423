using System;

namespace RailwayFareCalculator;

public static class FareCalculator
{
    public const decimal BaseRatePerKilometer = 8m;

    public static FareCalculationResult Calculate(int distanceKm, int ticketCount, ComfortClass comfortClass)
    {
        if (distanceKm <= 0)
            throw new ArgumentOutOfRangeException(nameof(distanceKm), "Расстояние должно быть больше 0.");
        if (ticketCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(ticketCount), "Количество билетов должно быть больше 0.");

        decimal multiplier = comfortClass switch
        {
            ComfortClass.Platskart => 1.0m,
            ComfortClass.Coupe => 1.1m,
            ComfortClass.Polulyuks => 1.2m,
            ComfortClass.Lyuks => 1.3m,
            _ => throw new ArgumentOutOfRangeException(nameof(comfortClass))
        };

        decimal baseAmount = distanceKm * BaseRatePerKilometer * ticketCount;
        decimal totalAmount = Math.Round(baseAmount * multiplier, 2, MidpointRounding.AwayFromZero);

        return new FareCalculationResult(
            DistanceKm: distanceKm,
            TicketCount: ticketCount,
            ComfortClass: comfortClass,
            ComfortMultiplier: multiplier,
            BaseAmount: baseAmount,
            TotalAmount: totalAmount);
    }
}
