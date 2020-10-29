using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core.Infrastructure.Mapper;
using Nop.Service.Customer;
using VintageCars.Domain.Customer.Address.Commands;
using VintageCars.Domain.Customer.Address.Responses;

namespace VintageCars.Service.Customers.Address.Handlers
{
    public class GetAddressDetailHandler : IRequestHandler<GetAddressDetailQuery, AddressDetailResponse>
    {
        private readonly ICustomerService _customerService;

        public GetAddressDetailHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Task<AddressDetailResponse> Handle(GetAddressDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                throw new ArgumentNullException();

            var address = _customerService.GetAddressesByCustomerId(request.UserId).FirstOrDefault();

            return Task.FromResult(AutoMapperConfiguration.Mapper.Map<AddressDetailResponse>(address));
        }
    }
}
