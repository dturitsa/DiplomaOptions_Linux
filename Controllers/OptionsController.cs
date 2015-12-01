﻿﻿﻿using Microsoft.AspNet.Mvc;
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
    public class OptionsController : Controller
    {
        private OptionsContext _context { get; set; }

        [FromServices]
        public ILogger<OptionsController> Logger { get; set; }

        public OptionsController(OptionsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Options.ToList());
        }

        public ActionResult Create()
        {
       //     ViewBag.Items = GetOptionsListItems();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Option option)
        {
            if (ModelState.IsValid)
            {
                _context.Options.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(option);
        }

        public ActionResult Details(int id)
        {
            Option option = _context.Options
                .Where(b => b.OptionId == id)
                .FirstOrDefault();
            if (option == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);
                return HttpNotFound();
            }
            return View(option);
        }
/*
        private IEnumerable<SelectListItem> GetOptionsListItems(int selected = -1)
        {
            var tmp = _context.Options.ToList();

            // Create authors list for <select> dropdown
            return tmp
                .OrderBy(s => s.title)
                .Select(s => new SelectListItem
                {
                    Text = String.Format("{0}, {1}", s.OptionId, s.title),
                    Value = s.OptionId.ToString(),
                    Selected = s.OptionId == selected
                });
        }
    */    

        public async Task<ActionResult> Edit(int id)
        {
            Option option = await FindOptionAsync(id);
            if (option == null)
            {
                Logger.LogInformation("Edit: Item not found {0}", id);
                return HttpNotFound();
            }

         //   ViewBag.Items = GetOptionsListItems(Option.OptionId);
            return View(option);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Option option)
        {
            try
            {
                option.OptionId = id;
                _context.Options.Attach(option);
                _context.Entry(option).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            return View(option);
        }

        private Task<Option> FindOptionAsync(int id)
        {
            return _context.Options.SingleOrDefaultAsync(s => s.OptionId == id);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Option option = await FindOptionAsync(id);
            if (option == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return HttpNotFound();
            }
            ViewBag.Retry = retry ?? false;
            return View(option);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Option option = await FindOptionAsync(id);
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, retry = true });
            }
            return RedirectToAction("Index");
        }
    }
}
