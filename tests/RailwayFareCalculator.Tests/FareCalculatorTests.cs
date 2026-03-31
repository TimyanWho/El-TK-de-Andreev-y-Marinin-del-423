using System;
using Xunit;

namespace RailwayFareCalculator.Tests;

public class FareCalculatorTests
{
    [Theory]
    [InlineData(100, 1, ComfortClass.Platskart, 800.00)]
    [InlineData(100, 1, ComfortClass.Coupe, 880.00)]
    [InlineData(250, 3, ComfortClass.Polulyuks, 7200.00)]
    [InlineData(80, 2, ComfortClass.Lyuks, 1664.00)]
    public void Calculate_ReturnsExpectedTotal(int distanceKm, int ticketCount, ComfortClass comfortClass, decimal expectedTotal)
    {
        var result = FareCalculator.Calculate(distanceKm, ticketCount, comfortClass);

        Assert.Equal(expectedTotal, result.TotalAmount);
    }

    [Theory]
    [InlineData(1, 1, ComfortClass.Platskart, 8.00)]
    [InlineData(10, 1, ComfortClass.Coupe, 88.00)]
    [InlineData(120, 10, ComfortClass.Polulyuks, 11520.00)]
    [InlineData(75, 20, ComfortClass.Lyuks, 15600)]
    public void Calculate_HandlesLargerValues(int distanceKm, int ticketCount, ComfortClass comfortClass, decimal expectedTotal)
    {
        var result = FareCalculator.Calculate(distanceKm, ticketCount, comfortClass);

        Assert.Equal(expectedTotal, result.TotalAmount);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-10, 1)]
    [InlineData(50, 0)]
    [InlineData(50, -2)]
    public void Calculate_InvalidValues_Throws(int distanceKm, int ticketCount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FareCalculator.Calculate(distanceKm, ticketCount, ComfortClass.Platskart));
    }

    [Fact]
    public void Calculate_Platskart_HasMultiplierOne()
    {
        var result = FareCalculator.Calculate(10, 1, ComfortClass.Platskart);

        Assert.Equal(1.0m, result.ComfortMultiplier);
    }

    [Fact]
    public void Calculate_ReturnsAllDetails()
    {
        var result = FareCalculator.Calculate(15, 4, ComfortClass.Coupe);

        Assert.Equal(15, result.DistanceKm);
        Assert.Equal(4, result.TicketCount);
        Assert.Equal(ComfortClass.Coupe, result.ComfortClass);
        Assert.Equal(1.1m, result.ComfortMultiplier);
        Assert.Equal(480m, result.BaseAmount);
        Assert.Equal(528m, result.TotalAmount);
    }
}
