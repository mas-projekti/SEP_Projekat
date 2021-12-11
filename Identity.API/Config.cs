using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
          new Client[]
          {
             new Client
             {
                 ClientId = "klijentneki",
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 ClientSecrets =
                 {
                 new Secret("tajnovitatajna".Sha256())
                 },
                AllowedScopes = { "paypal-api", "bitcoin-api" }
             },
             new Client
             {
                 ClientId = "jasna_skomrak",
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 ClientSecrets =
                 {
                 new Secret("tajnavinoveloze".Sha256())
                 },
                AllowedScopes = { "bitcoin-api" }
             },
             new Client
                   {
                       ClientId = "klijent_neki_drugi",
                       ClientName = "Amazon",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "bitcoin-api",
                           "roles"
                       }
                   }
          };
        public static IEnumerable<ApiScope> ApiScopes =>
          new ApiScope[]
          {
            new ApiScope("paypal-api", "Paypal API"),
            new ApiScope("bitcoin-api", "Bitcoin API")
          };
        public static IEnumerable<IdentityResource> IdentityResources =>
         new IdentityResource[]
         {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
         };
        public static List<TestUser> TestUsers =>
           new List<TestUser>
           {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "dzoni",
                    Password = "makaroni",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Nijona"),
                        new Claim(JwtClaimTypes.FamilyName, "Mikolic")
                    }
                }
           };


    }
}
