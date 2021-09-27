using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;
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
        public long Joys { get; set; } // So Joy tich luy
        public Rank Rank { get; set; }
        public string GroupPicture { get; set; }
        public virtual ICollection<GroupUserMapping> GroupMappings { get; set; }
        public virtual ICollection<CompaignGroupMapping> CompaignGroupMappings { get; set; }
    }
}
