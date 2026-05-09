using MediatR;
using Microsoft.AspNetCore.Mvc;
using Valsy.Application.Customers.Commands.CreateCustomer;

namespace Valsy.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;

    public CustomersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customerId = await _sender.Send(
            new CreateCustomerCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.AddressLine1,
                request.City,
                request.Country,
                request.RequestedBy),
            cancellationToken);

        return Ok(new { customerId });
    }

    public record CreateCustomerRequest(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string AddressLine1,
        string City,
        string Country,
        string RequestedBy);
}
