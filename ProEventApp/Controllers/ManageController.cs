using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProEventApp.Models;
using ProEventApp.ViewModels;

namespace ProEventApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;

        public ManageController()
        {
            _context=new ApplicationDbContext();
        }

        [Authorize(Roles = RoleName.Professional+","+RoleName.AppUser)]
        public ActionResult NewProfile()
        {
            var currentId = User.Identity.GetUserId();
            
            var userTeraz = _context.Users.FirstOrDefault(m => m.Id == currentId);
            var isUser = userTeraz.AppUserId != null;
            var isProfessional = userTeraz.ProfessionalId != null;

            if (isUser)
            {
                var _appUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentId);
                var viewModel = new UserProfileViewModel()
                {
                    AppUser = _appUser
                };
                return View("userProfileForm", viewModel);
            }
            if (isProfessional)
            {
                var _professional = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == currentId);
                var viewModel = new ProfessionalProfileViewModel()
                {
                    Professional = _professional
                };
                return View("professionalProfileForm", viewModel);
            }
            return RedirectToAction("Index", "Manage");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult SaveProfileToUser(UserProfileViewModel _userProfile)
        {
            var currentId = User.Identity.GetUserId();
            //if (!ModelState.IsValid)
            //{
                
            //    var _appUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentId);
            //    var viewModel = new UserProfileViewModel()
            //    {
            //        AppUser = _appUser
            //    };
            //    return View("userProfileForm", viewModel);
            //}
            _userProfile.Profile.WhoCreated = User.Identity.GetUserId();
            _context.Profiles.Add(_userProfile.Profile);
            _context.SaveChanges();


            var profileToLink = _context.Profiles.SingleOrDefault(m => m.WhoCreated == currentId);
            var userToGetProfile = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentId);

            userToGetProfile.ProfileId = profileToLink.Id;
            _context.SaveChanges();


            return RedirectToAction("Index", "Manage");
        }
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Professional)]
        public ActionResult SaveProfileToProfessional(ProfessionalProfileViewModel _proProfile)
        {
            var currentId = User.Identity.GetUserId();
            //if (!ModelState.IsValid)
            //{
            //    var _professional = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == currentId);
            //    var viewModel = new ProfessionalProfileViewModel()
            //    {
            //        Professional = _professional
            //    };
            //    return View("professionalProfileForm", viewModel);
            //}
            _proProfile.Profile.WhoCreated = User.Identity.GetUserId();
            _context.Profiles.Add(_proProfile.Profile);
            _context.SaveChanges();
            
            var profileToLink = _context.Profiles.SingleOrDefault(m => m.WhoCreated == currentId);
            var professionalToGetProfile = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == currentId);

            professionalToGetProfile.ProfileId = profileToLink.Id;
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Manage");
        }
        //public ActionResult NewAdmin()
        //{
        //    var appuser = _context.AppUsers.ToList();
        //    //var viewModel = new ProfessionFormViewModel
        //    //{
        //    //    Categories = categories
        //    //};


        //    return View("AdminForm");
        //}
        //public async Task<ActionResult> AddUserToAdminRole(AppUser _appUser)
        //{
        //    var currentId = User.Identity.GetUserId();
        //    _appUser.CurrentUserId = currentId;

        //    var userTeraz = _context.Users.FirstOrDefault(m => m.Id == currentId);

        //    _appUser.Email = userTeraz.Email;
        //    _appUser.ProfileId = 1; // tworzy wiazanie do profilu "domyslnego"!! 


        //    if (_appUser.Id == 0)
        //    {
        //        _context.AppUsers.Add(_appUser);
        //    }

        //    _context.SaveChanges();



        //    var appUserToLink = _context.AppUsers.FirstOrDefault(m => m.CurrentUserId == currentId);
        //    int appUserToLinkId = appUserToLink.Id;
        //    userTeraz.AppUserId = appUserToLinkId;

        //    _context.SaveChanges();
        //    //var roleStore = new RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new ApplicationDbContext());
        //    //var roleManager = new RoleManager<IdentityRole>(roleStore);
        //    //await roleManager.CreateAsync(new IdentityRole(RoleName.Admin));
        //    //UserManager.AddToRole(userTeraz.Id, RoleName.Admin);

        //    return RedirectToAction("Index", "Manage");
        //}
        public ActionResult NewAppUser()
        {
            var appuser = _context.AppUsers.ToList();
            //var viewModel = new ProfessionFormViewModel
            //{
            //    Categories = categories
            //};


            return View("AppUserForm");
        }
        [ValidateAntiForgeryToken]
        public ActionResult AddUserToAppUserRole(AppUser _appUser)
        {
            var currentId = User.Identity.GetUserId();
            _appUser.CurrentUserId = currentId;

            var userTeraz = _context.Users.FirstOrDefault(m => m.Id == currentId);

            _appUser.Email = userTeraz.Email;
            _appUser.ProfileId = 1; // tworzy wiazanie do profilu "domyslnego"!! 
            

            if (_appUser.Id == 0)
            {
                _context.AppUsers.Add(_appUser);
            }

            _context.SaveChanges();



            var appUserToLink = _context.AppUsers.FirstOrDefault(m => m.CurrentUserId == currentId);
            int appUserToLinkId = appUserToLink.Id;
            userTeraz.AppUserId = appUserToLinkId;

            _context.SaveChanges();
            var roleStore = new RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            //await roleManager.CreateAsync(new IdentityRole("AppUser"));
            UserManager.AddToRole(userTeraz.Id, "AppUser");

            return RedirectToAction("Index", "Manage");
        }

        public ActionResult NewProfessional()
        {
            var pro = _context.Professionals.ToList();
            var professions = _context.Professions.ToList();

            var viewModel = new ProfessionalFormViewModel
            {
                Professions = professions
            };


            return View("ProfessionalForm",viewModel);
        }


        [ValidateAntiForgeryToken]
        public ActionResult AddUserToProfessionalRole(/*Professional _professional*/ProfessionalFormViewModel _professional)
        {

            //if (!ModelState.IsValid)
            //{
            //    var pro = _context.Professionals.ToList();
            //    var professions = _context.Professions.ToList();

            //    var viewModel = new ProfessionalFormViewModel
            //    {
            //        Professions = professions
            //    };


            //    return View("ProfessionalForm", viewModel);
            //}

            
            var currentId = User.Identity.GetUserId();
            
            var userTeraz = _context.Users.FirstOrDefault(m => m.Id == currentId);

            bool flaga = _context.Professionals.Any(m => m.CurrentUserId == currentId);
            if (!flaga)
            {
                _professional.Professional.CurrentUserId = currentId;

                

                _professional.Professional.Email = userTeraz.Email;
                _professional.Professional.ProfileId = 1;// tworzy tymczasowy profil "domyslny"

                if (_professional.Professional.Id == 0)
                {
                    _context.Professionals.Add(_professional.Professional);
                }

                _context.SaveChanges();
            }
            
            var professionalToLink = _context.Professionals.FirstOrDefault(m => m.CurrentUserId == currentId);
            int professionalToLinkId = professionalToLink.Id;
            userTeraz.ProfessionalId = professionalToLinkId;

            _context.SaveChanges();
            var roleStore = new RoleStore<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            //await roleManager.CreateAsync(new IdentityRole("AppUser"));
            UserManager.AddToRole(userTeraz.Id, "Professional");

            return RedirectToAction("Index", "Manage");
        }





        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };

            var userToCheck = _context.Users.SingleOrDefault(m => m.Id == userId); // user z bazy logowania 

            bool haveRole = userToCheck.AppUserId != null || userToCheck.ProfessionalId != null ? true : false; //czy user ma role w systemie wybrana 

            AppUser rolledUser = new AppUser();
            Professional rolledProfessional = new Professional();
            bool isUser = userToCheck.AppUserId != null ? true : false;
            bool isPro = userToCheck.ProfessionalId!=null? true : false;

            if(isUser)
                rolledUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId);

            if(isPro)
                rolledProfessional = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == userId);

            bool userHasProfile = rolledUser.ProfileId != 1 ? true : false;
            bool proHasProfile = rolledProfessional.ProfileId != 1 ? true : false;  // przy tworzeniu nowego uzytkownika przypisuje mu
                                                                                    // id profilu 1



            if (!haveRole)
            {
                return View("IndexWithRoleChoose",model);
            }

            if (haveRole)
            {
                if (isUser && !userHasProfile)
                {
                    return View("IndexCreateUserProfile", model);
                }
                if (isPro && !proHasProfile)
                {
                    return View("IndexCreateProfessionalProfile", model);
                }
            }

            if (isUser && userHasProfile && User.IsInRole(RoleName.AppUser))
            {
                var _appUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId);
                var _appUserId = _appUser.Id;

                model.InvitationStatuses = _context.InvitationStatuses.ToList();
                model.AppUser = _appUser;




                var profile = _context.Profiles.SingleOrDefault(m => m.Id == _appUser.ProfileId);

                var _profileImages = _context.ProfileImages.Include(m => m.Image)
                    .Where(m => m.ProfileId == profile.Id)
                    .ToList();

                

                model.ProfileImages = _profileImages;
                model.Profile = profile;



                return View("IndexUserDashboard", model);
            }
            if (isPro && proHasProfile && User.IsInRole(RoleName.Professional))
            {
                var pro = _context.Professionals.Include(m=>m.Profession).Include(m=>m.Profile).SingleOrDefault(m => m.CurrentUserId == userId);
                var proId = pro.Id;
                
                model.InvitationStatuses = _context.InvitationStatuses.ToList();


                var acceptedEvents = _context.EventProfessionals.Include(m=>m.AppEvent).Where(m => m.InvitationStatusId == 2)
                    .Where(m => m.ProfessionalId == proId).ToList();

                var acceptedEventIds = acceptedEvents.Select(m => m.AppEventId).ToList();

                var _listOfAcceptedAppEvents = _context.Events.Include(m => m.Status).Include(m => m.Category)
                    .Include(m => m.AppUser).Include(m => m.City)
                    .Where(m => acceptedEventIds.Contains(m.Id));

                var _allAppEvents = _context.Events.Include(m => m.Status).Include(m => m.Category)
                                    .Include(m => m.AppUser).Include(m => m.City).ToList();

                
                var tempAll = _allAppEvents;
                
                foreach (var item in _listOfAcceptedAppEvents) tempAll.Remove(item);
                var liczba = tempAll.Count();  // sprawdzam czy usunela zaakceptoany event
                model.AppEvents = tempAll.Where(m=>m.CategoryId == pro.Profession.CategoryId);
                model.Professional = pro;
                var profile = _context.Profiles.SingleOrDefault(m => m.Id == pro.ProfileId);

                var _profileImages = _context.ProfileImages.Include(m => m.Image)
                                        .Where(m => m.ProfileId == profile.Id)
                                        .ToList();

                //var viewModel = new ProfileViewModel()
                //{
                //    Professional = professional,
                //    Profile = profile,
                //    ProfileImages = _profileImages
                //};



                model.ProfileImages = _profileImages;
                model.Profile = profile;

                return View("IndexProfessionalDashboard", model);
            }


            return View(model);
        }




        [Authorize(Roles = RoleName.Professional)]
        public ActionResult ListProInv()
        {
            var userId = User.Identity.GetUserId();
            var pro = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == userId);
            var proId = pro.Id;

            var viewModel = new ListOfProInvsViewModel();

            var eventToGetAppEventId = _context.EventProfessionals.Where(m => m.ProfessionalId == proId);
            
            var _events = _context.EventProfessionals.Include(m => m.AppEvent)
                            .Include(m => m.InvitationStatus).Where(m => m.ProfessionalId == proId)
                            .Where(m=>m.InvitationStatusId!=2 && m.InvitationStatusId!=3)
                            .Where(m=>m.Rola == "AppUser").ToList();

            
            viewModel.EventProfessionals = _events;
            viewModel.InvitationStatuses = _context.InvitationStatuses.ToList();

            return View("ListOfProInvs", viewModel);
        }

        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult ListInvFromProToEvent()
        {
            var userId = User.Identity.GetUserId();
            var appuser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId);
            var appUserId = appuser.Id;

            var viewModel = new ListOfProInvsViewModel();
            
            var _events = _context.EventProfessionals.Include(m => m.AppEvent)
                .Include(m => m.InvitationStatus).Include(m=>m.Professional).Where(m => m.AppEvent.AppUserId == appUserId)
                .Where(m => m.InvitationStatusId != 2 && m.InvitationStatusId != 3)
                .Where(m => m.Rola == "Professional").ToList();


            viewModel.EventProfessionals = _events;
            viewModel.InvitationStatuses = _context.InvitationStatuses.ToList();

            return View("ListInvFromProToEvent", viewModel);
        }

        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult AppUserChangeInvStatusToAccept(int id)
        {
            
            var invToChangeStatus = _context.EventProfessionals.SingleOrDefault(m => m.Id == id);
            invToChangeStatus.InvitationStatusId = _context.InvitationStatuses
                .SingleOrDefault(m => m.Name == "Accepted").Id;

            _context.SaveChanges();

            return RedirectToAction("ListInvFromProToEvent");

        }


        
        [Authorize(Roles = RoleName.Professional)]
        public ActionResult ProChangeInvStatusToAccept(int id)
        {
            var invToChangeStatus = _context.EventProfessionals.SingleOrDefault(m=>m.Id==id);
            invToChangeStatus.InvitationStatusId = _context.InvitationStatuses
                                                    .SingleOrDefault(m => m.Name == "Accepted").Id;

            _context.SaveChanges();

            return RedirectToAction("ListProInv");

        }

        [Authorize(Roles = RoleName.Professional)]
        public ActionResult ListOfAcceptedEvents()
        {
            var userId = User.Identity.GetUserId();
            var pro = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == userId);
            var proId = pro.Id;
            // 2 == Accepted
            var acceptedEvents = _context.EventProfessionals.Where(m => m.InvitationStatusId == 2)
                                                            .Where(m=>m.ProfessionalId==proId).ToList();
            var acceptedEventIds = acceptedEvents.Select(m => m.AppEventId).ToList();
            var _events = _context.Events.Include(m => m.Status).Include(m => m.Category)
                            .Include(m => m.AppUser).Include(m => m.City)
                            .Where(m => acceptedEventIds.Contains(m.Id));

            return View("ListOfAcceptedEvents",_events);
        }

        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult ListOfAcceptedEventsForAppUser()
        {
            var userId = User.Identity.GetUserId();
            var _appUser = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId);
            var _appUserId = _appUser.Id;
            // 2 == Accepted
            var acceptedEvents = _context.EventProfessionals.Where(m => m.InvitationStatusId == 2)
                .Where(m => m.AppEvent.AppUserId == _appUserId).ToList();
            var acceptedEventIds = acceptedEvents.Select(m => m.AppEventId).ToList();
            var _events = _context.Events.Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .Where(m => acceptedEventIds.Contains(m.Id));

            return View("ListOfAcceptedEventsForAppUser", _events);
        }

        // only user can authorize if the work is done
        // 2==> Inv Accepted
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult DecideIfJobDone(int id)
        {
            var invToChangeStatus = _context.EventProfessionals.SingleOrDefault(m => m.Id == id);

            invToChangeStatus.Done = true;


            // id of the event form the url
            var _urlString = Request.UrlReferrer.ToString();
            var url = new Uri(_urlString);
            var lastSegment = url.Segments.Last();
            var _event_id = Convert.ToInt32(lastSegment);


            var _event = _context.Events.SingleOrDefault(e => e.Id == _event_id);
            _event.Done = true;


            _context.SaveChanges();



            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult AppUserChangeInvStatusToDecline(int id)
        {
            var invToChangeStatus = _context.EventProfessionals.SingleOrDefault(m => m.Id == id);
            invToChangeStatus.InvitationStatusId = _context.InvitationStatuses
                .SingleOrDefault(m => m.Name == "Decline").Id;

            _context.SaveChanges();

            return RedirectToAction("ListInvFromProToEvent");

        }

        [Authorize(Roles = RoleName.Professional)]
        public ActionResult ProChangeInvStatusToDecline(int id)
        {
            var invToChangeStatus = _context.EventProfessionals.SingleOrDefault(m => m.Id == id);
            invToChangeStatus.InvitationStatusId = _context.InvitationStatuses
                                                    .SingleOrDefault(m => m.Name == "Decline").Id;

            _context.SaveChanges();

            return RedirectToAction("ListProInv");

        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}