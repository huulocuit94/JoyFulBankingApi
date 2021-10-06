using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Infrastructures;

namespace Web.Core.Dtos
{
    public class PagedList<T> : IPagedList<T>
    {
        public int TotalCount { get; set; }

        public IList<T> Items { get; set; }
    }
}
