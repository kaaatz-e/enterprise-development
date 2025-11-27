namespace AirCompany.Application.Contracts.Passenger;
public record PassengerCreateUpdateDto(string PassportNumber, string FullName, DateOnly? BirthDate);