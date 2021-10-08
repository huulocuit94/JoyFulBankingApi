using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Shared.Dtos.Ranks
{
    public class RankGroupDto
    {
        public List<RandGroupDetailDto> Data { get; set; } = new List<RandGroupDetailDto>();
    }
    public class RandGroupDetailDto
    {
        public string Compaign { get; set; }
        public List<RankDto> Items { get; set; } = new List<RankDto>();
    }
}
