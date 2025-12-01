namespace AirCompany.Application.Contracts.Passenger;

/// <summary>
/// DTO for reading passenger information
/// </summary>
/// <param name="Id">The unique identifier of the passenger </param>
/// <param name="PassportNumber">The passport number of the passenger</param>
/// <param name="FullName">The full name of the passenger</param>
/// <param name="BirthDate">The date of birth of the passenger. Null indicates unknown or not provided</param>
public record PassengerDto(Guid Id, string PassportNumber, string FullName, DateOnly? BirthDate);
