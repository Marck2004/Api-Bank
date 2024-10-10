using Cobo.Application.Dtos.Account;
using Cobo.Infraestructure.Models;
using FluentResults;

namespace Cobo.Domain.Interfaces;
public interface IAccountInterface
{
    Task<IEnumerable<AccountQueriesDto>> GetAccountsByUserId(Guid UserId);
    Task<Account> GetAccountsByAccountNumber(string accountNumber);
    Result AddAccountByUserId(Guid UserId);
    Task<Result> DeleteAccountById(DeleteAccountCommand deleteAccountCommand);
}
