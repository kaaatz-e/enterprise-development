using AirCompany.Application.Contracts.Ticket;
using AirCompany.Generator.Nats.Host.Settings;
using Bogus;
using Microsoft.Extensions.Options;

namespace AirCompany.Generator.Nats.Host.Generator;

/// <summary>
/// Generates random ticket contracts using Bogus
/// </summary>
public class TicketGenerator(IOptions<GeneratorSettings> settings)
{
    private readonly Faker<TicketCreateUpdateDto> _faker = CreateFaker(settings.Value);

    /// <summary>
    /// Creates and configures a Faker instance for ticket DTOs
    /// </summary>
    /// <param name="settings">The generator settings containing ID pools</param>
    /// <returns>A configured Faker instance</returns>
    private static Faker<TicketCreateUpdateDto> CreateFaker(GeneratorSettings settings) =>
        new Faker<TicketCreateUpdateDto>()
            .CustomInstantiator(f => new TicketCreateUpdateDto(
                FlightId: f.PickRandom(settings.FlightIds),
                PassengerId: f.PickRandom(settings.PassengerIds),
                SeatNumber: $"{f.Random.Int(1, 40)}{f.PickRandom('A', 'B', 'C', 'D', 'E', 'F')}",
                HasHandLuggage: f.Random.Bool(),
                TotalBaggageWeightKg: f.Random.Bool(0.7f) ? f.Random.Double(0, 30) : null
            ));

    /// <summary>
    /// Generates multiple random tickets
    /// </summary>
    public IEnumerable<TicketCreateUpdateDto> Generate(int count) => _faker.Generate(count);
}