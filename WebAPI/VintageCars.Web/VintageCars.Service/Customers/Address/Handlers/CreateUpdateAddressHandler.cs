using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using Nop.Service.Customer;
using VintageCars.Domain.Customer.Address.Commands;
using VintageCars.Domain.Exceptions;

namespace VintageCars.Service.Customers.Address.Handlers
{
    public class CreateUpdateAddressHandler : IRequestHandler<CreateUpdateAddressCommand, Unit>
    {
        private readonly IAddressService _addressService;
        private readonly ICustomerService _customerService;

        public CreateUpdateAddressHandler(IAddressService addressService, ICustomerService customerService)
        {
            _addressService = addressService;
            _customerService = customerService;
        }

        public Task<Unit> Handle(CreateUpdateAddressCommand request, CancellationToken cancellationToken)
        {
            Nop.Core.Domain.Common.Address address = null;
            if (!request.Id.HasValue)
                address = _addressService.Insert(
                    AutoMapperConfiguration.Mapper.Map<Nop.Core.Domain.Common.Address>(request));
            else
            {
                address = _addressService.GetAddress(request.Id.Value);
                var addrToUpdate = AutoMapperConfiguration.Mapper.Map(request, address);
                _addressService.Update(addrToUpdate);
            }

            var customerAddress = _customerService.GetAddressesByCustomerId(request.UserId).FirstOrDefault(a => a.Id == address?.Id);
            var customer = _customerService.GetCustomer(request.UserId);
            
            try
            {
                if (customerAddress is null)
                    _customerService.InsertCustomerAddress(customer, address);
            }
            catch (ArgumentNullException ex)
            {
                throw new ResourcesNotFoundException("Not found required resources.", ex);
            }

            return Unit.Task;
        }
    }
}
