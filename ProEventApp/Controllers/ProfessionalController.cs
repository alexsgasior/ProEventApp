using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProEventApp.Models;

namespace ProEventApp.Controllers
{
    [AllowAnonymous]
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
        public ActionResult Index(string proName)
        {
            var pros = _context.Professionals.Include(m => m.Profession).ToList();
            
            return View();
        }

        public ActionResult ListOfProsByProfessions(string search_query)
        {
            var profs = _context.Professionals.Include(m => m.Profession).ToList();
            if (!string.IsNullOrWhiteSpace(search_query))
            {
                profs = _context.Professionals.Include(m=>m.Profession.Category).Include(m => m.Profession).Where(m => m.Profession.Name == search_query || m.Name== search_query
                                                                                || m.Surname == search_query || m.Profession.Category.Name==search_query ||m.CompanyName == search_query
                                                                                || m.Phone == search_query)
                                                                                .ToList();
            }
            
            if (User.IsInRole(RoleName.AppUser) || User.IsInRole(RoleName.Professional))
            {
                return View("ListOfProsForProAndAppUser", profs);
            }

            if (User.IsInRole(RoleName.Admin))
            {
                return View("ListOfProsForAdmin", profs);
            }

            return View("ListByProfessions", profs);
        }






    }
}