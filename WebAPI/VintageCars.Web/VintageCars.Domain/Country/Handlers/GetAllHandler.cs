using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core;
using Nop.Core.Infrastructure.Mapper;
using Nop.Service.Country;
using VintageCars.Domain.Country.Commands;
using VintageCars.Domain.Country.Response;

namespace VintageCars.Domain.Country.Handlers
{
    public class GetAllHandler : IRequestHandler<GetAllCommand, IPagedList<CountryView>>
    {
        private readonly ICountryService _countryService;

        public GetAllHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public Task<IPagedList<CountryView>> Handle(GetAllCommand request, CancellationToken cancellationToken)
        {
            var pagedList = _countryService.GetAllCountries(request.Paged.PageIndex, request.Paged.PageSize);
            return Task.FromResult(
                AutoMapperConfiguration.Mapper
                    .Map<IPagedList<Nop.Core.Domain.Directory.Country>, IPagedList<CountryView>>(pagedList));
        }
    }
}
