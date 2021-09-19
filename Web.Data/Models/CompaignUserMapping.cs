using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
using Web.Data.BaseModels;
using Web.Data.Models.IdentityUser;

namespace Web.Data.Models
{
    public class CompaignUserMapping: BaseAuditedEntity
    { 
        public Guid CompaignId { get; set; }
        public Guid UserId { get; set; }
        public long JoyAmount { get; set; }
        [ForeignKey("CompaignId")]
        public virtual Compaign Compaign { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
