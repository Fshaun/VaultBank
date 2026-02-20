using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Enums;

namespace VaultBank.Application.Models.DTOs
{
    public class TransactionDto
    {

        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}