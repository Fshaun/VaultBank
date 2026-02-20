using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Entities;

namespace VaultBank.Domain.Repositories
{

    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByAccountAsync(Guid accountId);
        Task AddAsync(Transaction transaction);
        Task SaveChangesAsync();
    }

}
