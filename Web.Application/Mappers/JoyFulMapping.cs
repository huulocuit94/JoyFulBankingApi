using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Dtos.Compaigns;
using Web.Data.Models;

namespace Web.Application.Mappers
{
    public class JoyFulMapping: Profile
    {
        public JoyFulMapping()
        {
            CreateMap<Compaign, CompaignDto>().ReverseMap();
        }
    }
}
