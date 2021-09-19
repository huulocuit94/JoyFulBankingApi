using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Infrastructures;
using Web.Data.Models.IdentityUser;

namespace Web.Data.BaseModels
{
    public class AuditedEntity : Entity, IAuditedEntity
    {
        [ForeignKey("CreatedByUserId")]
        public Guid CreatedByUserId { get ; set ; }
        [ForeignKey("ModifiedByUserId")]
        public Guid ModifiedByUserId { get ; set ; }
        public DateTimeOffset CreatedDate { get ; set ; }
        public DateTimeOffset ModifiedDate { get ; set ; }
        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; }
        [ForeignKey("ModifiedByUserId")]
        public virtual User ModifiedByUser { get; set; }
    }
}
