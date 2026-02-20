using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Entities;
using VaultBank.Domain.Repositories;
using VaultBank.Infrastructure.Persistence;

namespace VaultBank.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly VaultDbContext _db;

    public TransactionRepository(VaultDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Transaction>> GetByAccountAsync(Guid accountId) =>
        await _db.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.OccurredAt)
            .ToListAsync();

    public async Task AddAsync(Transaction transaction) =>
        await _db.Transactions.AddAsync(transaction);

    public async Task SaveChangesAsync() =>
        await _db.SaveChangesAsync();
}