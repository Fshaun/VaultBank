using Microsoft.AspNetCore.Mvc;
using VaultBank.Application.Interfaces;

namespace VaultBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("account/{accountId:guid}")]
    public async Task<IActionResult> GetForAccount(Guid accountId)
    {
        var txs = await _transactionService.GetTransactionsForAccountAsync(accountId);
        return Ok(txs);
    }
}