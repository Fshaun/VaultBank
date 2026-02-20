using Microsoft.AspNetCore.Mvc;
using VaultBank.Application.Interfaces;
using VaultBank.Application.Models.DTOs;

namespace VaultBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAccount(Guid id)
    {
        var account = await _accountService.GetAccountAsync(id);
        if (account == null) return NotFound();
        return Ok(account);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetCustomerAccounts(Guid customerId)
    {
        var accounts = await _accountService.GetCustomerAccountsAsync(customerId);
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<IActionResult> Open([FromBody] CreateAccountRequest request)
    {
        var account = await _accountService.OpenAccountAsync(request);
        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }

    [HttpPost("{id:guid}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] DepositRequest request)
    {
        await _accountService.DepositAsync(id, request.Amount, request.Description);
        return NoContent();
    }

    [HttpPost("{id:guid}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] WithdrawRequest request)
    {
        await _accountService.WithdrawAsync(id, request.Amount, request.Description);
        return NoContent();
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        await _accountService.TransferAsync(
            request.FromAccountId,
            request.ToAccountId,
            request.Amount,
            request.Description);

        return NoContent();
    }
}