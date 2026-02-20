using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Enums;

namespace VaultBank.Application.Models.DTOs
{
    public class AccountDto
    {

        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool IsFrozen { get; set; }

    }
}
