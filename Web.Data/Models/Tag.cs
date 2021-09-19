using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class Tag : BaseAuditedEntity
    {
        public Tag()
        {
            Categories = new HashSet<Category>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
