using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Entities;

namespace VaultBank.Domain.Repositories
{
    public interface IAccountRepository
    {

        Task<BankAccount?> GetByIdAsync(Guid id);
        Task<BankAccount?> GetByAccountNumberAsync(string accountNumber);
        Task<IEnumerable<BankAccount>> GetByCustomerAsync(Guid customerId);
        Task AddAsync(BankAccount account);
        Task UpdateAsync(BankAccount account);
        Task SaveChangesAsync();

    }
}
