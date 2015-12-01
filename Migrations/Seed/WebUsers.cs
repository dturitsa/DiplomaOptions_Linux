using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using DiplomaWebSite.Models;
using DiplomaWebSite.Services;
using DiplomaWebSite.ViewModels.Account;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplomaWebSite.Migrations.Seed
{
    public class WebUsers
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public WebUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeDataAsync()
        {
            await CreateUsersAsync();
        }

        private async Task CreateUsersAsync()
        {
            var admin = await _userManager.FindByEmailAsync("a@a.a");
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = "A00111111", Email = "a@a.a" };
                await _userManager.CreateAsync(admin, "P@$$w0rd");
            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            await _userManager.AddToRoleAsync(admin, "Admin");

            var student = await _userManager.FindByEmailAsync("b@b.b");
            if (student == null)
            {
                student = new ApplicationUser { UserName = "A00222222", Email = "b@b.b" };
                await _userManager.CreateAsync(student, "P@$$w0rd");
            }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }
            await _userManager.AddToRoleAsync(student, "Student");
        }

    }
}
