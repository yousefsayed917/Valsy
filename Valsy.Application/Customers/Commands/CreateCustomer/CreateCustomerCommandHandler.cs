using AutoMapper;
using MediatR;
using Valsy.Domain.Customers;
using Valsy.Domain.Customers.Repository;

namespace Valsy.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = _mapper.Map<Customer>(request);
        customer.Create(customer);

        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();
        return customer.Id;
    }
}
