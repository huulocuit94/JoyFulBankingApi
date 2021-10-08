using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.BaseModels;
using Web.Data.Models.IdentityUser;

namespace Web.Data.Models
{
    public class TransferJoy: BaseAuditedEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public Guid GiftId { get; set; }
        public long TranferedJoys { get; set; } = 0;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("GiftId")]
        public virtual Gift Gift { get; set; }
    }
}
