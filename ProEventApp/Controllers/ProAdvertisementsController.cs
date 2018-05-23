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
        public ActionResult Save(ProAdvertisement ad)  
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

        [Authorize(Roles = RoleName.Professional+","+RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole(RoleName.Professional) || User.IsInRole(RoleName.Admin))
            {
                var padd = _context.ProAdvertisements.SingleOrDefault(p => p.Id == id);

                if (User.IsInRole(RoleName.Professional))
                {
                    var currentLoggedIdPro = User.Identity.GetUserId();
                    var proLinkedtoLogged = _context.Professionals.Include(x => x.Profession).Include(x => x.Profession.Category).SingleOrDefault(m => m.CurrentUserId == currentLoggedIdPro);
                    int idToQueryPro = proLinkedtoLogged.Id;


                    if (padd.ProfessionalId != idToQueryPro)
                    {
                        return HttpNotFound();
                    }
                    _context.ProAdvertisements.Remove(padd);
                    _context.SaveChanges();

                }
                

                
                if (User.IsInRole(RoleName.Admin))
                {
                    _context.ProAdvertisements.Remove(padd);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "ProAdvertisements");
                }
                
            }
            
            return RedirectToAction("Index", "ProAdvertisements");
        }


        // GET: ProAdvertisements
        [AllowAnonymous]
        public ActionResult Index()
        {

            var allAds = _context.ProAdvertisements.Include(x=>x.Advertisement)
                        .Include(x=>x.Professional).ToList();


            if (User.IsInRole(RoleName.Professional))
            {
                var currentLoggedIdPro = User.Identity.GetUserId();
                var proLinkedtoLogged = _context.Professionals.Include(x => x.Profession).Include(x => x.Profession.Category).SingleOrDefault(m => m.CurrentUserId == currentLoggedIdPro);
                int idToQueryPro = proLinkedtoLogged.Id;
                


                //return View("IndexPro", allAds);
                return View("IndexPro", allAds);
            }
            if (User.IsInRole(RoleName.Admin))
            {
                return View("IndexAdmin", allAds);
            }

            
            return View("IndexUserAdds", allAds);
            

            
            
        }



    }
}