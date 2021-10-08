using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Core.Infrastructures;

namespace Web.Application.Shared.Dtos.Groups
{
    public class GroupDto: IEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Joys { get; set; } // So Joy tich luy
        public Rank Rank { get; set; }
        public string GroupPicture { get; set; }
    }
}
