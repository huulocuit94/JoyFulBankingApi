using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Core.Infrastructures;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class Compaign : BaseAuditedEntity
    {
        public Compaign()
        {
            CompaignUserMappings = new HashSet<CompaignUserMapping>();
            Deals = new HashSet<Deal>();
        }
        public string Name { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
        public CompaignStatus Status { get; set; }
        public string Description { get; set; }
        public string FileData { get; set; }
        public long YoysAchievement { get; set; }
        public virtual ICollection<Deal> Deals { get; set; }
        public virtual ICollection<CompaignUserMapping> CompaignUserMappings { get; set; }
    }
}
