using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaultBank.Application.Interfaces;
using VaultBank.Application.Models.DTOs;
using VaultBank.Domain.Entities;
using VaultBank.Domain.Repositories;

namespace VaultBank.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepo;

    // ✅ Constructor injection
    public CustomerService(ICustomerRepository customerRepo)
    {
        _customerRepo = customerRepo;
    }

    public async Task<CustomerDto?> GetCustomerAsync(Guid id)
    {
        var customer = await _customerRepo.GetByIdAsync(id);
        return customer is null ? null : MapToDto(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepo.GetAllAsync();
        return customers.Select(MapToDto);
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request)
    {
        var existing = await _customerRepo.GetByEmailAsync(request.Email);
        if (existing != null)
            throw new InvalidOperationException("Customer with this email already exists.");

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone
        };

        await _customerRepo.AddAsync(customer);
        await _customerRepo.SaveChangesAsync();

        return MapToDto(customer);
    }

    private static CustomerDto MapToDto(Customer customer) =>
        new()
        {
            Id = customer.Id,
            FullName = $"{customer.FirstName} {customer.LastName}",
            Email = customer.Email,
            Phone = customer.Phone
        };
}