using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Domain.Enums;

namespace VaultBank.Application.Models.DTOs
{
    public class CreateAccountRequest
    {

        public Guid CustomerId { get; set; }
        public AccountType AccountType { get; set; }

    }
}
