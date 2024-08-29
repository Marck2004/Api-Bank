using System;
using System.Collections.Generic;

namespace Cobo.Domain.Models;

public partial class Transacction
{
    public Guid Id { get; set; }

    public string NumCuentaOrg { get; set; } = null!;

    public string NumCuentaDst { get; set; } = null!;

    public decimal Cant { get; set; }
}
