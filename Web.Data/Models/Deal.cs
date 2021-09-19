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
            Tags = new HashSet<Tag>();
        }
        public string Title { get; set; }
        public string Link { get; set; }
        public string SourceLink { get; set; }
        public string Description { get; set; }
        public DealStatus Status { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
