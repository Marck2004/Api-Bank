namespace Cobo.Application.Dtos.Account;
public record struct AccountQueriesDto(Guid Id, string NumCuenta, double Balance, Guid UserId, DateTimeOffset FechaCreacion);

