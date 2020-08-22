using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Customers;

namespace VintageCars.Service.Infrastructure
{
    public interface IJwtService
    {
        string GenerateToken(Customer customer, IEnumerable<CustomerRole> customerRole);
    }
}
