using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class Group : BaseAuditedEntity
    {
        public Group()
        {
            GroupMappings = new HashSet<GroupUserMapping>();
            CompaignGroupMappings = new HashSet<CompaignGroupMapping>();
        }
        public string Name { get; set; }
        public virtual ICollection<GroupUserMapping> GroupMappings { get; set; }
        public virtual ICollection<CompaignGroupMapping> CompaignGroupMappings { get; set; }
    }
}
