using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Infrastructures
{
    public interface IAuditedEntity
    {
        Guid CreatedByUserId { get; set; }
        Guid ModifiedByUserId { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset ModifiedDate { get; set; }
    }
}
