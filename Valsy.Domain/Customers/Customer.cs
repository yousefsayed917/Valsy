using Valsy.Domain.Common;
using Valsy.Domain.Orders;

namespace Valsy.Domain.Customers;

public class Customer : AggregateRoot<int>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public Address Address { get; private set; }
    public List<Order> Orders { get; private set; } = new();

    private Customer() { }

    public Customer Create(Customer customer)
    {
        return customer;
    }
}
public record Address(string AddressLine1, string City, string Country);
