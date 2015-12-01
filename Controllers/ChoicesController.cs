﻿﻿﻿﻿using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiplomaWebSite.Models;

namespace DiplomaWebSite.Controllers
{
    
    public class ChoicesController : Controller
    {
        private OptionsContext _context { get; set; }

        [FromServices]
        public ILogger<ChoicesController> Logger { get; set; }

        public ChoicesController(OptionsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var yearTerms = from yt in _context.YearTerms
                            select new
                            {
                                Id = yt.YearTermId,
                                Name = yt.year + " / " + yt.term
                            };

            ViewBag.yearTerms = new SelectList(yearTerms, "Id", "Name");
            
            return View(_context.Choices.ToList());
        }

                // GET: Choices/Create
        [Authorize(Roles = "Student")]
        public ActionResult Create()
        {
            //Choice currentUser = new Choice();
            ViewBag.curentUserId = User.GetUserName();
            ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
            ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
            ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
            ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");

           // return View(currentUser);
            return View();
        }

        
        // POST: Choices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Create(Choice choice)
        {
            var defaultTermId = _context.YearTerms.Where(c => c.isDefault == true).First().YearTermId;
            var checkStudentId = _context.Choices.Where(c => c.StudentId == choice.StudentId);

            if (checkStudentId.Where(c => c.YearTermId == defaultTermId).Count() != 0)
            {
                ViewBag.curentUserId = User.GetUserName();
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                    
                    
                ModelState.AddModelError(string.Empty, "You have already registered for this term");

                return View();
            }
            if (choice.FirstChoiceOptionId == choice.SecondChoiceOptionId
               || choice.FirstChoiceOptionId == choice.ThirdChoiceOptionId
               || choice.FirstChoiceOptionId == choice.FourthChoiceOptionId
               || choice.SecondChoiceOptionId == choice.ThirdChoiceOptionId
               || choice.SecondChoiceOptionId == choice.FourthChoiceOptionId
               || choice.ThirdChoiceOptionId == choice.FourthChoiceOptionId)
            {
                ViewBag.curentUserId = User.GetUserName();
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ModelState.AddModelError(string.Empty, "Cannot have duplicate selections!");

                return View();
            }
            choice.YearTermId = defaultTermId;
            choice.SelectionDate = DateTime.Now;
            _context.Choices.Add(choice);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        
        public async Task<ActionResult> Edit(int id)
        {
            Choice choice = await FindChoiceAsync(id);
            if (choice == null)
            {
                Logger.LogInformation("Edit: Item not found {0}", id);
                return HttpNotFound();
            }
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
          return View(choice);
        }

   // POST: Choices/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id, Choice choice)
        {
            if (_context.YearTerms.Where(c => c.isDefault).Count() != 0)
            {
                choice.YearTermId = _context.YearTerms.Where(c => c.isDefault == true).First().YearTermId;
            }
            try
            {
                if (choice.FirstChoiceOptionId == choice.SecondChoiceOptionId
                       || choice.FirstChoiceOptionId == choice.ThirdChoiceOptionId
                       || choice.FirstChoiceOptionId == choice.FourthChoiceOptionId
                       || choice.SecondChoiceOptionId == choice.ThirdChoiceOptionId
                       || choice.SecondChoiceOptionId == choice.FourthChoiceOptionId
                       || choice.ThirdChoiceOptionId == choice.FourthChoiceOptionId)
                {
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                    ModelState.AddModelError(string.Empty, "Cannot select duplicate options!");

                    return View(choice);
                }
                var studentId = _context.Choices.Where(c => c.ChoiceId == id).Select(c => c.StudentId).FirstOrDefault();
                choice.ChoiceId = id;
                choice.StudentId = studentId;
                choice.SelectionDate = DateTime.Now;
                _context.Choices.Attach(choice);
                _context.Entry(choice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
                Console.WriteLine(ex);
                
            }
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.isActive == true), "OptionId", "title");
            return View(choice);
        }

        private Task<Choice> FindChoiceAsync(int id)
        {
            return _context.Choices.SingleOrDefaultAsync(s => s.ChoiceId == id);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Choice choice = await FindChoiceAsync(id);
            if (choice == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return HttpNotFound();
            }
            ViewBag.Retry = retry ?? false;
            return View(choice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Choice choice = await FindChoiceAsync(id);
                _context.Choices.Remove(choice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, retry = true });
            }
            return RedirectToAction("Index");
        }
        
       [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetChoices(int? yeartermid)
        {
   if(yeartermid == null)
            {
                yeartermid = _context.YearTerms.Where(y => y.isDefault == true).Select(yt => yt.YearTermId).FirstOrDefault();   
            }
              
            var currentTerm = (from yt in _context.YearTerms
                               where yt.YearTermId == yeartermid
                               select new
                               {
                                   Name = yt.year + " / " + yt.term
                               }).FirstOrDefault();
           // ViewBag.currentTerm = currentTerm.Name;
           ViewBag.currentTerm = "Selected Term: " + currentTerm.Name;

              var choices = _context
                    .Choices
                    .Where(c => c.YearTermId == yeartermid);
           // ViewBag.Options = getOptionsData(yeartermid);

     

        return PartialView("_IndexPartial", choices);   
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetChart(int? yeartermid)
        {
   if(yeartermid == null)
            {
                yeartermid = _context.YearTerms.Where(y => y.isDefault == true).Select(yt => yt.YearTermId).FirstOrDefault();   
            }
              
            var currentTerm = (from yt in _context.YearTerms
                               where yt.YearTermId == yeartermid
                               select new
                               {
                                   Name = yt.year + " / " + yt.term
                               }).FirstOrDefault();
                         
          
           ViewBag.currentTerm = "Selected Term: " + currentTerm.Name;
             ViewBag.items = new string[]{ "datacom", "web and mobile", "tech pro", "Client Server"};
              var choices = _context
                    .Choices
                    .Where(c => c.YearTermId == yeartermid);
             
             
             var firstChoice = new int[4];       
             firstChoice[0] = (from row in _context.Choices
                                      where row.FirstChoiceOptionId == 1
                                         && row.YearTermId == yeartermid
                                      select row).Count();
              firstChoice[1] = (from row in _context.Choices
                                      where row.FirstChoiceOptionId == 2
                                         && row.YearTermId == yeartermid
                                      select row).Count();
             firstChoice[2] = (from row in _context.Choices
                                      where row.FirstChoiceOptionId == 3
                                         && row.YearTermId == yeartermid
                                      select row).Count();
             firstChoice[3] = (from row in _context.Choices
                                      where row.FirstChoiceOptionId == 4
                                         && row.YearTermId == yeartermid
                                      select row).Count();                        
            ViewBag.firstChoice = firstChoice;
        return PartialView("_ChartPartial", choices);   
        }
        
    }
}