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
    public class ProfilesController : Controller
    {
        private ApplicationDbContext _context;

        public ProfilesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult Save(Profile profile)
        {
            if (!ModelState.IsValid)
            {
                var states = _context.Profiles.ToList();
                return View("ProfileForm");
            }


            if (profile.Id == 0)
            {
                _context.Profiles.Add(profile);
            }
            else
            {
                var profileInDb = _context.Profiles.Single(p => p.Id == profile.Id);

                profileInDb.Motto = profile.Motto;
                profileInDb.About = profile.About;


            }

            _context.SaveChanges();


            return RedirectToAction("Index","Profiles");
        }



        [Authorize(Roles = RoleName.Professional)]
        public ActionResult ProDetailProfile()
        {
            var userId = User.Identity.GetUserId();
            var pro = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == userId);
            var proId = pro.Id;
            var professional = _context.Professionals.Include(m=>m.Profession).SingleOrDefault(m => m.Id == proId);
            var profileId = _context.Professionals.SingleOrDefault(m => m.Id == proId).ProfileId;
            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);
            var _profileImages = _context.ProfileImages.Include(m => m.Image).Where(m => m.ProfileId == profileId)
                .ToList();

            var viewModel = new ProfileViewModel()
            {
                Professional = professional,
                Profile = profile,
                ProfileImages = _profileImages
            };

            return View("ProDetailProfile",viewModel);
        }
        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult ProDetailProfileSpecific(int id)
        {
            var pro = _context.Professionals.SingleOrDefault(m => m.Id == id);
            var proId = pro.Id;
            var professional = _context.Professionals.Include(m => m.Profession).SingleOrDefault(m => m.Id == proId);
            var profileId = _context.Professionals.SingleOrDefault(m => m.Id == proId).ProfileId;
            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);
            var _profileImages = _context.ProfileImages.Include(m => m.Image).Where(m => m.ProfileId == profileId)
                .ToList();

            var viewModel = new ProfileViewModel()
            {
                Professional = professional,
                Profile = profile,
                ProfileImages = _profileImages
            };

            return View("ProDetailProfile", viewModel);
        }



        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult UserDetailProfileSpecific(int id)
        {
            var _appUser = _context.AppUsers.SingleOrDefault(m => m.Id == id);
            var _appUserId = _appUser.Id;
            var profileId = _context.AppUsers.SingleOrDefault(m => m.Id == _appUserId).ProfileId;
            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);
            var _profileImages = _context.ProfileImages.Include(m => m.Image).Where(m => m.ProfileId == profileId)
                .ToList();

            var viewModel = new ProfileViewModel()
            {
                AppUser = _appUser,
                Profile = profile,
                ProfileImages = _profileImages
            };

            return View("UserDetailProfile", viewModel);
        }
        [Authorize(Roles = RoleName.Professional)]
        public ActionResult UserDetailProfileSpecificForPro(int id)
        {
            var _appUser = _context.AppUsers.SingleOrDefault(m => m.Id == id);
            var _appUserId = _appUser.Id;
            var profileId = _context.AppUsers.SingleOrDefault(m => m.Id == _appUserId).ProfileId;
            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);
            var _profileImages = _context.ProfileImages.Include(m => m.Image).Where(m => m.ProfileId == profileId)
                .ToList();

            var viewModel = new ProfileViewModel()
            {
                AppUser = _appUser,
                Profile = profile,
                ProfileImages = _profileImages
            };

            return View("UserDetailProfileForPro", viewModel);
        }


        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult AddImg(int id)
        {
            Image img = new Image();
            TempData["ID"] = id;
            return View("AddImgProfile",img);
        }


        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult AddImggToProfile(Image modelImage, HttpPostedFileBase image1, int profileId = 0)
        {
            Guid guid = Guid.NewGuid();
            string strGuid = guid.ToString();


            profileId = Convert.ToInt32(TempData["ID"]);

            if (image1 != null)
            {
                modelImage.Bytes = new byte[image1.ContentLength];
                modelImage.ContentBase64 = profileId.ToString() + strGuid;
                image1.InputStream.Read(modelImage.Bytes, 0, image1.ContentLength);

            }
            _context.Images.Add(modelImage);
            _context.SaveChanges();


            var imagetoAdd = _context.Images.SingleOrDefault(m =>
                m.ContentBase64 == profileId.ToString() + strGuid);

            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);


            var _profileImage = new ProfileImage()
            {
                Profile = profile,
                ProfileId = profileId,
                Image = imagetoAdd,
                ImageId = imagetoAdd.Id
            };
            
            _context.ProfileImages.Add(_profileImage);
            
            _context.SaveChanges();

            return RedirectToAction("ProDetailProfile", "Profiles");
        }








        public ActionResult New()
        {           
            return View("ProfileForm");
        }

        public ActionResult Index()
        {
            var profiles = _context.Profiles.ToList();
            return View(profiles);
        }
    }
}
