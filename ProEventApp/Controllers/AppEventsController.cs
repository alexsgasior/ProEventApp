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

        

        [Authorize(Roles = RoleName.Professional)]
        public ActionResult NewInvToEventFromPro(int id=0)
        {
            var appEventId = id;//Convert.ToInt32(TempData["IdOfCurrentEventPro"]);
            TempData["idOfEventPassToInvPro"] = appEventId;
            

            var userIdToPass = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            //var appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);
            var pro = _context.Professionals.SingleOrDefault(c => c.Id == user.ProfessionalId);

            //var professionalToInv = _context.Professionals.SingleOrDefault(m => m.Id == id);
            var eventToInv = _context.Events.SingleOrDefault(m => m.Id == appEventId);
            var invStatus = _context.InvitationStatuses.ToList();

            var professionalWhoInvited = _context.Professionals.SingleOrDefault(m => m.Id == pro.Id);


            TempData["professionalToInvPro"] = professionalWhoInvited.Id;

            var viewModel = new EventProfessionalForm()
            {
                //AppEvent = eventToInv,
                InvitationStatuses = invStatus
                //Professional = professionalToInv
            };


            return View("EventProfessionalFormFromPro", viewModel);
        }

        [Authorize(Roles = RoleName.Professional)]
        public ActionResult SaveProfessionalToEventFromPro(EventProfessionalForm _eventProfessional, int id = 0)
        {
            var appEventId = Convert.ToInt32(TempData["idOfEventPassToInvPro"]); // IdOfCurrentEvent
            id = Convert.ToInt32(TempData["professionalToInvPro"]);

            var userIdToPass = User.Identity.GetUserId();
            //var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            //var appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);

            var professionalToInv = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == userIdToPass);
            var eventToInv = _context.Events.SingleOrDefault(m => m.Id == appEventId);
            var invStatus =
                _context.InvitationStatuses.SingleOrDefault(m => m.Id == _eventProfessional.EventProfessional.InvitationStatusId);

            var invStatus_Pending = _context.InvitationStatuses.SingleOrDefault(m => m.Name == "Pending");

            var inv = new EventProfessional()
            {
                Comment = _eventProfessional.EventProfessional.Comment,
                Importance = _eventProfessional.EventProfessional.Importance,
                Price = _eventProfessional.EventProfessional.Price,
                ProfessionalId = professionalToInv.Id,
                Professional = professionalToInv,
                InvitationStatusId = /*_eventProfessional.EventProfessional.InvitationStatusId*/invStatus_Pending.Id,
                InvitationStatus = invStatus_Pending,
                AppEvent = eventToInv,
                AppEventId = appEventId,
                Rola = "Professional"
            };
            _context.EventProfessionals.Add(inv);
            _context.SaveChanges();
            //return View();
            return RedirectToAction("Index", "AppEvents");
        }

        
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult NewInvToEvent(int id)
        {
            var appEventId = Convert.ToInt32(TempData["IdOfCurrentEvent"]);
            TempData["idOfEventPassToInv"] = appEventId;

            var userIdToPass = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            var appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);

            var professionalToInv = _context.Professionals.SingleOrDefault(m => m.Id == id);
            var eventToInv = _context.Events.SingleOrDefault(m => m.Id == appEventId);
            var invStatus = _context.InvitationStatuses.ToList();

            TempData["professionalToInv"] = professionalToInv.Id;

            var viewModel = new EventProfessionalForm()
            {
                //AppEvent = eventToInv,
                InvitationStatuses = invStatus
                //Professional = professionalToInv
            };


            return View("EventProfessionalForm",viewModel);
        }
        // zrobic na przykladzie od Mosha, najprostsze dodawanie z wyborem z listy dropdown
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult SaveProfessionalToEvent(EventProfessionalForm _eventProfessional,int id=0)
        {
            var appEventId = Convert.ToInt32(TempData["idOfEventPassToInv"]); // IdOfCurrentEvent
            id = Convert.ToInt32(TempData["professionalToInv"]);

            var userIdToPass = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            _eventProfessional.EventProfessional.Importance = Convert.ToDouble(_eventProfessional.ScoringRange); 
            

            var appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);

            var professionalToInv = _context.Professionals.SingleOrDefault(m => m.Id == id);            
            var eventToInv = _context.Events.SingleOrDefault(m => m.Id == appEventId);
            var invStatus =
                _context.InvitationStatuses.SingleOrDefault(m => m.Id == _eventProfessional.EventProfessional.InvitationStatusId);
            var invStatus_Pending = _context.InvitationStatuses.SingleOrDefault(m => m.Name == "Pending");

            var inv = new EventProfessional()
            {
                Comment = _eventProfessional.EventProfessional.Comment,
                Importance = _eventProfessional.EventProfessional.Importance,
                Price = _eventProfessional.EventProfessional.Price,
                DateOfJob = eventToInv.Date,
                ProfessionalId = professionalToInv.Id,
                Professional = professionalToInv,
                InvitationStatusId = /*_eventProfessional.EventProfessional.InvitationStatusId*/invStatus_Pending.Id,
                InvitationStatus = invStatus,
                AppEvent = eventToInv,
                AppEventId = appEventId,
                Rola = "AppUser"
            };
            _context.EventProfessionals.Add(inv);
            _context.SaveChanges();
            //return View();
            return RedirectToAction("Index", "AppEvents");
        }

        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult AddImg(int id)
        {
            Image img = new Image();
            TempData["ID"] = id;
            return View(img);
        }


        public void AddImgWithEvent(HttpPostedFileBase image1, string comment, int appEventId = 0)
        {
            Guid guid = Guid.NewGuid();
            string strGuid = guid.ToString();
            Image modelImage =new Image();
            
            if (image1 != null)
            {
                modelImage.Bytes = new byte[image1.ContentLength];
                modelImage.ContentBase64 = appEventId.ToString() + strGuid;
                modelImage.WhoAdded = User.IsInRole(RoleName.AppUser) ? "AppUser" : "Professional";
                modelImage.Comment = comment;
                image1.InputStream.Read(modelImage.Bytes, 0, image1.ContentLength);
            }
            else
            {
                return;
            }
            _context.Images.Add(modelImage);
            _context.SaveChanges();

            var imagetoAdd = _context.Images.SingleOrDefault(m =>
                m.ContentBase64 == /*_appEvent.Id*/appEventId.ToString() + strGuid);

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
        }


        [Authorize(Roles = RoleName.Admin + "," + RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult AddImgg(Image modelImage, HttpPostedFileBase image1,string comment, int appEventId=0)
        {
            Guid guid = Guid.NewGuid();
            string strGuid = guid.ToString();
            
            appEventId = Convert.ToInt32(TempData["ID"]);

            if (image1 != null)
            {
                modelImage.Bytes = new byte[image1.ContentLength];
                modelImage.ContentBase64 = /*_appEvent.Id*/appEventId.ToString() + strGuid;
                modelImage.WhoAdded = User.IsInRole(RoleName.AppUser) ? "AppUser" : "Professional";
                modelImage.Comment = comment;
                image1.InputStream.Read(modelImage.Bytes, 0, image1.ContentLength);
            }
            _context.Images.Add(modelImage);
            _context.SaveChanges();

            var imagetoAdd = _context.Images.SingleOrDefault(m =>
                m.ContentBase64 == /*_appEvent.Id*/appEventId.ToString() + strGuid);

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult Save(AppEventFormViewModel model, string comment, HttpPostedFileBase image1)
        {
            var userIdToPass = User.Identity.GetUserId();
            var user = _context.Users.FirstOrDefault(u => u.Id == userIdToPass);
            AppUser appUserToPass = _context.AppUsers.SingleOrDefault(c => c.Id == user.AppUserId);
            
            AppEvent _event = model.AppEvent;

            Guid str_guid = Guid.NewGuid();
            string idToGive = str_guid.ToString();

            _event.Findid = idToGive;
            _event.AppUser = appUserToPass;
            _event.AppUserId = appUserToPass.Id;
            

            if (_event.Id == 0)
            {
                _context.Events.Add(_event);
                _context.SaveChanges();
                var _eventToAddImgWith = _context.Events.SingleOrDefault(x => x.Findid == idToGive);
                AddImgWithEvent(image1,comment,_eventToAddImgWith.Id);
            }
            else
            {
                var eventInDb = _context.Events.Single(m => m.Id == _event.Id);

                eventInDb.Name = _event.Name;
                eventInDb.Description = _event.Description;
                eventInDb.StatusId = _event.StatusId;
                eventInDb.CategoryId = _event.CategoryId;
                //eventInDb.Status = _event.Status;
                eventInDb.CityId = _event.CityId;
                eventInDb.Street = _event.Street;
                eventInDb.HouseNumber = _event.HouseNumber;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "AppEvents");
        }

        [Authorize(Roles = RoleName.AppUser)]
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
        [Authorize(Roles = RoleName.AppUser)]
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

        [Authorize(Roles = RoleName.AppUser+","+RoleName.Admin)]
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


        [HttpGet]
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult DetailsBackToManage(int id)
        {
            TempData["IdOfCurrentEvent"] = id;
            
            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);

            if (_event == null)
            {
                return HttpNotFound();
            }

            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();
           
            var userId = User.Identity.GetUserId();
            var _appUserId = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId).Id;
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var _eventsAccepted = _context.EventProfessionals.Include(m => m.AppEvent)
                .Include(m => m.InvitationStatus).Include(m => m.Professional)
                .Where(m => m.AppEvent.AppUserId == _appUserId)
                .Where(m => m.AppEvent.Id == _event.Id)
                .Where(m => m.InvitationStatusId == 2).ToList();
            
            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                EventProfessionals = _eventsAccepted,
                Comments = comments
            };

            return View("AppEventDetailsFormBackToManage", viewModel);
        }
        [HttpGet]
        [Authorize(Roles = RoleName.AppUser)]
        public ActionResult DetailsAcceptedEvent(int id)
        {
            TempData["IdOfCurrentEvent"] = id;

            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);

            if (_event == null)
            {
                return HttpNotFound();
            }

            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();

            var userId = User.Identity.GetUserId();
            var _appUserId = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId).Id;
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var _eventsAccepted = _context.EventProfessionals.Include(m => m.AppEvent)
                .Include(m => m.InvitationStatus).Include(m => m.Professional)
                .Where(m => m.AppEvent.AppUserId == _appUserId)
                .Where(m => m.AppEvent.Id == _event.Id)
                .Where(m => m.InvitationStatusId == 2).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                EventProfessionals = _eventsAccepted,
                Comments = comments
            };

            return View("AcceptedEvent", viewModel);
        }



        [HttpGet]
        public ActionResult DetailsBackToListInvFromToEvent(int id)
        {
            TempData["IdOfCurrentEvent"] = id;
            
            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);

            if (_event == null)
            {
                return HttpNotFound();
            }
            
            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                Comments = comments
            };

            return View("DetailsBackToListInvFromToEvent", viewModel);
        }
        
        //[Route("AppEvents/Details/id")]
        [HttpGet]
        [Authorize(Roles = RoleName.AppUser+","+RoleName.Admin)]
        public ActionResult Details(int id)
        {
            TempData["IdOfCurrentEvent"] = id;
            
            var _event = _context.Events
                         .Include(m => m.Status).Include(m => m.Category)
                         .Include(m => m.AppUser).Include(m => m.City)
                         .SingleOrDefault(m => m.Id == id);

            if (_event == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var _appUserId = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == userId).Id;

            var _eventsAccepted = _context.EventProfessionals.Include(m => m.AppEvent)
                .Include(m => m.InvitationStatus).Include(m => m.Professional)
                .Where(m => m.AppEvent.AppUserId == _appUserId)
                .Where(m => m.AppEvent.Id == _event.Id)
                .Where(m => m.InvitationStatusId == 2).ToList();

            if (_eventsAccepted.Any())
            {
                return DetailsAcceptedEvent(id);
            }

            
            var _eventImages = _context.EventImages.Include(m=>m.Image).Where(m=>m.AppEventId==id).ToList();
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                Comments = comments
            };

            return View("AppEventDetailsForm",viewModel);
        }

        [Authorize(Roles = RoleName.Professional)]
        public ActionResult DetailsArchiveForPro(int id)
        {
            TempData["IdOfCurrentEvent"] = id;

            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);

            if (_event == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var _appUserId = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == userId).Id;

            //var _eventsAccepted = _context.EventProfessionals.Include(m => m.AppEvent)
            //    .Include(m => m.InvitationStatus).Include(m => m.Professional)
            //    .Where(m => m.AppEvent.AppUserId == _appUserId)
            //    .Where(m => m.AppEvent.Id == _event.Id)
            //    .Where(m => m.InvitationStatusId == 2).ToList();

            //if (_eventsAccepted.Any())
            //{
            //    return DetailsAcceptedEvent(id);
            //}


            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();
            var _evPro = _context.EventProfessionals.Where(e => e.ProfessionalId == _appUserId).Where(e=>e.Done==true).ToList();

            var _evProIds = _evPro.SingleOrDefault(e => e.Id == _evPro.FirstOrDefault().Id);
            var _eventProToPass = _context.EventProfessionals.SingleOrDefault(e => e.Id == _evProIds.Id);
            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                Comments = comments,
                EventProfessional = _eventProToPass
            };

            return View("DeatilsEventArchiveForPro", viewModel);
        }

        public ActionResult SaveCommentToEvent(EventComment eventComment)
        {
            var id = Convert.ToInt32(TempData["IdOfCurrentEvent"]);
            
            var currentId = User.Identity.GetUserId();

            var userTeraz = _context.Users.FirstOrDefault(m => m.Id == currentId);
            var isUser = userTeraz.AppUserId != null;
            
            if (eventComment.Id == 0)
            {
                var appUserInDb = new AppUser();
                var isProfessional = userTeraz.ProfessionalId != null;
                var proInDb = new Professional();
                if (isUser)
                {
                    appUserInDb = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentId);
                }
                if (isProfessional)
                {
                    proInDb = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == currentId);
                }
                
                if (isUser)
                {
                    eventComment.Who = appUserInDb.Name + " " + appUserInDb.Surname;
                }
                else
                {
                    eventComment.Who = proInDb.Name + " " + proInDb.Surname;
                }
                eventComment.DateCreated = DateTime.Now;
                eventComment.AppEventId = id;
                _context.Comments.Add(eventComment);
            }
            else
            {
                var commentInDb = _context.Comments.SingleOrDefault(p => p.Id == eventComment.Id);

                commentInDb.Content = eventComment.Content;
            }

            _context.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());            
        }

        
        [HttpGet]
        [Authorize(Roles = RoleName.Professional)]
        public ActionResult DetailsForPro(int id)
        {
            TempData["IdOfCurrentEvent"] = id;
            
            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                Comments = comments
            };

            return View("AppEventDetailsForPro", viewModel);
        }
        
        [HttpGet]
        [Authorize(Roles = RoleName.Professional)]
        public ActionResult DetailsForProFromAcceptedEvents(int id)
        {
            TempData["IdOfCurrentEvent"] = id;
            
            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                Comments = comments
            };

            return View("AppEventDetailsForProFromAcceptedEvents", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Professional)]
        public ActionResult DetailsForProFromAcceptedEvents2(int id)
        {
            TempData["IdOfCurrentEvent"] = id;
            
            var _event = _context.Events
                .Include(m => m.Status).Include(m => m.Category)
                .Include(m => m.AppUser).Include(m => m.City)
                .SingleOrDefault(m => m.Id == id);
            if (_event == null)
            {
                return HttpNotFound();
            }
            var _eventImages = _context.EventImages.Include(m => m.Image).Where(m => m.AppEventId == id).ToList();
            var comments = _context.Comments.Where(p => p.AppEventId == id).ToList();

            var viewModel = new AppEvent_ImageViewModel()
            {
                Event = _event,
                EventImages = _eventImages,
                Comments = comments
            };

            return View("AppEventDetailsForProFromAcceptedEvents2", viewModel);
        }

        [Authorize(Roles = RoleName.AppUser + "," + RoleName.Professional)]
        public ActionResult EventsArchive()
        {
            var currentLoggedId = User.Identity.GetUserId();
            int idToQuery = 0;
            var userLinkedtoLogged = new AppUser();


            if (User.IsInRole(RoleName.AppUser))
            {
                userLinkedtoLogged = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentLoggedId);
                idToQuery = userLinkedtoLogged.Id;

                var _events = _context.Events.Where(e => e.Done == true)
                    .Include(m => m.Status).Include(m => m.Category)
                    .Include(m => m.AppUser).Include(m => m.City)
                    .ToList().OrderBy(m => m.Status.Name)
                    .Where(m => m.AppUserId == idToQuery);


                return View("EventsArchive", _events);
            }
            var proLinkedToLogged = new Professional();
            if (User.IsInRole(RoleName.Professional))
            {
                proLinkedToLogged = _context.Professionals.SingleOrDefault(m => m.CurrentUserId == currentLoggedId);
                idToQuery = proLinkedToLogged.Id;

                var _doneEvents = _context.EventProfessionals.Include(m => m.AppEvent).Where(m => m.Done==true)
                    .Where(m => m.ProfessionalId == idToQuery).ToList();
                var _doneEventIds = _doneEvents.Select(m => m.AppEventId).ToList();


                //var _events = _context.Events.Where(e => e.Done == true)
                //    .Include(m => m.Status).Include(m => m.Category)
                //    .Include(m => m.AppUser).Include(m => m.City)
                //    .Where(e=> _eventsDone.Contains(e.Id))
                //    .ToList().OrderBy(m => m.Status.Name);

                var _allEventsDoneByPro = _context.Events.Include(m => m.Status).Include(m => m.Category)
                    .Include(m => m.AppUser).Include(m => m.City).Where(e => _doneEventIds.Contains(e.Id)).ToList();

                return View("EventsArchive", _allEventsDoneByPro);
            }

            return ViewBag("No done events yet.");
        }


        public ActionResult FindProsByCategory(int id)
        {
            var pros = _context.Professionals.Include(m=>m.Profession).Where(m => m.Profession.CategoryId == id).ToList();

            return View("ProsByCategory", pros);
        }




        [Authorize(Roles = RoleName.AppUser+","+RoleName.Admin+","+RoleName.Professional)]
        public ActionResult Index()
        {
            var _allEvent = _context.Events.Where(e => e.Done == false)
                .Include(m => m.Status).Include(m => m.Category).Include(m => m.AppUser)
                .Include(m => m.City).OrderBy(m => m.Category.Name).ToList();



            
            if (User.IsInRole(RoleName.Professional))
            {
                var currentLoggedIdPro = User.Identity.GetUserId();
                var proLinkedtoLogged = _context.Professionals.Include(x=>x.Profession).Include(x=>x.Profession.Category).SingleOrDefault(m => m.CurrentUserId == currentLoggedIdPro);
                int idToQueryPro = proLinkedtoLogged.Id;
                string proCat = proLinkedtoLogged.Profession.Category.Name;
                var _allEventByCategory = _context.Events.Where(e => e.Done == false)
                    .Include(m => m.Status).Include(m => m.Category).Include(m => m.AppUser)
                    .Include(m => m.City).Where(m => m.Category.Name == proCat).ToList();

                var viewModel = new ProEventsListIndexViewModel
                {
                    AppEvents = _allEvent,
                    ProCategoryList = _allEventByCategory
                };

                return View("IndexEventsList", /*_allEvent*/viewModel);
            }
            if (User.IsInRole(RoleName.Admin))
            {
                return View("Index", _allEvent);
            }
            var currentLoggedId = User.Identity.GetUserId();
            var userLinkedtoLogged = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentLoggedId);
            int idToQuery = userLinkedtoLogged.Id;

            var _events = _context.Events.Where(e => e.Done == false)
                .Include(m => m.Status).Include(m=>m.Category)
                .Include(m => m.AppUser).Include(m=>m.City).ToList().OrderBy(m => m.Status.Name).Where(m=>m.AppUserId==idToQuery);
            
            return View("Index",_events);
        }


        public ActionResult ListOfEventsByTags(string search_query)
        {

            

            var _allEvents = _context.Events.Where(e => e.Done == false)
            .Include(m => m.Status).Include(m => m.Category).Include(m => m.AppUser)
            .Include(m => m.City).OrderBy(m => m.Category.Name).Where(x=>x.Tags.Contains(search_query)).ToList();

            

            return View("ListOfEventsByTags", _allEvents);
        }






        public ActionResult Calendar()
        {
            return View("Calendar");
        }


        public JsonResult GetEvents()
        {
            if (User.IsInRole(RoleName.Professional) || User.IsInRole(RoleName.Admin))
            {
                var events = _context.Events.Where(e => e.Done == false)
                    .Include(m => m.Status).Include(m => m.Category).Include(m => m.AppUser)
                    .Include(m => m.City).OrderBy(m => m.Category.Name).ToList();

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            else
            {
                var currentLoggedId = User.Identity.GetUserId();
                var userLinkedtoLogged = _context.AppUsers.SingleOrDefault(m => m.CurrentUserId == currentLoggedId);
                int idToQuery = userLinkedtoLogged.Id;


                var events = _context.Events.Where(e => e.Done == false)
                    .Include(m => m.Status).Include(m => m.Category)
                    .Include(m => m.AppUser).Include(m => m.City).ToList().OrderBy(m => m.Status.Name).Where(m => m.AppUserId == idToQuery);

                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            
        }

    }
}