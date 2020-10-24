using System.Collections.Generic;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Common;
using VintageCars.Domain.Country.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Country.Commands
{
    public class GetAllCommand : QueryPagedBase<PagedList<CountryView>>
    {
        public GetAllCommand()
        {
        }

        public GetAllCommand(PagedRequest paged) : base(paged)
        {
        }
    }
}
