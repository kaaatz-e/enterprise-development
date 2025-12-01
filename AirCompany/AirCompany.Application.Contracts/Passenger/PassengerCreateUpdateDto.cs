namespace AirCompany.Application.Contracts.Passenger;

/// <summary>
/// DTO for creating and updating passenger information
/// </summary>
/// <param name="PassportNumber">The passport number of the passenger. Must be unique for each passenger</param>
/// <param name="FullName">The full name of the passenger including first name, last name, and any middle names</param>
/// <param name="BirthDate">The date of birth of the passenger. Null indicates unknown or not provided</param>
public record PassengerCreateUpdateDto(string PassportNumber, string FullName, DateOnly? BirthDate);