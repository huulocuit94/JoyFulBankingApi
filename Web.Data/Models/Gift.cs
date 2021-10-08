using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.BaseModels;

namespace Web.Data.Models
{
    public class Gift : BaseEntity
    {
        public string Name { get; set; }
        public string Description {get;set;}
        public string FileData { get; set; }
        public int Joys { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
    }
}
