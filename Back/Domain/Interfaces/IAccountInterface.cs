using Cobo.Application.Dtos.Account;
using FluentResults;

namespace Cobo.Domain.Interfaces;
public interface IAccountInterface
{
    Task<IEnumerable<AccountQueriesDto>> GetAccountsByUserId(Guid UserId);
    Result AddAccountByUserId(Guid UserId);
}
