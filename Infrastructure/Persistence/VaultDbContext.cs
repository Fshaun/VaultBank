using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Entities;

namespace VaultBank.Infrastructure.Persistence;

public class VaultDbContext : DbContext
{
    public VaultDbContext(DbContextOptions<VaultDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<BankAccount> Accounts => Set<BankAccount>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VaultDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
