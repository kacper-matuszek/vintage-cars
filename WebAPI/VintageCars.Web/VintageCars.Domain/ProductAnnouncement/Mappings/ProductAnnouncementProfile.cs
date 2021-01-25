using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using VintageCars.Domain.Extensions;
using VintageCars.Domain.ProductAnnouncement.Commands;
using VintageCars.Domain.ProductAnnouncement.Models;
using VintageCars.Domain.ProductAnnouncement.Response;
using Db = VintageCars.Data.Models;

namespace VintageCars.Domain.ProductAnnouncement.Mappings
{
    public class ProductAnnouncementProfile : Profile, IOrderedMapperProfile
    {
        public ProductAnnouncementProfile()
        {
            CreateMap<CreateProductAnnouncement, Db.ProductAnnouncement>()
                .GenerateIdFromCreateCommand()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
            CreateMap<ProductAnnouncementAttribute, Db.ProductAnnouncementAttribute>()
                .GenerateId();

            CreateMap<Db.ProductAnnouncement, ProductAnnouncementShortInfoView>()
                .ForMember(dest => dest.MainPicture, opt => opt.Ignore());
            CreateMap<Db.ProductAnnouncement, ProductAnnouncementDetailsView>();
        }
        public int Order => 7;
    }
}
