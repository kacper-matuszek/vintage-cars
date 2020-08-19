using Nop.Core.Domain.Customers;

namespace Nop.Core.Requests.Customers
{
    public class CustomerRegistrationRequest
    {
        public Customer Customer { get; set; }
        public string Password { get; set; }
        public PasswordFormat PasswordFormat { get; set; }
        public int StoreId { get; set; }
        public bool IsApproved { get; set; }
    }
}
