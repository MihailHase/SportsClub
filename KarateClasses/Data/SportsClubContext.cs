using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsCLubClasses.Models;
using SportsCLubClasses.Data;

namespace SportsCLubClasses.Data
{
    public class SportsCLubClassesContext : IdentityDbContext
    {
        public SportsCLubClassesContext(DbContextOptions<SportsCLubClassesContext> options)
            : base(options)
        {
        }

        public DbSet<SportsCLubClasses.Models.SportsCLub> SportsCLub { get; set; } = default!;

        public DbSet<SportsCLubClasses.Models.Location> Location { get; set; }

        public DbSet<SportsCLubClasses.Models.Trainer> Trainer { get; set; }

        public DbSet<SportsCLubClasses.Models.Member> Member { get; set; }

        public DbSet<SportsCLubClasses.Models.Subscription> Subscription { get; set; }
    }
}
