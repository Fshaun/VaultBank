using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Application.Models.DTOs;

namespace VaultBank.Application.Interfaces
{
    public interface IAccountService
    {

        Task<AccountDto?> GetAccountAsync(Guid id);
        Task<IEnumerable<AccountDto>> GetCustomerAccountsAsync(Guid customerId);
        Task<AccountDto> OpenAccountAsync(CreateAccountRequest request);
        Task DepositAsync(Guid accountId, decimal amount, string description);
        Task WithdrawAsync(Guid accountId, decimal amount, string description);
        Task TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount, string description);

    }
}
