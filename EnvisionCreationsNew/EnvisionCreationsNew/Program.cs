using EnvisionCreationsNew.Data;
using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Services.Contracts;
using EnvisionCreationsNew.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EnvisionCreationsNew.Repositories.Contracts;
using EnvisionCreationsNew.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
});

builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IDownloadService, DownloadService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
