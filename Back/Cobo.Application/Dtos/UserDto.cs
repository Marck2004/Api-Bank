using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using NIF;
namespace Cobo.Application.Dtos;
public class UserDto
{
    public string Name { get; set; }
    public string Dni { get; set; }
    public string Password { get; set; }
    public ICollection<AccountDto?> Account {  get; set; }

}

