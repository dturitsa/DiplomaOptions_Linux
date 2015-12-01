using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaWebSite.Models;
using DiplomaWebSite.Services;
using DiplomaWebSite;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplomaWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext _applicationDbContext { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }

        [FromServices]
        public ILogger<OptionsController> Logger { get; set; }

        public RolesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager){
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(){
            return View(await _applicationDbContext.Roles.ToListAsync());
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Roles.Add(role);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(role);
        }


        public ActionResult AddUser(){
            var activeRoles = _applicationDbContext.Roles.Select(c => c);
            ViewBag.Roles = new SelectList(activeRoles, "Name", "Name");
            var activeUsers = _applicationDbContext.Users.Select(c => c);
            ViewBag.Users = new SelectList(activeUsers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(string Id, string RoleName){
            ApplicationUser user = _applicationDbContext.Users.Where(u => u.Id.Equals(Id)).FirstOrDefault();
            await _userManager.AddToRoleAsync(user, RoleName);
            return RedirectToAction("Index");
        }


        public ActionResult DeleteUser(){
            var activeRoles = _applicationDbContext.Roles.Select(c => c);
            ViewBag.Roles = new SelectList(activeRoles, "Name", "Name");
            var activeUsers = _applicationDbContext.Users.Select(c => c);
            ViewBag.Users = new SelectList(activeUsers, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string Id, string RoleName){
            if (Id == null || RoleName == null)
            {
                Logger.LogInformation("Delete: Item not found");
                return HttpNotFound();
            }
            ApplicationUser user = _userManager.Users.Where(c => c.Id == Id).First();
            if(await _userManager.IsInRoleAsync(user, RoleName)){
                await _userManager.RemoveFromRoleAsync(user, RoleName);
            }
            return RedirectToAction("Index");
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                Logger.LogInformation("Delete: Item not found");
                return HttpNotFound();
            }
            IdentityRole role = _applicationDbContext.Roles.Where(c => c.Id == id).First();
            if (role == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Choices/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Console.WriteLine("ID is: " + id + "@@@@@@@@@@@@@@@@@@@@@@@@@");
            IdentityRole role = _applicationDbContext.Roles.Where(c => c.Id == id).FirstOrDefault();
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            _applicationDbContext.Roles.Remove(role);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
