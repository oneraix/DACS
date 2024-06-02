using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Điều chỉnh theo nhu cầu của bạn
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

// Configure cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register UserManager and SignInManager
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();

// Dependency Injection for repositories
builder.Services.AddScoped<IClassRepository, EFClassRepository>();
builder.Services.AddScoped<ILoanTicketRepository, EFLoanTicketRepository>();
builder.Services.AddScoped<ILoanEquipmentRepository, EFLoanEquipmentRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IRoomCategoriesRepository, EFRoomCategoriesRepository>();
builder.Services.AddScoped<IIssueCategoryRepository, EFIssueCategoryRepository>();
builder.Services.AddScoped<IIssueDetailRepository, EFIssueDetailRepository>();
builder.Services.AddScoped<ISupportRequestRepository, EFSupportRequestRepository>();
builder.Services.AddScoped<IRoomScheduleRepository, EFRoomScheduleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ensure the authentication middleware is added before calling UseAuthorization()
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Configure endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Redirect root URL to login page
    endpoints.MapGet("/", async context =>
    {
        var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        var user = await userManager.GetUserAsync(context.User);
        if (user == null)
        {
            context.Response.Redirect("/Identity/Account/Login");
        }
        else
        {
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains(Role.Role_Admin))
            {
                context.Response.Redirect("/Admin/Home/Index");
            }
            else if (roles.Contains(Role.Role_GV))
            {
                context.Response.Redirect("/GV/Home/Index");
            }
            else if (roles.Contains(Role.Role_KTV))
            {
                context.Response.Redirect("/KTV/Home/Index");
            }
            else if (roles.Contains(Role.Role_QL))
            {
                context.Response.Redirect("/QL/Home/Index");
            }
            else
            {
                context.Response.Redirect("/Home/Index");
            }
        }
    });

    endpoints.MapRazorPages();
});

app.Run();
