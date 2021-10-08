using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Categories;
using Web.Application.Shared.Dtos.Compaigns;
using Web.Application.Shared.Dtos.Deals;
using Web.Application.Shared.Dtos.Gifts;
using Web.Application.Shared.Dtos.Groups;
using Web.Data.Models;

namespace Web.Application.Mappers
{
    public class JoyFulMapping : Profile
    {
        public JoyFulMapping()
        {
            CreateMap<Compaign, CompaignDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Gift, GiftDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Deal, DealDto>()
                .ForMember(x => x.Compaign, opt => opt.MapFrom(y => y.Compaign.Name))
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.Name))
                .ForMember(x => x.ExpiredDate, opt => opt.MapFrom(y => y.Compaign.ExpiredDate))
                .ReverseMap();
            CreateMap<Deal, DealGroupDto>()
                 .ForMember(x => x.Compaign, opt => opt.MapFrom(y => y.Compaign.Name))
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.Name))
                .ForMember(x => x.ExpiredDate, opt => opt.MapFrom(y => y.Compaign.ExpiredDate));
        }
    }
}
