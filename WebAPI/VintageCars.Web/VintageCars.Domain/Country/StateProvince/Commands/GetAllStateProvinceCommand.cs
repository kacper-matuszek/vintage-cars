using System;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Country.StateProvince.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Country.StateProvince.Commands
{
    public class GetAllStateProvinceCommand : QueryPagedBase<PagedList<StateProvinceView>>
    {
        public Guid CountryId { get;  }
        public GetAllStateProvinceCommand()
        {
        }

        public GetAllStateProvinceCommand(PagedRequest paged) 
            : base(paged)
        {
        }

        public GetAllStateProvinceCommand(Guid countryId, PagedRequest paged)
        {
            CountryId = countryId;
            Paged = paged;
        }
    }
}
