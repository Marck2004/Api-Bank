using Cobo.Application.Dtos;
using System;
using System.Collections.Generic;

namespace Cobo.Domain.Models;

public partial class User
{
    public string Nombre { get; set; } = null!;

    public string Passwd { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public Guid Id { get; set; }
    public ICollection<AccountDto?> Account {  get; set; }
}
