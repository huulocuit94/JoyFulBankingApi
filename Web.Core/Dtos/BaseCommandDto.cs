using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Core.Dtos
{
    public class BaseCommandDto
    {
        [JsonIgnore]
        [BindNever]
        public Guid CurrentUserId { get; set; }
    }
}
