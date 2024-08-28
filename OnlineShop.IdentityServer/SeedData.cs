// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.IdentityServer.Data;
using OnlineShop.Library.Common.Models;
using OnlineShop.Library.Data;
using OnlineShop.Library.UserManagementService.Models;
using Serilog;
using System;
using System.Linq;
using System.Security.Claims;

namespace OnlineShop.IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<UserDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<UserDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var jannet = userMgr.FindByNameAsync("jannet").Result;
                    if (jannet == null)
                    {
                        jannet = new ApplicationUser
                        {
                            UserName = "jannet",
                            FirstName = "Jannet",
                            LastName = "Bokovnya",
                            Email = "jannet@email.com",
                            EmailConfirmed = true,
                            DefaultAddress = new Address()
                            {
                                City = "Warsaw",
                                Country = "Poland",
                                PostalCode = "00-001",
                                AddressLine1 = "Jasna 21",
                                AddressLine2 = "34"
                            },
                            DeliveryAddress = new Address()
                            {
                                City = "Kraków",
                                Country = "Poland",
                                PostalCode = "30-001",
                                AddressLine1 = "Wspólna 45"
                            },
                        };
                        var result = userMgr.CreateAsync(jannet, "Pass_123").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(jannet, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Jannet Bokovnya"),
                            new Claim(JwtClaimTypes.GivenName, "Jannet"),
                            new Claim(JwtClaimTypes.FamilyName, "Bokovnya"),
                            new Claim(JwtClaimTypes.WebSite, "https://github.com/JannetBokovnya/"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Jannet has been created");
                    }
                    else
                    {
                        Log.Debug("Jannet already exists");

                        if (jannet.DefaultAddress == null)
                        {
                            jannet.DefaultAddress = new Address()
                            {
                                City = "Warsaw",
                                Country = "Poland",
                                PostalCode = "00-001",
                                AddressLine1 = "Jasna 21",
                                AddressLine2 = "34"
                            };
                        }

                        if (jannet.DeliveryAddress == null)
                        {
                            jannet.DeliveryAddress = new Address()
                            {
                                City = "Kraków",
                                Country = "Poland",
                                PostalCode = "30-001",
                                AddressLine1 = "Wspólna 45"
                            };
                        }

                        var result = userMgr.UpdateAsync(jannet).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("Jannet has been updated");
                    }
                }
            }
        }
    }
}
