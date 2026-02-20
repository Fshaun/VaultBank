using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultBank.Application.Models.DTOs
{
    public class WithdrawRequest
    {

        public decimal Amount { get; set; }
        public string Description { get; set; } = "Withdrawal";

    }
}
