using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultBank.Application.Models.DTOs
{
    public class TransferRequest
    {

        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = "Transfer";

    }
}

