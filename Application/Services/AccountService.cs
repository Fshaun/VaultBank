using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Application.Interfaces;
using VaultBank.Application.Models.DTOs;
using VaultBank.Domain.Entities;
using VaultBank.Domain.Enums;
using VaultBank.Domain.Repositories;

namespace VaultBank.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepo;
    private readonly ICustomerRepository _customerRepo;
    private readonly ITransactionRepository _transactionRepo;

    // ✅ Constructor injection
    public AccountService(IAccountRepository accountRepo, ICustomerRepository customerRepo, ITransactionRepository transactionRepo)
    {
        _accountRepo = accountRepo;
        _customerRepo = customerRepo;
        _transactionRepo = transactionRepo;
    }

    public async Task<AccountDto?> GetAccountAsync(Guid id)
    {
        var account = await _accountRepo.GetByIdAsync(id);
        return account is null ? null : MapToDto(account);
    }

    public async Task<IEnumerable<AccountDto>> GetCustomerAccountsAsync(Guid customerId)
    {
        var accounts = await _accountRepo.GetByCustomerAsync(customerId);
        return accounts.Select(MapToDto);
    }

    public async Task<AccountDto> OpenAccountAsync(CreateAccountRequest request)
    {
        var customer = await _customerRepo.GetByIdAsync(request.CustomerId);
        if (customer is null)
            throw new InvalidOperationException("Customer does not exist.");

        var account = new BankAccount
        {
            Id = Guid.NewGuid(),
            CustomerId = customer.Id,
            AccountType = request.AccountType,
            AccountNumber = GenerateAccountNumber()
        };

        await _accountRepo.AddAsync(account);
        await _accountRepo.SaveChangesAsync();

        return MapToDto(account);
    }

    public async Task DepositAsync(Guid accountId, decimal amount, string description)
    {
        var account = await _accountRepo.GetByIdAsync(accountId)
                      ?? throw new InvalidOperationException("Account not found.");

        if (account.IsFrozen)
            throw new InvalidOperationException("Account is frozen.");

        account.Deposit(amount);

        var tx = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Type = TransactionType.Deposit,
            Amount = amount,
            OccurredAt = DateTime.UtcNow,
            Description = description
        };

        await _transactionRepo.AddAsync(tx);
        await _accountRepo.UpdateAsync(account);
        await _accountRepo.SaveChangesAsync();
        await _transactionRepo.SaveChangesAsync();
    }

    public async Task WithdrawAsync(Guid accountId, decimal amount, string description)
    {
        var account = await _accountRepo.GetByIdAsync(accountId)
                      ?? throw new InvalidOperationException("Account not found.");

        if (account.IsFrozen)
            throw new InvalidOperationException("Account is frozen.");

        account.Withdraw(amount);

        var tx = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Type = TransactionType.Withdraw,
            Amount = amount,
            OccurredAt = DateTime.UtcNow,
            Description = description
        };

        await _transactionRepo.AddAsync(tx);
        await _accountRepo.UpdateAsync(account);
        await _accountRepo.SaveChangesAsync();
        await _transactionRepo.SaveChangesAsync();
    }

    public async Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount, string description)
    {
        if (fromAccountId == toAccountId)
            throw new ArgumentException("Cannot transfer to the same account.");

        var from = await _accountRepo.GetByIdAsync(fromAccountId)
                   ?? throw new InvalidOperationException("Source account not found.");
        var to = await _accountRepo.GetByIdAsync(toAccountId)
                   ?? throw new InvalidOperationException("Destination account not found.");

        if (from.IsFrozen || to.IsFrozen)
            throw new InvalidOperationException("One of the accounts is frozen.");

        from.Withdraw(amount);
        to.Deposit(amount);

        var txFrom = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = from.Id,
            Type = TransactionType.Transfer,
            Amount = -amount,
            OccurredAt = DateTime.UtcNow,
            Description = $"Transfer to {to.AccountNumber}: {description}"
        };

        var txTo = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = to.Id,
            Type = TransactionType.Transfer,
            Amount = amount,
            OccurredAt = DateTime.UtcNow,
            Description = $"Transfer from {from.AccountNumber}: {description}"
        };

        await _transactionRepo.AddAsync(txFrom);
        await _transactionRepo.AddAsync(txTo);

        await _accountRepo.UpdateAsync(from);
        await _accountRepo.UpdateAsync(to);

        await _accountRepo.SaveChangesAsync();
        await _transactionRepo.SaveChangesAsync();
    }

    private static string GenerateAccountNumber()
    {
        // Very simple number, in real life you'd use something proper
        return $"VB-{Random.Shared.Next(10000000, 99999999)}";
    }

    private static AccountDto MapToDto(BankAccount account) =>
        new()
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            CustomerId = account.CustomerId,
            AccountType = account.AccountType,
            Balance = account.Balance,
            IsFrozen = account.IsFrozen
        };
}
