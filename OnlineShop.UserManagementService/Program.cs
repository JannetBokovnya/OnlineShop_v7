
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using OnlineShop.Library.Constants;
using OnlineShop.Library.Data;
using OnlineShop.Library.UserManagementService.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//
//public void ConfigureServices(IServiceCollection services)
//end

builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionNames.UserConnection)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
   .AddEntityFrameworkStores<UserDbContext>()
   .AddDefaultTokenProviders();

builder.Services.AddAuthentication(
    IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "https://localhost:5001";
        //options.ApiName = "https://localhost:5001/resources";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", IdConstants.ApiScope);
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers().RequireAuthorization("ApiScope");
//});
app.MapControllers().RequireAuthorization("ApiScope");

app.Run();
