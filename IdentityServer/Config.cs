﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            }; 


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("MovieApi", "My API")
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // better for call an API from an API
                new Client
                {
                    ClientId = "ClientId_API",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("ClientSecret_API".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "MovieApi" }
                },
                
                // better for call an API from an MVC
                new Client
                {
                    ClientId = "ClientId_MVC",

                    // MVC client using Authorization code flow (code)
                    AllowedGrantTypes = GrantTypes.Code,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("ClientSecret_MVC".Sha256())
                    },
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5003/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },
                    
                    RequireConsent = false,

                    // scopes that client has access to (resources)
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,  
                        IdentityServerConstants.StandardScopes.Profile,  
                        IdentityServerConstants.StandardScopes.Email,  
                        "MovieApi"
                    },
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true
                }
            };
        
    }
}