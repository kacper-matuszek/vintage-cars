using System;

namespace VintageCars.Service.Customers.Address
{
    public interface IAddressService
    {
        Nop.Core.Domain.Common.Address GetAddress(Guid addressId);
        Nop.Core.Domain.Common.Address Insert(Nop.Core.Domain.Common.Address address);
        void Update(Nop.Core.Domain.Common.Address address);
        void Delete(Nop.Core.Domain.Common.Address address);
    }
}