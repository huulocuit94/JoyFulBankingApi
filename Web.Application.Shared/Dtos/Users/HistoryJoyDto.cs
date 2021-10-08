using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Shared.Dtos.Users
{
    public class HistoryJoyDto
    {
        public DateTimeOffset Date { get; set; }
        public string Content { get; set; }
        public string Joy { get; set; }
    }
}
