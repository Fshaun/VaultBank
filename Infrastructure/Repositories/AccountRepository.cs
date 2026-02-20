using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VaultBank.Domain.Entities;
using VaultBank.Domain.Repositories;
using VaultBank.Infrastructure.Persistence;

namespace VaultBank.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly VaultDbContext _db;

    public AccountRepository(VaultDbContext db)
    {
        _db = db;
    }

    public async Task<BankAccount?> GetByIdAsync(Guid id) =>
        await _db.Accounts.Include(a => a.Transactions)
                          .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<BankAccount?> GetByAccountNumberAsync(string accountNumber) =>
        await _db.Accounts.Include(a => a.Transactions)
                          .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

    public async Task<IEnumerable<BankAccount>> GetByCustomerAsync(Guid customerId) =>
        await _db.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();

    public async Task AddAsync(BankAccount account) =>
        await _db.Accounts.AddAsync(account);

    public Task UpdateAsync(BankAccount account)
    {
        _db.Accounts.Update(account);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _db.SaveChangesAsync();
}