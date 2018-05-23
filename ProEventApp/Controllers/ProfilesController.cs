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

        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult UserProfile()
        {
            var userId = User.Identity.GetUserId();
            var _appUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId);
            var _appUserId = _appUser.Id;
            var profileId = _context.AppUsers.SingleOrDefault(m => m.Id == _appUserId).ProfileId;
            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);
            var _profileImages = _context.ProfileImages.Include(m => m.Image).Where(m => m.ProfileId == profileId)
                .ToList();
            var _UPComments = _context.UserProComments.Where(c => c.AppUserId == _appUserId).ToList();


            var viewModel = new ProfileViewModel()
            {
                AppUser = _appUser,
                Profile = profile,
                ProfileImages = _profileImages,
                UserProComments = _UPComments
            };

            return View("UserDetailProfile", viewModel);
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
            var _UPComments = _context.UserProComments.Where(c => c.ProfessionalId == proId).Include(c=>c.Comment).ToList();


            var viewModel = new ProfileViewModel()
            {
                Professional = professional,
                Profile = profile,
                ProfileImages = _profileImages,
                UserProComments = _UPComments
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
            var _UPComments = _context.UserProComments.Where(c => c.ProfessionalId == proId).Include(c=>c.Comment).ToList();

            var viewModel = new ProfileViewModel()
            {
                Professional = professional,
                Profile = profile,
                ProfileImages = _profileImages,
                UserProComments = _UPComments
            };

            return View("ProDetailProfile", viewModel);
        }

        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult ProfileFromAds(int id)
        {
            var pro = _context.Professionals.SingleOrDefault(m => m.Id == id);
            var proId = pro.Id;
            var professional = _context.Professionals.Include(m => m.Profession).SingleOrDefault(m => m.Id == proId);
            var profileId = _context.Professionals.SingleOrDefault(m => m.Id == proId).ProfileId;
            var profile = _context.Profiles.SingleOrDefault(m => m.Id == profileId);
            var _profileImages = _context.ProfileImages.Include(m => m.Image).Where(m => m.ProfileId == profileId)
                .ToList();
            var _UPComments = _context.UserProComments.Where(c => c.ProfessionalId == proId).Include(c => c.Comment).ToList();

            var viewModel = new ProfileViewModel()
            {
                Professional = professional,
                Profile = profile,
                ProfileImages = _profileImages,
                UserProComments = _UPComments
            };

            return View("ProfileFromAds", viewModel);
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
            var _UPComments = _context.UserProComments.Where(c => c.AppUserId == _appUserId).ToList();


            var viewModel = new ProfileViewModel()
            {
                AppUser = _appUser,
                Profile = profile,
                ProfileImages = _profileImages,
                UserProComments = _UPComments
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
            var _UPComments = _context.UserProComments.Where(c => c.AppUserId == _appUserId).ToList();

            var viewModel = new ProfileViewModel()
            {
                AppUser = _appUser,
                Profile = profile,
                ProfileImages = _profileImages,
                UserProComments = _UPComments
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




            if (User.IsInRole(RoleName.Professional))
            {
                return RedirectToAction("ProDetailProfile", "Profiles");
            }
            
             return RedirectToAction("UserProfile", "Profiles");
            
        }
        /// <summary>
        /// user or pro can only add comment to contrahent's profile after 
        /// the job is done so the if statement is if job accepted
        ///  and job done == true than can add comment to the profile
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public ActionResult AddCommentToUser(Comment comment)
        {  // ProDetailProfileSpecific
            var userId = User.Identity.GetUserId();
            var appUser = new AppUser();
            var appUserId = 0;
            var pro = new Professional();
            var proId = 0;

            var _urlString = Request.UrlReferrer.ToString();
            var url = new Uri(_urlString);
            var lastSegment = url.Segments.Last();

            var profileId = Convert.ToInt32(lastSegment);

            var profile = _context.Profiles.SingleOrDefault(p => p.Id == profileId);
            var idOfProfilesUser = profileId;



            if (User.IsInRole(RoleName.AppUser))
            {
                 appUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId);
                appUserId = appUser.Id;

                pro = _context.Professionals.SingleOrDefault(m => m.Id == idOfProfilesUser);

                // 2 == Accepted
                var acceptedEvents = _context.EventProfessionals.Where(m => m.InvitationStatusId == 2)
                    .Where(m => m.AppEvent.AppUserId == appUserId).Where(m => m.ProfessionalId == pro.Id).Where(m => m.Done == true).ToList();

                if (acceptedEvents.Any())
                {

                    if (comment.Id == 0)
                    {
                        Guid guid = Guid.NewGuid();
                        string strGuid = guid.ToString();

                        comment.DateCreated = DateTime.Now;
                        comment.Unique = strGuid;
                        comment.Who = appUser.Nickname == String.Empty
                                    ? appUser.Name + " " + appUser.Surname
                                    : appUser.Nickname;
                        _context.UPComments.Add(comment);

                        _context.SaveChanges();

                        var cAdded = _context.UPComments.SingleOrDefault(c => c.Unique == strGuid);
                        var _upComment = new UserProComment();
                        _upComment.Comment = cAdded;
                        _upComment.CommentId = cAdded.Id;
                        _upComment.ProfessionalId = pro.Id;
                        _upComment.Professional = _context.Professionals.SingleOrDefault(p => p.Id == pro.Id);
                        _upComment.AppUserId = appUser.Id;
                        _upComment.AppUser = _context.AppUsers.SingleOrDefault(u => u.Id == appUser.Id);
                        _upComment.Who = appUser.Id.ToString();


                        _context.UserProComments.Add(_upComment);
                        _context.SaveChanges();
                    }

                }
                else
                {
                    ViewBag.Message("No deals done together, you cannot comment on this Pro!");
                    return Redirect(Request.UrlReferrer.ToString());
                }


            }
            if (User.IsInRole(RoleName.Professional))
            {
                pro = _context.Professionals.SingleOrDefault(p => p.CurrentUserId == userId);
                proId = pro.Id;

                appUser = _context.AppUsers.SingleOrDefault(m => m.Id == idOfProfilesUser);

                // 2 == Accepted
                var acceptedEvents = _context.EventProfessionals.Where(m => m.InvitationStatusId == 2)
                    .Where(m => m.AppEvent.AppUserId == appUser.Id).Where(m => m.ProfessionalId == proId).Where(m => m.Done == true).ToList();


                if (acceptedEvents.Any())
                {

                    if (comment.Id == 0)
                    {
                        Guid guid = Guid.NewGuid();
                        string strGuid = guid.ToString();

                        comment.DateCreated = DateTime.Now;
                        comment.Unique = strGuid;
                        comment.Who = pro.Name + " " + pro.Surname;
                        _context.UPComments.Add(comment);

                        _context.SaveChanges();

                        var cAdded = _context.UPComments.SingleOrDefault(c => c.Unique == strGuid);
                        var _upComment = new UserProComment();
                        _upComment.CommentId = cAdded.Id;
                        _upComment.Comment = cAdded;
                        _upComment.ProfessionalId = pro.Id;
                        _upComment.Professional = _context.Professionals.SingleOrDefault(p => p.Id == pro.Id);
                        _upComment.AppUserId = appUser.Id;
                        _upComment.AppUser = _context.AppUsers.SingleOrDefault(u => u.Id == appUser.Id);
                        _upComment.Who = pro.Id.ToString();

                        _context.UserProComments.Add(_upComment);
                        _context.SaveChanges();
                    }


                }
                else
                {
                    ViewBag.Message("No deals done together, you cannot comment on this User!");
                    return Redirect(Request.UrlReferrer.ToString());
                }
                
            }

           
            return Redirect(Request.UrlReferrer.ToString());
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
