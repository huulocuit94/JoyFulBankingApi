using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class SharedDealTracking : BaseAuditedEntity
    {
        public Guid DealId { get; set; }
        public Guid? GroupId { get; set; }
        public string LinkToSharing { get; set; }
        public virtual Deal Deal {get;set; }
        public virtual Group Group { get;set; }
    }
}
