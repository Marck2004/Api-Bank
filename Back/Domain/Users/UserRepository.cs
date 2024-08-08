using Cobo.Application.Dtos;
using Cobo.Domain.Models;
using Domain.Interfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Cobo.Domain.UsersRepository;
public class UserRepository : IUserInterface
{
    private readonly BancoContext _context;
    public UserRepository(BancoContext context)
    {
        _context = context;
    }
    public Result deleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<UserDto> getUser(Guid id)
    {
        IQueryable<UserDto> user = (
            from Users in _context.Users
            where Users.Id == id
            select new UserDto
            {
                //Account = Users.Account,
                Dni = Users.Dni,
                Name = Users.Nombre,
                Password = Users.Passwd
            });
        return user;
    }

    public List<UserDto> getUsers()
    {
        IQueryable<UserDto> users = (
            from Users in _context.Users
            select new UserDto
                {
                Name = Users.Nombre,
                Password = Users.Passwd,
                Dni = Users.Dni,
                Account = Users.Account
                });
        return users.ToList();
    }

    public Result updateUser(Guid id, User user)
    {
        throw new NotImplementedException();
    }
}
