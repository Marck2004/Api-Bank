namespace Cobo.Infraestructure.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public string NumCuenta { get; set; } = null!;

    public double Balance { get; set; }

    public Guid UserId { get; set; }

    public DateTimeOffset FechaCreacion { get; set; }

    public virtual User IdUsuarioNavigation { get; set; } = null!;
}
