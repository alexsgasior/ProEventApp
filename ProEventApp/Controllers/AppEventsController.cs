using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProEventApp.Models;
using ProEventApp.ViewModels;

namespace ProEventApp.Controllers
{
    public class AppEventsController : Controller
    {
        private ApplicationDbContext _context;

        public AppEventsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        


        public ActionResult AddImg(int id)
        {
            Image img = new Image();
            TempData["ID"] = id;
            return View(img);
        }

        public ActionResult AddImgg(/*AppEvent _appEvent*/ Image modelImage, HttpPostedFileBase image1, int appEventId=0)
        {

            appEventId = Convert.ToInt32(TempData["ID"]);

            if (image1 != null)
            {
                modelImage.Bytes = new byte[image1.ContentLength];
                modelImage.ContentBase64 = /*_appEvent.Id*/appEventId.ToString() + DateTime.Today.DayOfYear.ToString();
                image1.InputStream.Read(modelImage.Bytes, 0, image1.ContentLength);

            }
            _context.Images.Add(modelImage);
            _context.SaveChanges();


            var imagetoAdd = _context.Images.SingleOrDefault(m =>
                m.ContentBase64 == /*_appEvent.Id*/appEventId.ToString() + DateTime.Today.DayOfYear.ToString());

            var _appEvent = _context.Events.SingleOrDefault(m => m.Id == appEventId);


            var _eventImage = new EventImage()
            {
                AppEvent = _appEvent,
                AppEventId = /*_appEvent.Id*/appEventId,
                Image = imagetoAdd,
                ImageId = imagetoAdd.Id
            };

            _context.EventImages.Add(_eventImage);


            _context.SaveChanges();
            return RedirectToAction("Index", "AppEvents");


        }


        [HttpPost]
        public ActionResult Save(AppEventFormViewModel model)
        {
            AppEvent _event = model.AppEvent;

            var userIdToPass = User.Identity.GetUserId();

            var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            AppUser appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);

            _event.AppUser = appUserToPass;
            _event.AppUserId = appUserToPass.Id;


            if (_event.Id == 0)
            {
                _context.Events.Add(_event);
            }
            else
            {
                var eventInDb = _context.Events.Single(m => m.Id == _event.Id);

                eventInDb.Name = _event.Name;
                eventInDb.Description = _event.Description;
                eventInDb.StatusId = _event.StatusId;
                //eventInDb.Status = _event.Status;
                eventInDb.CityId = _event.CityId;
                eventInDb.Street = _event.Street;
                eventInDb.HouseNumber = _event.HouseNumber;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "AppEvents");

        }

        public ActionResult New()
        {
            var userIdToPass = User.Identity.GetUserId();

            var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            AppUser appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);

            var statuses = _context.Statuses.ToList();
            var cities = _context.Cities.ToList();
            //var eventImages = _context.EventImages.ToList();
            var categories = _context.Categories.ToList();
            var viewModel = new AppEventFormViewModel
            {
                Statuses = statuses,
                Cities = cities,
                //EventImages = eventImages,
                Categories = categories,
                AppUser = appUserToPass
            };


            return View("AppEventForm", viewModel);
        }

        [Route("AppEvents/Edit/id")]
        public ActionResult Edit(int id)
        {
            var _event = _context.Events.SingleOrDefault(m => m.Id == id);
            if (_event == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AppEventFormViewModel()
            {
                //Profession = profession,
                //Categories = _context.Categories.ToList()
                AppEvent = _event,
                Statuses = _context.Statuses.ToList(),
                Cities = _context.Cities.ToList(),
                Categories = _context.Categories.ToList()
                //EventImages = _context.EventImages.ToList()
            };
            return View("AppEventForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var _event = _context.Events.SingleOrDefault(m => m.Id == id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            _context.Events.Remove(_event);
            _context.SaveChanges();
            return RedirectToAction("Index", "AppEvents");
        }


        //[Route("AppEvents/Details/id")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var _event = _context.Events
                         .Include(m => m.Status).Include(m => m.Category)
                         .Include(m => m.AppUser).Include(m => m.City)
                         .SingleOrDefault(m => m.Id == id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            var _eventImages = _context.EventImages.Include(m=>m.Image).Where(m=>m.AppEventId==id).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages
            };

            return View("AppEventDetailsForm",viewModel);

        }

        public ActionResult FindProsByCategory(int id)
        {
            var pros = _context.Professionals.Include(m=>m.Profession).Where(m => m.Profession.CategoryId == id).ToList();

            return View("ProsByCategory", pros);

        }




        public ActionResult Index()
        {
            var currentLoggedId = User.Identity.GetUserId();
            var userLinkedtoLogged = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentLoggedId);
            int idToQuery = userLinkedtoLogged.Id;

            var _events = _context.Events.Include(m => m.Status).Include(m=>m.Category).Include(m => m.AppUser).Include(m=>m.City).ToList().OrderBy(m => m.Status.Name).Where(m=>m.AppUserId==idToQuery);

            return View(_events);
        }

    }
}