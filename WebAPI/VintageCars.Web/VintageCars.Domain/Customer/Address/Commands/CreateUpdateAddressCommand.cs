using System;
using VintageCars.Domain.Commands.Base;

namespace VintageCars.Domain.Customer.Address.Commands
{
    public class CreateUpdateAddressCommand : AuthorizationCommandBase
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? StateProvinceId { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public string ZipPostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
