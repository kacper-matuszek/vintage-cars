using System;
using Nop.Core;

namespace Nop.Service.Country
{
    public interface ICountryService
    {
        IPagedList<Core.Domain.Directory.Country> GetAllCountries(int pageIndex = 0,
            int pageSize = Int32.MaxValue, bool published = true);
    }
}