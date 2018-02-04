using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProEventApp.Models;

namespace ProEventApp.Controllers
{
    public class EventImagesController : Controller
    {

        private ApplicationDbContext _context;

        public EventImagesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        public ActionResult Save(EventImage eventImage)
        {
            if (eventImage.Id == 0)
            {
                _context.EventImages.Add(eventImage);
            }
            else
            {
                var evImInDb = _context.EventImages.Single(p => p.Id == eventImage.Id);

                //categoryInDb.Name = category.Name;
                evImInDb.Description = eventImage.Description;
                evImInDb.AppEventId = eventImage.AppEventId;
                evImInDb.AppEvent = eventImage.AppEvent;
                evImInDb.ImageId = eventImage.ImageId;
                evImInDb.Image = eventImage.Image;
            }

            _context.SaveChanges();


            return RedirectToAction("New", "AppEvents");
        }



        public ActionResult New()
        {
            var imagesList = _context.Images.ToList();
            var appevents = _context.Events.ToList();
            
            //var viewModel = new ProfessionFormViewModel
            //{
            //    Categories = categories
            //};


            return View("EventImagesForm");
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



        // GET: EventImages
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}