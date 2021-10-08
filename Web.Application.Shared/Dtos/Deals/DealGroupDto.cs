using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Shared.Enums;

namespace Web.Application.Shared.Dtos.Deals
{
    public class DealGroupDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Rules { get; set; }
        public string Description { get; set; }
        public string FileData { get; set; }
        public string Compaign { get; set; }
        public string Category { get; set; }
        public Guid? GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
        public DealStatus Status { get; set; }
    }
}
