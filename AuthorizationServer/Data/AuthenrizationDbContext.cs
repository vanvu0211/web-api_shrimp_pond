using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.DatabaseContext
{
    public class AuthenrizationDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthenrizationDbContext(DbContextOptions options) : base(options)
        {
        }
    }

}
