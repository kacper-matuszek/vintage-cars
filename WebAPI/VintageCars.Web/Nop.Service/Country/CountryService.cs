using System;
using System.Linq;
using Nop.Core;
using Nop.Data;

namespace Nop.Service.Country
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Core.Domain.Directory.Country> _countryRepository;

        public CountryService(IRepository<Core.Domain.Directory.Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public virtual IPagedList<Core.Domain.Directory.Country> GetAllCountries(int pageIndex = 0,
            int pageSize = Int32.MaxValue, bool published = true)
        {
            var query = _countryRepository.Table.Where(c => c.Published == published).OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name);

            return new PagedList<Core.Domain.Directory.Country>(query, pageIndex, pageSize);
        }
    }
}
