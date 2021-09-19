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
    public class TransferScore : BaseAuditedEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public long TranferedScores { get; set; } = 0;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
