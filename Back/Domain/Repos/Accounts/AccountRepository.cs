using Cobo.Application.Dtos.Account;
using Cobo.Domain.Interfaces;
using Cobo.Infraestructure.Models;
using Dapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using System.Data.SqlClient;

namespace Cobo.Domain.Repos.Accounts;
public class AccountRepository : IAccountInterface
{
    private readonly BancoContext _context;
    private readonly Logger _logger;

    private readonly string connectionString;
    public AccountRepository(BancoContext context, IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _context = context;
    }

    public async Task<IEnumerable<AccountQueriesDto>> GetAccountsByUserId(Guid UserId)
    {
        SqlConnection connection = new(connectionString);
        try
        {
            connection.Open();

            const string sql = $"""
                SELECT *
                FROM dbo.Account AS account
                WHERE account.{nameof(AccountQueriesDto.UserId)} = @UserId
                """;

            return await connection.QueryAsync<AccountQueriesDto>(
                sql, new { UserId });
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return [];
        }
    }

    public Result AddAccountByUserId(Guid UserId)
    {
        try
        {
            bool findUser = _context.Users.Select(c => c.Id == UserId).FirstOrDefault();
            if (!findUser)
                return Result.Fail("El usuario no existe en la base de datos");

            Account newAccount = new()
            {
                Id = Guid.NewGuid(),
                NumCuenta = AccountNumberGenerator(),
                Balance = 0.0,
                UserId = UserId,
                FechaCreacion = DateTimeOffset.Now
            };

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> DeleteAccountById(DeleteAccountCommand deleteAccountCommand)
    {
        int deletedAccountResult = await _context.Accounts
                                    .Where(c => c.Id == deleteAccountCommand.AccountId &&
                                    c.UserId == deleteAccountCommand.UserId)
                                    .ExecuteDeleteAsync();
        if (deletedAccountResult == 0)
            return Result.Fail("Error al intentar borrar una cuenta bancaria");

        return Result.Ok();
    }

    private string AccountNumberGenerator()
    {
        Random random = new();
        string cuenta = "ES";

        for (int i = 0; i < 22; i++)
        {
            cuenta += random.Next(0, 10).ToString();
        }

        return cuenta;
    }

    public async Task<Account> GetAccountsByAccountNumber(string accountNumber)
    {
        SqlConnection connection = new(connectionString);
        try
        {
            connection.Open();

            const string sql = $"""
                SELECT *
                FROM dbo.Account AS account
                WHERE account.{nameof(AccountQueriesDto.NumCuenta)} = @accountNumber
                """;

            return await connection.QueryFirstAsync<Account>(
                sql, new { accountNumber });
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return new Account();
        }
    }
}

