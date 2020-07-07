using IdentityServer4;
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
              }
          };

        //public static IEnumerable<ApiResource> GetApiResources()
        //{
        //    return new[]
        //    {
        //        new ApiResource
        //        {
        //            Name = "Cepres.Services.API",
        //            DisplayName = "Cepres Services API #1",
        //            Description = "Allow the application to access Cepres Services API #1 on your behalf",
        //            Scopes = new List<string> {"cepres.services.api"},
        //            ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
        //            UserClaims = new List<string> {"role"}
        //        }
        //    };
        //}

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("cepres.services.api", "Read/Write Access to Cepres Services API #1")
            };
        }
    }
}
