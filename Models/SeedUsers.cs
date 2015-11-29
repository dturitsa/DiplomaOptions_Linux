
/*
//trying to use this file to seed users but it's not working
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaWebSite.Models
{
    public static class SeedUsers
    {
        public static void Initialize(ApplicationDbContext context)
        {
           var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            if (!roleManager.RoleExists("Student"))
                roleManager.Create(new IdentityRole("Student"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (userManager.FindByEmail("a@a.a") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "a@a.a",
                    UserName = "A00111111",
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");

                if (userManager.FindByEmail("s@s.s") == null)
                {
                    user = new ApplicationUser
                    {
                        Email = "s@s.s",
                        UserName = "A00222222",
                    };
                    result = userManager.Create(user, "P@$$w0rd");
                    if (result.Succeeded)
                        userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Student");
                }
            }


            }
        }
    }

*/