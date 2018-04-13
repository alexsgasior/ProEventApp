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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Save(State state)
        {
            //if (!ModelState.IsValid)
            //{
            //    var states = _context.States.ToList();
            //    return View("StateForm");
            //}


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


        
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult New()
        {
            var states = _context.States.ToList();
           
            return View("StateForm");
        }


        [Route("States/Edit/id")]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            var state = _context.States.SingleOrDefault(p => p.Id == id);
            if (state == null)
            {
                return HttpNotFound();
            }


            return View("StateForm", state);
        }



        
        [Authorize(Roles = RoleName.Admin)]
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
        
    }
}