using Cobo.Domain.Models;
using Domain.Interfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public User getUser(Guid id)
    {
        User user = (User)_context.Users.Select(user => user.Id == id);
        return user;
    }

    public List<User> getUsers()
    {
        return _context.Users.ToList();
    }

    public Result updateUser(Guid id, User user)
    {
        throw new NotImplementedException();
    }
}
