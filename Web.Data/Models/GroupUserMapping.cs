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
    public class GroupUserMapping : BaseAuditedEntity
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public bool IsOwner { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
    }
}
