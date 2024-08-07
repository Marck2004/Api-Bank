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
    public virtual ICollection<AccountDto>? Account {  get; set; }
    public UserDto(string name, string password, string dni, ICollection<AccountDto>? account)
    {
        Name = name;
        Password = password;
        if (validateDni(dni).IsSuccess)
        {
            Dni = dni;
        }
        else if(validateDni(dni).IsFailed)
        {
              throw new Exception("Error de validacion en el dni");
        }
        Account = account;
    }
    private Result validateDni(string dni)
    {
        bool isDniCorrect = NifValidator.Validate(dni);
        if (isDniCorrect)
        {
            return Result.Ok();
        }
        else
        {
            return Result.Fail("Error de validacion en el Dni");
        }
    }

}

