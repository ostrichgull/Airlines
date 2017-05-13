using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airlines.Models;
using Airlines.Common;
using PagedList;

namespace Airlines.Controllers
{
    public class AircraftController : Controller
    {
        private AirlineDbContext db = new AirlineDbContext();

        // GET: Aircraft
        public ActionResult Index(int? id, int? page)
        {
            var aircrafts = db.Aircrafts
                                .Include(a => a.Company)
                                .OrderBy(a => a.Company.Name).ThenBy(a => a.Name);

            ViewBag.Page = page;

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(aircrafts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Aircraft/Details/5
        public ActionResult Details(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aircraft aircraft = db.Aircrafts.Find(id);

            if (aircraft == null)
            {
                return HttpNotFound();
            }

            ViewBag.Page = page;

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public ActionResult Create(int? id, int? page)
        {
            IQueryable<Company> companies = db.Companies
                                            .Where(a => a.CompanyTypeID == (int?)CompanyTypeValue.AircraftManufacturer);

            ViewBag.CompanyID = new SelectList(companies, "ID", "Name");
            ViewBag.Page = page;

            return View();
        }

        // POST: Aircraft/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyID,Name,Description,Capacity")] Aircraft aircraft, HttpPostedFileBase airplane, int? id, int? page)
        {
            if (ModelState.IsValid)
            {
                if (airplane != null)
                    aircraft.Airplane = Utility.GetImage(airplane);

                db.Aircrafts.Add(aircraft);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = 0, page = page });
            }

            IQueryable<Company> companies = db.Companies
                                            .Where(a => a.CompanyTypeID == (int?)CompanyTypeValue.AircraftManufacturer);

            ViewBag.CompanyID = new SelectList(companies, "ID", "Name", aircraft.CompanyID);
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        public ActionResult Edit(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aircraft aircraft = db.Aircrafts.Find(id);

            if (aircraft == null)
            {
                return HttpNotFound();
            }

            IQueryable<Company> companies = db.Companies
                                            .Where(a => a.CompanyTypeID == (int?)CompanyTypeValue.AircraftManufacturer);

            ViewBag.CompanyID = new SelectList(companies, "ID", "Name", aircraft.CompanyID);
            ViewBag.Page = page;

            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyID,Name,Description,Capacity")] Aircraft aircraft, HttpPostedFileBase airplane, int? id, int? page)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aircraft).State = EntityState.Modified;

                if (airplane != null)
                    aircraft.Airplane = Utility.GetImage(airplane);
                else
                    db.Entry(aircraft).Property(m => m.Airplane).IsModified = false;

                db.SaveChanges();
                return RedirectToAction("Index", new { id = id, page = page });
            }

            IQueryable<Company> companies = db.Companies
                                            .Where(a => a.CompanyTypeID == (int?)CompanyTypeValue.AircraftManufacturer);

            ViewBag.CompanyID = new SelectList(companies, "ID", "Name", aircraft.CompanyID);
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        public ActionResult Delete(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aircraft aircraft = db.Aircrafts.Find(id);

            if (aircraft == null)
            {
                return HttpNotFound();
            }

            ViewBag.Page = page;

            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? page)
        {
            Aircraft aircraft = db.Aircrafts.Find(id);

            db.Aircrafts.Remove(aircraft);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = id, page = page });
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
