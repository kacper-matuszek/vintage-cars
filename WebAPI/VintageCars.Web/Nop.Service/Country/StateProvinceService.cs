using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Directory;
using Nop.Data;

namespace Nop.Service.Country
{
    public class StateProvinceService : IStateProvinceService
    {
        private readonly IRepository<StateProvince> _stateProvinceRepository;

        public StateProvinceService(IRepository<StateProvince> stateProvinceRepository)
        {
            _stateProvinceRepository = stateProvinceRepository;
        }

        public virtual IPagedList<StateProvince> GetAll(Guid countryId, bool published = true, int pageIndex = 0,
            int pageSize = Int32.MaxValue)
        {
            var query = _stateProvinceRepository.Table
                .Where(sp => sp.CountryId == countryId && sp.Published == published).OrderBy(sp => sp.DisplayOrder)
                .ThenBy(sp => sp.Name);

            return new PagedList<StateProvince>(query, pageIndex, pageSize);
        }
    }
}
