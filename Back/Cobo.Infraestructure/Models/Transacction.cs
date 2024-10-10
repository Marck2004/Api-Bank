namespace Cobo.Infraestructure.Models;

public partial class Transactions
{
    public Guid Id { get; set; }

    public string NumCuentaOrg { get; set; } = null!;

    public string NumCuentaDst { get; set; } = null!;

    public double Cant { get; set; }
}
