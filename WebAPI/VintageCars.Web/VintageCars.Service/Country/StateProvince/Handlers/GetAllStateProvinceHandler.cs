using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Service.Country;
using VintageCars.Domain.Common;
using VintageCars.Domain.Country.StateProvince.Commands;
using VintageCars.Domain.Country.StateProvince.Response;
using VintageCars.Domain.Extensions;

namespace VintageCars.Service.Country.StateProvince.Handlers
{
    public class GetAllStateProvinceHandler : IRequestHandler<GetAllStateProvinceCommand, PagedList<StateProvinceView>>
    {
        private readonly IStateProvinceService _stateProvinceService;

        public GetAllStateProvinceHandler(IStateProvinceService stateProvinceService)
        {
            _stateProvinceService = stateProvinceService;
        }

        public Task<PagedList<StateProvinceView>> Handle(GetAllStateProvinceCommand request, CancellationToken cancellationToken)
        {
            var pagedList = _stateProvinceService.GetAll(request.CountryId, pageIndex: request.Paged.PageIndex,
                pageSize: request.Paged.PageSize);
            return Task.FromResult(pagedList.ConvertPagedList<Nop.Core.Domain.Directory.StateProvince, StateProvinceView>());
        }
    }
}
