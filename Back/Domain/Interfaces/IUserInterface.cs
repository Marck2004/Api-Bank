using Cobo.Application.Dtos.Users;
using FluentResults;


namespace Cobo.Domain.Interfaces;
public interface IUserInterface
{
    public Task<IEnumerable<QueriesUserDto>> GetUsers();
    public Task<QueriesUserDto> GetUser(string email, string password);
    public Result UpdateUser(Guid id, CommandsUserDto user);
    public Result DeleteUser(Guid id);
    public Result AddUser(CommandsUserDto user);
}

