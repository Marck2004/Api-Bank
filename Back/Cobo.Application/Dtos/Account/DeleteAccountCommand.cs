namespace Cobo.Application.Dtos.Account;
public record struct DeleteAccountCommand(Guid UserId, Guid AccountId);
