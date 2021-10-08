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
    public class DealUserMapping : BaseAuditedEntity
    {
        public Guid UserId { get; set; }
        public Guid DealId { get; set; }
        public Guid? TransactionId { get; set; }
        public UserDealStatus UserDealStatus { get; set; }
        public Guid? FromGroupId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DealId")]
        public virtual Deal Deal { get; set; }
        [ForeignKey("FromGroupId")]
        public virtual Group Group { get; set; }
    }
}
