using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Customer.Address.Responses;

namespace VintageCars.Domain.Customer.Address.Commands
{
    public class GetAddressDetailQuery : AuthorizationQueryBase<AddressDetailResponse>
    {
    }
}
