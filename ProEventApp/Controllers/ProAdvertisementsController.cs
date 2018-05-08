using ProEventApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProEventApp.Controllers
{
    public class ProAdvertisementsController : Controller
    {
        private ApplicationDbContext _context;

        public ProAdvertisementsController()
        {
            _context = new ApplicationDbContext();
        }


        [Authorize(Roles = RoleName.Professional)]
        public ActionResult New()
        {
            
            return View("AdvertisementForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles =RoleName.Professional)]
        public ActionResult Save(Advertisement ad)  // dodaj TEN SAM obiekt do tabelki asocjacyjnej i bedzie git :D
        {
            if (!ModelState.IsValid)
            {
                return View("AdvertisementForm");
            }
            if (ad.Id == 0)
            {
                _context.Advertisements.Add(ad);
            }
            else
            {
                var adInDb = _context.Advertisements.Single(p => p.Id == ad.Id);

                adInDb.Title = ad.Title;
                adInDb.AdText = ad.AdText;
                
            }

            _context.SaveChanges();


            return RedirectToAction("Index", "ProAdvertisements");
        }

        // GET: ProAdvertisements
        public ActionResult Index()
        {
            return View();
        }
    }
}