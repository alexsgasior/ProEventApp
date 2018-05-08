using ProEventApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProEventApp.ViewModels;

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
        public ActionResult Save(ProAdvertisement ad)  // dodaj TEN SAM obiekt do tabelki asocjacyjnej i bedzie git :D
        {
            Guid guid = Guid.NewGuid();
            string strGuid = guid.ToString();

            ad.Advertisement.FindId = strGuid;

            Advertisement justAd = ad.Advertisement;

            //if (!ModelState.IsValid)
            //{
            //    return View("AdvertisementForm");
            //}
            if (justAd.Id == 0)
            {
                _context.Advertisements.Add(justAd);
            }
            else
            {
                var adInDb = _context.Advertisements.Single(p => p.Id == ad.AdvertisementId);
                adInDb.Title = ad.Advertisement.Title;
                adInDb.AdText = ad.Advertisement.AdText;
            }
            _context.SaveChanges();
            
            var currentLoggedIdPro = User.Identity.GetUserId();
            var proLinkedtoLogged = _context.Professionals.Include(x => x.Profession).Include(x => x.Profession.Category).SingleOrDefault(m => m.CurrentUserId == currentLoggedIdPro);
            int idToQueryPro = proLinkedtoLogged.Id;
            var add = _context.Advertisements.SingleOrDefault(x => x.FindId == strGuid);

            var proAd = new ProAdvertisement
            {
                ProfessionalId = idToQueryPro,
                Professional = proLinkedtoLogged,
                Advertisement =add,
                AdvertisementId = add.Id
            };
            _context.ProAdvertisements.Add(proAd);
            _context.SaveChanges();
            return RedirectToAction("Index", "ProAdvertisements");
        }

        [Authorize(Roles = RoleName.Admin+" "+RoleName.Professional)]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole(RoleName.Professional))
            {
                var currentLoggedIdPro = User.Identity.GetUserId();
                var proLinkedtoLogged = _context.Professionals.Include(x => x.Profession).Include(x => x.Profession.Category).SingleOrDefault(m => m.CurrentUserId == currentLoggedIdPro);
                int idToQueryPro = proLinkedtoLogged.Id;

                var padd = _context.ProAdvertisements.Where(p => p.Id == id).Where(x=>x.ProfessionalId == idToQueryPro);
                if (padd == null)
                {
                    return HttpNotFound();
                }
                var addToDelete = padd.SingleOrDefault(x => x.AdvertisementId == id);
                _context.ProAdvertisements.Remove(addToDelete);
                _context.SaveChanges();
            }
            else
            {
                var add = _context.ProAdvertisements.SingleOrDefault(p => p.Id == id);
                if (add == null)
                {
                    return HttpNotFound();
                }
                _context.ProAdvertisements.Remove(add);
                _context.SaveChanges();
                
            }
            return RedirectToAction("Index", "ProAdvertisements");
        }


        // GET: ProAdvertisements
        public ActionResult Index()
        {

            var allAds = _context.ProAdvertisements.Include(x=>x.Advertisement)
                        .Include(x=>x.Professional).ToList();


            if (User.IsInRole(RoleName.Professional))
            {
                var currentLoggedIdPro = User.Identity.GetUserId();
                var proLinkedtoLogged = _context.Professionals.Include(x => x.Profession).Include(x => x.Profession.Category).SingleOrDefault(m => m.CurrentUserId == currentLoggedIdPro);
                int idToQueryPro = proLinkedtoLogged.Id;
                


                return View("IndexPro", allAds);
            }
            else if (User.IsInRole(RoleName.Admin))
            {
                return View("IndexAdmin", allAds);
            }
            else
            {
                return View("IndexPublic", allAds);
            }
        }



    }
}