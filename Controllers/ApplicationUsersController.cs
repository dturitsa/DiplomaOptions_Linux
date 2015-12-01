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
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<ApplicationUser> userManager { get; set; }

        [FromServices]
        public ILogger<OptionsController> Logger { get; set; }

        public ApplicationUsersController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> uman){
            db = applicationDbContext;
            userManager = uman;
        }
        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ApplicationUser applicationUser = db.Users.Where( c => c.Id == id).FirstOrDefault();
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ApplicationUser applicationUser = db.Users.Where( c => c.Id == id).FirstOrDefault();
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            ApplicationUser editUser = db.Users.Where( c => c.Id == applicationUser.Id).FirstOrDefault();
            editUser.Email = applicationUser.Email;
            editUser.LockoutEnabled = applicationUser.LockoutEnabled;
            editUser.UserName = applicationUser.UserName;
            //Sets login count to enable the lockout
            if (editUser.LockoutEnabled)
            {
                editUser.AccessFailedCount = 5000;
                editUser.LockoutEnd = DateTime.UtcNow.AddYears(777);

            }
            else
            {
                editUser.AccessFailedCount = 0;
                editUser.LockoutEnd = null;
            }
            if (ModelState.IsValid)
            {
                db.Entry(editUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
