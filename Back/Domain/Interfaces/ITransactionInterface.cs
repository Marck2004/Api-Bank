using Cobo.Application.Dtos.Transaction;
using FluentResults;

namespace Cobo.Domain.Interfaces;
public interface ITransactionInterface
{
    public Task<Result> CreateTransaction(CreateTransactionDto newTransaction);
}
