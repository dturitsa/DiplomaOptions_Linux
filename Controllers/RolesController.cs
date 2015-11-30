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
        
        public IActionResult Index(){
            return View(_applicationDbContext.Roles.ToList());
        }
    }
}
