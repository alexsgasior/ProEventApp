using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProEventApp.Models;

namespace ProEventApp.Controllers
{
    public class ProfessionalController : Controller
    {
        private ApplicationDbContext _context;

        public ProfessionalController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        public ActionResult Delete(int id)
        {
            var pro = _context.Professionals.SingleOrDefault(p => p.Id == id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            _context.Professionals.Remove(pro);
            _context.SaveChanges();
            return RedirectToAction("Index", "Professional");

        }


        // GET: Professional
        public ActionResult Index()
        {
            var pros = _context.Professionals.Include(m=>m.Profession).ToList();

            return View(pros);
        }
    }
}