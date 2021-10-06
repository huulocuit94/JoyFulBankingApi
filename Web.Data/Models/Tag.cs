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
            
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
