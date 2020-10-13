using System;
using Nop.Core;
using Nop.Core.Domain.Directory;

namespace Nop.Service.Country
{
    public interface IStateProvinceService
    {
        IPagedList<StateProvince> GetAll(Guid countryId, bool published = true, int pageIndex = 0,
            int pageSize = Int32.MaxValue);
    }
}