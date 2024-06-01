using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with ApplicationUser
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Điều chỉnh theo nhu cầu của bạn
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

// Configure cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Dependency Injection for repositories
builder.Services.AddScoped<IClassRepository, EFClassRepository>();
builder.Services.AddScoped<ILoanTicketRepository, EFLoanTicketRepository>();
builder.Services.AddScoped<ILoanEquipmentRepository, EFLoanEquipmentRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IRoomCategoriesRepository, EFRoomCategoriesRepository>();
builder.Services.AddScoped<IIssueCategoryRepository, EFIssueCategoryRepository>();
builder.Services.AddScoped<IIssueDetailRepository, EFIssueDetailRepository>();


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
    endpoints.MapControllerRoute(name: "Admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
