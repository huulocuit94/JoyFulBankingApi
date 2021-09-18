﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Models.IdentityUser
{
    public class UserLogin: IdentityUserLogin<Guid>
    {
        public virtual User User { get; set; }
    }
}
