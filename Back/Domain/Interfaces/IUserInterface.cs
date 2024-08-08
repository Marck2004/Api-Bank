using Cobo.Application.Dtos;
using Cobo.Domain.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces;
    public interface IUserInterface
    {
    public  List<UserDto> getUsers();
    public IQueryable<UserDto> getUser(Guid id);
    public Result updateUser(Guid id, User user);
    public Result deleteUser(Guid id);
    }

