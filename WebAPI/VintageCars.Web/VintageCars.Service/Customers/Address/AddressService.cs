using System;
using Nop.Data;

namespace VintageCars.Service.Customers.Address
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Nop.Core.Domain.Common.Address> _addressRepository;

        public AddressService(IRepository<Nop.Core.Domain.Common.Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public Nop.Core.Domain.Common.Address GetAddress(Guid addressId)
        {
            return _addressRepository.GetById(addressId);
        }

        public Nop.Core.Domain.Common.Address Insert(Nop.Core.Domain.Common.Address address)
        {
            _addressRepository.Insert(address);
            return _addressRepository.GetById(address.Id);
        }

        public void Update(Nop.Core.Domain.Common.Address address)
        {
            _addressRepository.Update(address);
        }

        public void Delete(Nop.Core.Domain.Common.Address address)
        {
            _addressRepository.Delete(address);
        }
    }
}
