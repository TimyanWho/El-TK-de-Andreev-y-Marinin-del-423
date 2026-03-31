namespace RailwayFareCalculator;

public sealed record FareCalculationResult(
    int DistanceKm,
    int TicketCount,
    ComfortClass ComfortClass,
    decimal ComfortMultiplier,
    decimal BaseAmount,
    decimal TotalAmount);
