using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainingStudio02.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/FitnessClasses");
    options.Conventions.AllowAnonymousToPage("/FitnessClasses/Index");
    options.Conventions.AllowAnonymousToPage("/FitnessClasses/Details");

    options.Conventions.AuthorizeFolder("/Locations");
    options.Conventions.AllowAnonymousToPage("/Locations/Index");
    options.Conventions.AllowAnonymousToPage("/Locations/Details");

    options.Conventions.AuthorizeFolder("/Trainers");
    options.Conventions.AllowAnonymousToPage("/Trainers/Index");
    options.Conventions.AllowAnonymousToPage("/Trainers/Details");

    options.Conventions.AuthorizeFolder("/Categories");
    options.Conventions.AllowAnonymousToPage("/Categories/Index");
    options.Conventions.AllowAnonymousToPage("/Categories/Details");

    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");

    options.Conventions.AuthorizeFolder("/Subscriptions", "AdminPolicy");
});

builder.Services.AddDbContext<TrainingStudio02Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrainingStudio02Context") ?? throw new InvalidOperationException("Connection string 'TrainingStudio02Context' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("TrainingStudio02Context") ?? throw new InvalidOperationException("Connection string 'TrainingStudio02Context' not found.")));


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
