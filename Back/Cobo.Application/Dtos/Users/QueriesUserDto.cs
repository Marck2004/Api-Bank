using Cobo.Application.Dtos.Account;

namespace Cobo.Application.Dtos.Users;
public class QueriesUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Surname { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public List<AccountQueriesDto>? Account { get; set; } = new List<AccountQueriesDto>();
}

