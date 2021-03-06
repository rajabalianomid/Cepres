﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cepres.IdentityServer.Config
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
          };

        public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "test",
                  Password = "test"
              }
          };

        public static IEnumerable<Client> GetClients() =>
          new List<Client>
          {
              new Client
              {
                  ClientId = "cepres.services.api",
                  ClientSecrets = new List<Secret> {new Secret("cepres".Sha256())}, // change me!   
                  AllowedGrantTypes = GrantTypes.ClientCredentials,
                  AllowedScopes = {"cepres.services.api" }
              },
              new Client
                {
                    ClientId = "spa",
                    ClientName = "React Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:3000/callback" },
                    PostLogoutRedirectUris = { "http://localhost:3000/" },
                    AllowedCorsOrigins =     { "http://localhost:3000" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "cepres.services.api"
                    }
                }
          };

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("cepres.services.api", "Read/Write Access to Cepres Services API #1")
            };
        }
    }
}
