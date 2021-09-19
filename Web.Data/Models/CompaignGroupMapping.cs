using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class CompaignGroupMapping: BaseAuditedEntity
    {
        public Guid CompaignId { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("CompaignId")]
        public virtual Compaign Compaign { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

    }
}
