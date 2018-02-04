using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProEventApp.Models;
using ProEventApp.ViewModels;

namespace ProEventApp.Controllers
{
    public class CitiesController : Controller
    {


        private ApplicationDbContext _context;

        public CitiesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        public ActionResult Save(City city)
        {
            if (city.Id == 0)
            {
                _context.Cities.Add(city);
            }
            else
            {
                var cityInDb = _context.Cities.Single(p => p.Id == city.Id);

                cityInDb.Name = city.Name;
                cityInDb.PostalCode = city.PostalCode;
                cityInDb.State = city.State;
                cityInDb.StateId = cityInDb.StateId;

            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Cities");
        }




        public ActionResult New()
        {
            var states = _context.States.ToList();
            var viewModel = new CityFormViewModel
            {
                States = states
            };


            return View("CityForm", viewModel);
        }



        [Route("Cities/Edit/id")]
        public ActionResult Edit(int id)
        {
            var city = _context.Cities.SingleOrDefault(p => p.Id == id);
            if (city == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CityFormViewModel()
            {
                City = city,
                States = _context.States.ToList()
            };
            return View("CityForm", viewModel);
        }


        public ActionResult Delete(int id)
        {
            var city = _context.Cities.SingleOrDefault(p => p.Id == id);
            if (city == null)
            {
                return HttpNotFound();
            }
            _context.Cities.Remove(city);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cities");

        }


        //public ActionResult Details(int id)
        //{
        //    var pro = _context.Professions.SingleOrDefault(p => p.Id == id);
        //    if (pro == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var viewModel = new ProfessionFormViewModel
        //    {
        //        Profession = pro,
        //        Categories = _context.Categories.ToList()
        //    };
        //    return View("Details", viewModel);
        //}




        [Route("Cities/Index")]
        public ActionResult Index()
        {
            var cities = _context.Cities.Include(p => p.State).ToList();


            var viewModel = new CityStateViewModel()
            {
                Cities = cities,
                States = _context.States.ToList()
            };





            return View("Index", viewModel);
        }



    }
}