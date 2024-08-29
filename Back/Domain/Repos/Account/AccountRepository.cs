using Cobo.Application.Dtos.Account;
using Cobo.Domain.Interfaces;
using Cobo.Domain.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using System.Data.SqlClient;

namespace Cobo.Domain.Repos.Account;
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
}

