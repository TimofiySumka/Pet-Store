using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySite.Data;
using Microsoft.AspNetCore.Identity;
using MySite.Areas.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Razor;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MySiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySiteContext") ?? throw new InvalidOperationException("Connection string 'MySiteContext' not found.")));

builder.Services.AddDbContext<MySiteDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySiteContext") ??   throw new InvalidOperationException("Connection string 'MySiteContext' not found.")));


builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MySiteContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "uk" }; 
    options.SetDefaultCulture("uk")
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);

});

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});


builder.Services.AddRazorPages();
var app = builder.Build();
app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "catalog",
    pattern: "Catalog/{action=Index}/{id?}",
    defaults: new { controller = "Catalog" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.MapRazorPages();

using (var scope = app.Services.CreateScope()) //������ ���������� ���
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

}

using (var scope = app.Services.CreateScope()) // ������ ���������� �������������
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "timofegu.sumka@gmail.com";
    string name = "������������";
    string password = "Aa123123";

    if (!string.IsNullOrEmpty(email) && await userManager.FindByEmailAsync(email) == null)
    {
        var user = new User
        {
            UserName = email,
            Email = email,
            Name = name
        };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
