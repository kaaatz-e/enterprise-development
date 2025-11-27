namespace AirCompany.Application.Contracts.Passenger;

public record PassengerDto(Guid Id, string PassportNumber, string FullName, DateOnly? BirthDate);
