using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2SG.Repositories;
using PracticaMvcCore2SG.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthentication(options => {
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

//builder.Services.AddAuthorization(options => {
//    options.AddPolicy("NUESTROSPERMISOS",
//        policy => policy.RequireRole("admin")
//    );
//});



// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("SqlLibros");
builder.Services.AddTransient<RepositoryLibros>();

builder.Services.AddDbContext<LibrosContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();

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
app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMvc(routes => {
    routes.MapRoute(
        name: "default",
        //template: "{controller=Store}/{action=Index}/{id?}");
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
