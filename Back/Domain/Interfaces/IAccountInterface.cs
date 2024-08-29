using Cobo.Application.Dtos.Account;

namespace Cobo.Domain.Interfaces;
public interface IAccountInterface
{
    Task<IEnumerable<AccountQueriesDto>> GetAccountsByUserId(Guid UserId);
}
