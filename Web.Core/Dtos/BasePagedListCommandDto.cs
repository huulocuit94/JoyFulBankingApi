using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Dtos
{
    public class BasePagedListCommandDto : BaseCommandDto
    {
        public string Filter { get; set; } = string.Empty;

        [Required]
        public int PageIndex { get; set; } = 1;

        [Required]
        public int PageSize { get; set; } = 100;

        public string Order { get; set; } = "ModifiedDate desc";
    }
}
