using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Infrastructures;

namespace Web.Application.Shared.Dtos.Gifts
{
   public class GiftDto: IEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileData { get; set; }
        public int Joys { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
    }
}
