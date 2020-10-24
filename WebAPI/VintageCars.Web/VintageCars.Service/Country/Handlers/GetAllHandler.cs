using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Service.Country;
using VintageCars.Domain.Common;
using VintageCars.Domain.Country.Commands;
using VintageCars.Domain.Country.Response;
using VintageCars.Domain.Extensions;

namespace VintageCars.Service.Country.Handlers
{
    public class GetAllHandler : IRequestHandler<GetAllCommand, PagedList<CountryView>>
    {
        private readonly ICountryService _countryService;

        public GetAllHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public Task<PagedList<CountryView>> Handle(GetAllCommand request, CancellationToken cancellationToken)
        {
            var pagedList = _countryService.GetAllCountries(request.Paged.PageIndex, request.Paged.PageSize);
            return Task.FromResult(pagedList.ConvertPagedList<Nop.Core.Domain.Directory.Country,CountryView>());
        }
    }
}
