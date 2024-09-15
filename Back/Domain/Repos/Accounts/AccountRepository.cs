using Cobo.Application.Dtos.Account;
using Cobo.Domain.Interfaces;
using Cobo.Infraestructure.Models;
using Dapper;
using FluentResults;
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
}

