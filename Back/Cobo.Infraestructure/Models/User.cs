namespace Cobo.Infraestructure.Models;

public partial class User
{
    public string Nombre { get; set; } = string.Empty;
    public string? Apellido { get; set; } = string.Empty;
    public string Passwd { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid Id { get; set; }
}
