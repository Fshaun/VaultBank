using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Application.Interfaces;
using VaultBank.Application.Models.DTOs;
using VaultBank.Domain.Repositories;

namespace VaultBank.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepo;

    // ✅ Constructor injection
    public TransactionService(ITransactionRepository transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactionsForAccountAsync(Guid accountId)
    {
        var txs = await _transactionRepo.GetByAccountAsync(accountId);
        return txs.Select(t => new TransactionDto
        {
            Id = t.Id,
            AccountId = t.AccountId,
            Type = t.Type,
            Amount = t.Amount,
            OccurredAt = t.OccurredAt,
            Description = t.Description
        });
    }
}