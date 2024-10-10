namespace Cobo.Application.Dtos.Transaction;
public record struct CreateTransactionDto(string AccountNumber,
    string ExternalAccountNumber, double Ammount);
