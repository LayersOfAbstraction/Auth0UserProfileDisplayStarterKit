using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auth0UserProfileDisplayStarterKit.Support;
using Example.Auth0.AuthenticationApi.AccessTokenManagement;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TeamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//To use MVC we have to explicitly declare we are using it. Doing so will prevent a System.InvalidOperationException.
builder.Services.AddControllersWithViews();
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// Configure the HTTP request pipeline.
builder.Services.ConfigureSameSiteNoneCookies();

// Add the Auth0 HttpClientManagementConnection.
builder.Services.AddSingleton<IManagementConnection, HttpClientManagementConnection>();
// Add JWT renewal references  
builder.Services.AddAccessTokenManagement(builder.Configuration);
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();