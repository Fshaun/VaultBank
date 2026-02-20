using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Application.Models.DTOs;

namespace VaultBank.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetTransactionsForAccountAsync(Guid accountId);
    }
}
