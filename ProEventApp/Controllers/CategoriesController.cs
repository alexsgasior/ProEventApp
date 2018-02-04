using ProEventApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProEventApp.Controllers
{
    public class CategoriesController : Controller
    {

        private ApplicationDbContext _context;

        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (category.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                var categoryInDb = _context.Categories.Single(p => p.Id == category.Id);

                categoryInDb.Name = category.Name;
                categoryInDb.Description = category.Description;
                categoryInDb.Professions = category.Professions;


            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Categories");
        }



        public ActionResult New()
        {
            var categories = _context.Categories.ToList();
            //var viewModel = new ProfessionFormViewModel
            //{
            //    Categories = categories
            //};


            return View("CategoriesForm");
        }


        [Route("Categories/Edit/id")]
        public ActionResult Edit(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }


            return View("CategoriesForm", category);
        }



        public ActionResult Delete(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Categories");

        }




        // GET: Categories
        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}