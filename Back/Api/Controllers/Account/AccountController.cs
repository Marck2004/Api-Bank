using Cobo.Application.Dtos.Account;
using Cobo.Domain.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cobo.Api.Controllers.Account;

[Authorize]
[Route("api/[Controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountInterface _accountInterface;

    public AccountController(IAccountInterface context)
    {
        _accountInterface = context;
    }

    [HttpGet("{userId}")]
    public async Task<IEnumerable<AccountQueriesDto>> GetAccountsByUserId(Guid userId)
    {
        return await _accountInterface.GetAccountsByUserId(userId);
    }

    [HttpGet("Number/{accountNumber}")]
    public async Task<Infraestructure.Models.Account> GetAccountByAccountNumber(string accountNumber)
    {
        return await _accountInterface.GetAccountsByAccountNumber(accountNumber);
    }

    [HttpPost("{userId}")]
    public IActionResult AddAccountByUserId(Guid userId)
    {
        Result response = _accountInterface.AddAccountByUserId(userId);
        if (response.IsFailed) return Problem(response.Errors.First().ToString());

        return Ok(response);
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountCommand deletedAccount)
    {
        Result response = await _accountInterface.DeleteAccountById(deletedAccount);
        if (response.IsFailed) return Problem(response.Errors.First().ToString());
        return Ok(response);
    }
}

