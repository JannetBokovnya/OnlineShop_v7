// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using OnlineShop.Library.Constants;
using System.Collections.Generic;

namespace OnlineShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(IdConstants.ApiScope), //взаимодействие услуги между собой, что бы услуга а могла связаться с услугой в должна получить токен
                new ApiScope(IdConstants.WebScope),  //будут заходить пользователи через web  интерфейс
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
               // клиент для взаимодействия услуг между собой
                new Client
                {
                    ClientId = "test.client",
                    ClientName = "Test client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdConstants.WebScope,
                        IdConstants.ApiScope
                    }
                },

                 // внешний клиент, который будет заходить в приложение при помощи логина и пароля
                new Client
                {
                    ClientId = "external",
                    ClientName="External client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                     AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         IdConstants.WebScope
                    }

                },

                //new Client
                //{
                //    ClientId = "movieClient",
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,
                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },
                //    AllowedScopes={ "OnlineShop.Web" }
                //},
            };
    }
}