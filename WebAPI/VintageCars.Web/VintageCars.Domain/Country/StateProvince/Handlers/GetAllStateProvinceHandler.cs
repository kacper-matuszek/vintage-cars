using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nop.Core;
using Nop.Core.Infrastructure.Mapper;
using Nop.Service.Country;
using VintageCars.Domain.Country.StateProvince.Commands;
using VintageCars.Domain.Country.StateProvince.Response;

namespace VintageCars.Domain.Country.StateProvince.Handlers
{
    public class GetAllStateProvinceHandler : IRequestHandler<GetAllStateProvinceCommand, IPagedList<StateProvinceView>>
    {
        private readonly IStateProvinceService _stateProvinceService;

        public GetAllStateProvinceHandler(IStateProvinceService stateProvinceService)
        {
            _stateProvinceService = stateProvinceService;
        }

        public Task<IPagedList<StateProvinceView>> Handle(GetAllStateProvinceCommand request, CancellationToken cancellationToken)
        {
            var pagedList = _stateProvinceService.GetAll(request.CountryId, pageIndex: request.Paged.PageIndex,
                pageSize: request.Paged.PageSize);
            return Task.FromResult(AutoMapperConfiguration.Mapper
                .Map<IPagedList<Nop.Core.Domain.Directory.StateProvince>, IPagedList<StateProvinceView>>(pagedList));
        }
    }
}
