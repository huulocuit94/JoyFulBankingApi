using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Infrastructures
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
