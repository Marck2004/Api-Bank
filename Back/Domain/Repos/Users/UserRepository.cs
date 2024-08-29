using Cobo.Application.Dtos.Account;
using Cobo.Application.Dtos.Users;
using Cobo.Domain.Interfaces;
using Cobo.Domain.Models;
using Cobo.Infraestructure.Models;
using Dapper;
using FluentResults;
using Serilog.Core;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Cobo.Domain.Repos.Users;
public class UserRepository : IUserInterface
{
    private readonly BancoContext _context;
    private readonly Logger _logger;

    private readonly string _connection = "Server=localhost;Database=Banco;Trusted_Connection=True;TrustServerCertificate=True";
    public UserRepository(BancoContext context)
    {
        _context = context;
    }
    public Result DeleteUser(Guid id)
    {
        SqlConnection connection = new(_connection);
        try
        {
            connection.Open();

            User? findUser = _context.Users.Find(id);

            if (findUser != null)
            {
                _context.Users.Remove(findUser);
                _context.SaveChanges();
                return Result.Ok();
            }
            return Result.Fail("No se ha podido borrar al usuario de la base de datos");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return Result.Fail("Error en la consulta a la base de datos");
        }
    }

    public async Task<QueriesUserDto> GetUser(string email, string password)
    {
        SqlConnection connection = new(_connection);

        await connection.OpenAsync();

        try
        {

            string sql = @"
        select 
            users.Id,
            users.Nombre as name,
            users.Apellido as surname,
		    users.Passwd as password,
            users.Email,
		    users.Dni as dni,
		    accounts.NumCuenta,
		    accounts.Balance,
            accounts.Id,
		    accounts.UserId
		from dbo.Users AS users
		left join dbo.Account AS accounts
		on users.Id = accounts.UserId 
        where users.Email = @email
        and users.Passwd = @password
        ";


            return connection.QueryAsync<QueriesUserDto, AccountQueriesDto, QueriesUserDto>(
                sql,
                (user, account) =>
                {
                    user.Account = new List<AccountQueriesDto>();

                    if (account != null)
                    {
                        user.Account.Add(account);
                    }

                    return user;
                },
                new { email, password },
                splitOn: "NumCuenta").Result.First();
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return new QueriesUserDto();
        }
    }

    public async Task<IEnumerable<QueriesUserDto>> GetUsers()
    {
        SqlConnection connection = new(_connection);

        try
        {
            await connection.OpenAsync();

            string sql = @"
        select 
            users.Id,
            users.Nombre as name, 
            users.Apellido as surname,
		    users.Passwd as password,
            users.Email,
		    users.Dni as dni,
		    accounts.NumCuenta,
		    accounts.Balance,
            accounts.Id,
		    accounts.UserId
		from dbo.Users AS users
		left join dbo.Account AS accounts
		on users.Id = accounts.UserId 
        ";


            return await connection.QueryAsync<QueriesUserDto, AccountQueriesDto, QueriesUserDto>(
                sql,
                (user, account) =>
                {
                    user.Account = new List<AccountQueriesDto>();

                    if (account != null)
                    {
                        user.Account.Add(account);
                    }

                    return user;
                },
                splitOn: "NumCuenta");
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return [];
        }
    }

    public Result UpdateUser(Guid id, CommandsUserDto user)
    {
        try
        {

            User? findUser = _context.Users.Find(id);

            if (findUser is null) return Result.Fail("No se ha encontrado ningun usuario en la base de datos");

            findUser.Nombre = user.Name;
            findUser.Dni = user.Dni;
            findUser.Passwd = user.Password;
            findUser.Email = user.Email;
            findUser.Apellido = user.Surname;

            _context.SaveChanges();

            return Result.Ok();

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return Result.Fail("Ocurrio un error en el servidor");
        }
    }

    public Result AddUser(CommandsUserDto newUser)
    {
        try
        {
            bool findDni = _context.Users.Select(repo => repo.Dni == newUser.Dni)
                            .FirstOrDefault();
            if (findDni)
                return Result.Fail("Un usuario ya existe con el mismo dni");

            bool findEmail = _context.Users.Find(newUser.Email)
                            .FirstOrDefault();
            if (findEmail)
                return Result.Fail("Un usuario ya existe con el mismo correo");

            if (!Regex.IsMatch(newUser.Dni.Trim(), @"^\d{8}[A-Z]$"))
                return Result.Fail("Formato inválido para el dni");

            if (!Regex.IsMatch(newUser.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return Result.Fail("Formato inválido para el email");

            User newGuidUser = new()
            {
                Dni = newUser.Dni,
                Id = Guid.NewGuid(),
                Nombre = newUser.Name,
                Apellido = newUser.Surname,
                Passwd = newUser.Password,
                Email = newUser.Email,
            };

            _context.Users.Add(newGuidUser);
            _context.SaveChanges();
            return Result.Ok();

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            return Result.Fail("Ocurrio un error en el servidor");
        }
    }
}
