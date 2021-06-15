using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyJobBoard.DATA.EF;
using Microsoft.AspNet.Identity;
using MyJobBoard.UI.MVC.Models;

namespace MyJobBoard.UI.MVC.Controllers
{
    public class OpenPositionsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: OpenPositions
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                var currentUser = User.Identity.GetUserId();

                var openPositions = db.OpenPositions.Where(x => x.Location.ManagerId == currentUser).Include(o => o.Location).Include(o => o.Position);
               
               

                return View(openPositions.ToList());
            }
            if (Request.IsAuthenticated && User.IsInRole("Employee"))
            {

            
            
                var currentUser = User.Identity.GetUserId();
                var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
                var applications = db.Applications.Where(x => x.UserId == currentUser);
                List<OpenPosition> op = db.OpenPositions.ToList();
                foreach (var o in op)
                {


                    foreach (var a in applications)
                    {
                        if (o.OpenPositionId == a.OpenPositionId)
                        {
                            o.HasApplied = true;
                        }
                    }

                }
               
                

                return View(op.ToList());
            }

            else
            {
                var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
                return View(openPositions.ToList());
            }
        }

        
  

        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }

            var currentUser = User.Identity.GetUserId();
            var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);
            var applications = db.Applications.Where(x => x.UserId == currentUser);
            List<OpenPosition> op = db.OpenPositions.ToList();
            foreach (var o in op)
            {


                foreach (var a in applications)
                {
                    if (o.OpenPositionId == a.OpenPositionId)
                    {
                        o.HasApplied = true;
                    }
                }

            }



            return View(openPosition);
        }

        // GET: OpenPositions/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber");
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title");
            return View();
        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionId,PositionId,LocationId,Pay")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        public ActionResult Apply(int id)
        {
            var currentUser = User.Identity.GetUserId();

            Application a = new Application();
            a.OpenPositionId = id;
       
            a.UserId = currentUser;
            a.ApplicationDate = DateTime.Now;
            a.ApplicationStatus = 1;
            UserDetail ud = db.UserDetails.Where(x => x.UserId == currentUser).FirstOrDefault();
            a.ResumeFileName = ud.ResumeFileName;
           
  
                db.Applications.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
  




          
        }

        // GET: OpenPositions/Edit/5
        [Authorize(Roles ="Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionId,PositionId,LocationId,Pay")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "StoreNumber", openPosition.LocationId);
            ViewBag.PositionId = new SelectList(db.Positions, "PositionId", "Title", openPosition.PositionId);
            return View(openPosition);
        }

        // GET: OpenPositions/Delete/5
        [Authorize(Roles ="Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
