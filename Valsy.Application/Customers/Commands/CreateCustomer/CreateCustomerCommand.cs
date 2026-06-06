using MediatR;

namespace Valsy.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string AddressLine1,
    string City,
    string Country,
    string RequestedBy) : IRequest<int>;
