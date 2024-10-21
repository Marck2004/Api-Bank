using Cobo.Application.Dtos.Transaction;
using Cobo.Domain.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cobo.Api.Controllers.Transactions;

[Authorize]
[Route("api/[Controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionInterface _transactionInterface;

    public TransactionController(ITransactionInterface context)
    {
        _transactionInterface = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto request)
    {
        Result response = await _transactionInterface.CreateTransaction(request);
        if (response.IsFailed) return Problem(response.Errors.First().Message);

        return Ok(response);
    }
}
