using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Application.Models.DTOs;

namespace VaultBank.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerAsync(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request);

    }
}