using Cobo.Infraestructure.Models;
using System;
using System.Collections.Generic;

namespace Cobo.Domain.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public string NumCuenta { get; set; } = null!;

    public decimal Balance { get; set; }

    public Guid IdUsuario { get; set; }

    public virtual User IdUsuarioNavigation { get; set; } = null!;
}
