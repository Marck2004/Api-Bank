using Cobo.Application.Dtos.Transaction;
using Cobo.Domain.Interfaces;
using Cobo.Infraestructure.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Cobo.Domain.Repos.Transacction;
public partial class TransactionRepository : ITransactionInterface
{
    private readonly BancoContext _context;
    private readonly IAccountInterface _accountInterface;

    public TransactionRepository(IAccountInterface accountInterface, BancoContext context)
    {
        _accountInterface = accountInterface;
        _context = context;
    }

    public async Task<Result> CreateTransaction(CreateTransactionDto newTransaction)
    {
        Result validation = await Validate(newTransaction);
        if (validation.IsFailed) return Result.Fail(validation.Errors);

        Task<Account> originalAccount = _accountInterface.GetAccountsByAccountNumber(newTransaction.AccountNumber);
        Task<Account> externalAccount = _accountInterface.GetAccountsByAccountNumber(newTransaction.ExternalAccountNumber);
        await Task.WhenAll(originalAccount, externalAccount);

        Result updatedOriginalAccountResult = await UpdateAccountAmount(originalAccount.Result, -newTransaction.Ammount);
        if (updatedOriginalAccountResult.IsFailed) return Result.Fail(updatedOriginalAccountResult.Errors);

        Result updatedExternalAccountResult = await UpdateAccountAmount(externalAccount.Result, newTransaction.Ammount);
        if (updatedExternalAccountResult.IsFailed) return Result.Fail(updatedExternalAccountResult.Errors);

        await _context.Transactions.AddAsync(
            new Cobo.Infraestructure.Models.Transactions()
            {
                Id = Guid.NewGuid(),
                Cant = newTransaction.Ammount,
                NumCuentaOrg = newTransaction.AccountNumber,
                NumCuentaDst = newTransaction.ExternalAccountNumber
            });

        await _context.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> UpdateAccountAmount(Account updatedAccount, double ammount)
    {
        await _context.Accounts
            .Where(account => account.Id == updatedAccount.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(
                e => e.Balance,
                e => e.Balance + ammount));

        return Result.Ok();
    }

    private async Task<Result> Validate(CreateTransactionDto validateTransaction)
    {
        if (!AccountRegex().IsMatch(validateTransaction.AccountNumber.Trim()))
            return Result.Fail("Formato inválido para la cuenta de origen");
        if (!AccountRegex().IsMatch(validateTransaction.ExternalAccountNumber.Trim()))
            return Result.Fail("Formato inválido para la cuenta exterma");

        Account originalAccount = await _accountInterface.GetAccountsByAccountNumber(validateTransaction.AccountNumber);

        if (originalAccount.Balance < validateTransaction.Ammount) return Result.Fail("La cuenta de origen no tiene saldo suficiente");

        return Result.Ok();
    }

    [GeneratedRegex(@"^ES\d{2}\s?\d{4}\s?\d{4}\s?\d{2}\s?\d{10}$")]
    private static partial Regex AccountRegex();
}
