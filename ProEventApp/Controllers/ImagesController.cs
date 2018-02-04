using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProEventApp.Models;

namespace ProEventApp.Controllers
{
    public class ImagesController : Controller
    {

        private ApplicationDbContext _context;

        public ImagesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult AddImg(int? id)
        {
            Image img = new Image();

            return View(img);
        }

        [HttpPost]
        public ActionResult AddImg(Image modelImage, HttpPostedFileBase image1, int appEventId = 0)
        {
            //if (image1 != null)
            //{
            //    modelImage.Bytes = new byte[image1.ContentLength];
            //    image1.InputStream.Read(modelImage.Bytes, 0, image1.ContentLength);

            //}
            //_context.Images.Add(modelImage);
            //_context.SaveChanges();

            // uciąć tu

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

            return View(modelImage);
        }





        public ActionResult AddImage(int? id)
        {
            Image img = new Image();



            return View(img);
        }

        [HttpPost]
        public ActionResult AddImage(Image modelImage,HttpPostedFileBase image1,int appEventId=0)
        {
            //if (image1 != null)
            //{
            //    modelImage.Bytes = new byte[image1.ContentLength];
            //    image1.InputStream.Read(modelImage.Bytes, 0, image1.ContentLength);

            //}
            //_context.Images.Add(modelImage);
            //_context.SaveChanges();

            // uciąć tu

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

            return View(modelImage);
        }

        public ActionResult AddImageToAppEvent(/*AppEvent _appEvent*/int appEventId, Image modelImage, HttpPostedFileBase image1)
        {
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


        // GET: Images
        public ActionResult Index()
        {
            var images = _context.Images.ToList();
            return View(images);
        }
    }
}