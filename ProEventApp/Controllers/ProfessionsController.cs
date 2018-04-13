using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProEventApp.Models;
using ProEventApp.ViewModels;

namespace ProEventApp.Controllers
{
    public class ProfessionsController : Controller
    {
        private ApplicationDbContext _context;

        public ProfessionsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Professional)]
        public ActionResult Save(Profession profession)
        {
            if (!ModelState.IsValid)
            {
                var categories = _context.Categories.ToList();
                var viewModel = new ProfessionFormViewModel
                {
                    Categories = categories
                };


                return View("ProfessionForm", viewModel);
            }


            if (profession.Id == 0)
            {
                _context.Professions.Add(profession);
            }
            else
            {
                var professionInDb = _context.Professions.Single(p => p.Id == profession.Id);

                professionInDb.Name = profession.Name;
                professionInDb.Category = profession.Category;
                professionInDb.Description = profession.Description;
                professionInDb.CategoryId = profession.CategoryId;
                professionInDb.Professionals = profession.Professionals;

            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Professions");
        }




        [Authorize(Roles = RoleName.Admin + "," + RoleName.Professional)]
        public ActionResult New()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new ProfessionFormViewModel
            {
                Categories = categories
            };


            return View("ProfessionForm", viewModel);
        }



        [Route("Professions/Edit/id")]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Professional)]
        public ActionResult Edit(int id)
        {
            var profession = _context.Professions.SingleOrDefault(p => p.Id == id);
            if (profession == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProfessionFormViewModel
            {
                Profession = profession,
                Categories = _context.Categories.ToList()
            };
            return View("ProfessionForm", viewModel);
        }


        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            var profession = _context.Professions.SingleOrDefault(p => p.Id == id);
            if (profession == null)
            {
                return HttpNotFound();
            }
            _context.Professions.Remove(profession);
            _context.SaveChanges();
            return RedirectToAction("Index", "Professions");
        }




        [Route("Professions/Index")]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.Professional+","+RoleName.AppUser)]
        public ActionResult Index()
        {
            var professions = _context.Professions.Include(p => p.Category).ToList().OrderBy(m=>m.Category.Name);

            if (User.IsInRole(RoleName.AppUser))
            {
                return View("IndexList", professions);
            }

            return View("Index",professions);
        }


    }
}