

using Cobo.Domain.Interfaces;
using Moq;

namespace Cobo.UnitTest.Users;
public class UsersUnitTests : IAsyncLifetime
{
    private readonly Mock<IUserInterface> _userService = new();

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _userService.Reset();
        await Task.CompletedTask;
    }

}
