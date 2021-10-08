using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Core.Infrastructures;

namespace Web.Application.Shared.Dtos.Compaigns
{
    public class CompaignDto : IEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long YoysAchievement { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
        public CompaignStatus Status { get; set; }
        public string Description { get; set; }
        public string FileData { get; set; }
        public decimal Percent { get; set; } = 0;
    }
}
