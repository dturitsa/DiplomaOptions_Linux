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
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplomaWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext _applicationDbContext { get; set; }

        [FromServices]
        public ILogger<OptionsController> Logger { get; set; }

        public RolesController(ApplicationDbContext applicationDbContext){
            _applicationDbContext = applicationDbContext;
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
    }
}
