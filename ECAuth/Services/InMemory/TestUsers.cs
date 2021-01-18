using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ECAuth.Domain;
using IdentityModel;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ECAuth.Services.InMemory
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
             new TestUser{SubjectId = "1", Username = "admin", Password = "admin",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "SuperAdministrador"),
                        new Claim(JwtClaimTypes.GivenName, "admin"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "admin@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Administrador"),
                        new Claim(JwtClaimTypes.Role, "GestorParteHoras"),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Id, "1")
                    }
                },
             new TestUser{SubjectId = "84", Username = "mgarciag", Password = "mgarciag",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "María García"),
                        new Claim(JwtClaimTypes.GivenName, "mgarciag"),
                        new Claim(JwtClaimTypes.FamilyName, "mgarciag"),
                        new Claim(JwtClaimTypes.Email, "mgarciag@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Id, "84")
                    }
                },
             new TestUser{SubjectId = "14", Username = "acarbonell", Password = "acarbonell",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "Alberto Carbonell"),
                        new Claim(JwtClaimTypes.GivenName, "acarbonell"),
                        new Claim(JwtClaimTypes.FamilyName, "acarbonell"),
                        new Claim(JwtClaimTypes.Email, "acarbonell@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Id, "14")
                    }
                },
             new TestUser{SubjectId = "67", Username = "aguardiola", Password = "aguardiola",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "Alberto Guardiola"),
                        new Claim(JwtClaimTypes.GivenName, "aguardiola"),
                        new Claim(JwtClaimTypes.FamilyName, "aguardiola"),
                        new Claim(JwtClaimTypes.Email, "aguardiola@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Role, "GestorParteHoras"),
                        new Claim(JwtClaimTypes.Role, "GestorProyecto"),
                        new Claim(JwtClaimTypes.Role, "GestorAusencias"),
                        new Claim(JwtClaimTypes.Role, "GestorEmpleado"),
                        new Claim(JwtClaimTypes.Id, "67")
                    }
                },
             new TestUser{SubjectId = "86", Username = "aperezr", Password = "aperezr",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "Alvaro Pérez"),
                        new Claim(JwtClaimTypes.GivenName, "aperezr"),
                        new Claim(JwtClaimTypes.FamilyName, "aperezr"),
                        new Claim(JwtClaimTypes.Email, "aperezr@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Role, "GestorProyecto"),
                        new Claim(JwtClaimTypes.Id, "86")
                    }
                },
             new TestUser{SubjectId = "71", Username = "mjhernan", Password = "mjhernan",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "Mari Jose Hernán"),
                        new Claim(JwtClaimTypes.GivenName, "mjhernan"),
                        new Claim(JwtClaimTypes.FamilyName, "mjhernan"),
                        new Claim(JwtClaimTypes.Email, "mjhernan@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Role, "GestorParteHoras"),
                        new Claim(JwtClaimTypes.Role, "GestorEmpleado"),
                        new Claim(JwtClaimTypes.Id, "71")
                    }
                },
             new TestUser{SubjectId = "66", Username = "yllarena", Password = "yllarena",
                    Claims = new[]
                    {
                        new Claim(JwtClaimTypes.Name, "Yaiza LLarena"),
                        new Claim(JwtClaimTypes.GivenName, "yllarena"),
                        new Claim(JwtClaimTypes.FamilyName, "yllarena"),
                        new Claim(JwtClaimTypes.Email, "yllarena@test.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Usuario"),
                        new Claim(JwtClaimTypes.Role, "GestorAusencias"),
                        new Claim(JwtClaimTypes.Role, "GestorEmpleado"),
                        new Claim(JwtClaimTypes.Id, "66")
                    }
                }

        };
    }

    public static class TestUserExtensions
    {
        public static ApplicationUser ToApplicationUser(this TestUser user, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            var appUser = new ApplicationUser
            {
                UserName = user.Username,
                NormalizedUserName = user.Username.ToUpper(),
                Email = user.Claims.FirstOrDefault(c => c.Type.Equals(JwtClaimTypes.Email))?.Value,
                NormalizedEmail = user.Claims.FirstOrDefault(c => c.Type.Equals(JwtClaimTypes.Email))?.Value.ToUpper(),
                Id = user.SubjectId,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            appUser.PasswordHash = passwordHasher.HashPassword(appUser, user.Password);

            return appUser;
        }
    }
}