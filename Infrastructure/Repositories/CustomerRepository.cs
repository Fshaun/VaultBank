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

public class CustomerRepository : ICustomerRepository
{
    private readonly VaultDbContext _db;

    public CustomerRepository(VaultDbContext db)
    {
        _db = db;
    }

    public async Task<Customer?> GetByIdAsync(Guid id) =>
        await _db.Customers.FindAsync(id);

    public async Task<Customer?> GetByEmailAsync(string email) =>
        await _db.Customers.FirstOrDefaultAsync(c => c.Email == email);

    public async Task<IEnumerable<Customer>> GetAllAsync() =>
        await _db.Customers.ToListAsync();

    public async Task AddAsync(Customer customer) =>
        await _db.Customers.AddAsync(customer);

    public Task UpdateAsync(Customer customer)
    {
        _db.Customers.Update(customer);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() =>
        await _db.SaveChangesAsync();
}
