using Microsoft.AspNetCore.Mvc;
using VaultBank.Application.Interfaces;
using VaultBank.Application.Models.DTOs;

namespace VaultBank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    // ✅ Constructor injection
    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _customerService.GetAllCustomersAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var result = await _customerService.CreateCustomerAsync(request);
        return CreatedAtAction(nameof(GetCustomer), new { id = result.Id }, result);
    }
}