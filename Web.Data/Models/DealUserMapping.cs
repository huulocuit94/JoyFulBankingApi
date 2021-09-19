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
    public class DealUserMapping: BaseAuditedEntity
    {
        public Guid UserId { get; set; }
        public Guid DealId { get; set; }
        public Guid? TransactionId { get; set; }
        public double Amount { get; set; } // Số tiền chi
        public long Score { get; set; } // Số điểm đạt được trên từng deal
        public DealStatus Status { get; set; } // Trạng thái khi nhận deal                                
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DealId")]
        public virtual Deal Deal { get; set; }
    }
}
