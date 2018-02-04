using ProEventApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProEventApp.Controllers
{
    public class StatesController : Controller
    {
        private ApplicationDbContext _context;

        public StatesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpPost]
        public ActionResult Save(State state)
        {
            if (state.Id == 0)
            {
                _context.States.Add(state);
            }
            else
            {
                var stateInDb = _context.States.Single(p => p.Id == state.Id);

                stateInDb.Name = state.Name;


            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Cities");
        }



        public ActionResult New()
        {
            var states = _context.States.ToList();
            //var viewModel = new ProfessionFormViewModel
            //{
            //    Categories = categories
            //};


            return View("StateForm");
        }


        [Route("States/Edit/id")]
        public ActionResult Edit(int id)
        {
            var state = _context.States.SingleOrDefault(p => p.Id == id);
            if (state == null)
            {
                return HttpNotFound();
            }


            return View("StateForm", state);
        }



        public ActionResult Delete(int id)
        {
            var state = _context.States.SingleOrDefault(p => p.Id == id);
            if (state == null)
            {
                return HttpNotFound();
            }

            _context.States.Remove(state);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cities");

        }




        // GET: Categories
        //public ActionResult _StatesListShared() //Index
        //{
        //    var states = _context.States.ToList();
        //    return PartialView(states);  // Index = _StatesList
        //}
    }
}