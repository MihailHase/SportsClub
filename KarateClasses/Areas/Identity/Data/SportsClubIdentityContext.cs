using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsCLubClasses.Models;

namespace SportsCLubClasses.Data;

public class SportsCLubClassesIdentityContext : IdentityDbContext<IdentityUser>
{
    public SportsCLubClassesIdentityContext(DbContextOptions<SportsCLubClassesIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<SportsCLubClasses.Models.SportsCLub>? SportsCLub { get; set; }
}
