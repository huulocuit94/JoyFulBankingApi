using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class Deal : BaseAuditedEntity
    {
        public Deal()
        {
        }
        public string Title { get; set; }
        public string Link { get; set; }
        public string SourceLink { get; set; }
        public string Rules { get; set; }
        public string Description { get; set; }
        public DealStatus Status { get; set; }
        public Guid CompaignId { get; set; }
        public string FileData { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Compaign Compaign { get; set; }
        public virtual Category Category { get; set; }
    }
}
