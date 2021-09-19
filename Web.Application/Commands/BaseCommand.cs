using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Application.Commands
{
    public class BaseCommand
    {
        [JsonIgnore]
        [BindNever]
        public Guid CurrentUserId { get; set; }
    }
}
