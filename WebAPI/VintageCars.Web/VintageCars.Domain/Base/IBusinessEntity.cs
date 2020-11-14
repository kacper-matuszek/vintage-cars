using System;
using System.Collections.Generic;
using System.Text;

namespace VintageCars.Domain.Base
{
    public interface IBusinessEntity
    {
        Guid? Id { get; set; }
    }
}
