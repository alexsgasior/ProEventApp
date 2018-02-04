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
        public ActionResult Save(Profession profession)
        {
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


        //public ActionResult Details(int id)
        //{
        //    var pro = _context.Professions.SingleOrDefault(p => p.Id == id);
        //    if (pro == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var viewModel = new ProfessionFormViewModel
        //    {
        //        Profession = pro,
        //        Categories = _context.Categories.ToList()
        //    };
        //    return View("Details", viewModel);
        //}




        [Route("Professions/Index")]
        public ActionResult Index()
        {
            var professions = _context.Professions.Include(p => p.Category).ToList().OrderBy(m=>m.Category.Name);
            return View(professions);
        }


    }
}