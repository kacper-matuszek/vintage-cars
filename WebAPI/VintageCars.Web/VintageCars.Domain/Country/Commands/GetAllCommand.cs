using System.Collections.Generic;
using Nop.Core;
using VintageCars.Domain.Commands.Base;
using VintageCars.Domain.Country.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Domain.Country.Commands
{
    public class GetAllCommand : QueryPagedBase<IPagedList<CountryView>>
    {
        public GetAllCommand()
        {
        }

        public GetAllCommand(PagedRequest paged) : base(paged)
        {
        }
    }
}
