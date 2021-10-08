using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Tags = new HashSet<Tag>();
        }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
