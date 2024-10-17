using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySite.Data;
using Microsoft.AspNetCore.Identity;
using MySite.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MySiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySiteContext") ?? throw new InvalidOperationException("Connection string 'MySiteContext' not found.")));

builder.Services.AddDbContext<MySiteDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySiteContext") ??   throw new InvalidOperationException("Connection string 'MySiteContext' not found.")));


builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MySiteDBContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
