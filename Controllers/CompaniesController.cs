using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Airlines.Models;
using Airlines.Common;
using PagedList;
using System.Threading.Tasks;

namespace Airlines.Controllers
{
    public class CompaniesController : Controller
    {
        private AirlineDbContext db = new AirlineDbContext();
        
        // GET: Airlines
        public ActionResult Index(int? id, int? page, int? typeId)
        {
            typeId = (typeId ?? (int)CompanyTypeValue.Airline);
            page = (page ?? 1);

            var companies = db.Companies
                                .Include(c => c.Country)
                                .Where(c => c.CompanyTypeID == typeId)
                                .OrderBy(c => c.Name);

            ViewBag.Aircraft = db.AirlineAircrafts.ToList();
            ViewBag.AircraftList = db.Aircrafts.ToList();
            
            ViewBag.Title = Utility.GetCompanyTypeName(typeId);
            ViewBag.TypeId = typeId;
            ViewBag.Page = page;

            int pageSize = 4;
            int pageNumber = (int)page;

            return View(companies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Airlines/Details/5
        public ActionResult Details(int? id, int? page, int? typeId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = Utility.GetCompanyTypeName(typeId);
            ViewBag.TypeId = typeId;
            ViewBag.Page = page;

            return View(company);
        }

        // GET: Airlines/Create
        public ActionResult Create(int? id, int? page, int? typeId)
        {
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name");
            ViewBag.StateID = new SelectList(db.States, "ID", "Name");
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "Name", typeId);

            ViewBag.Title = Utility.GetCompanyTypeName(typeId);
            ViewBag.TypeId = typeId;
            ViewBag.Page = page;

            return View();
        }

        // POST: Airlines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CountryID,StateID,CompanyTypeID")] Company company, HttpPostedFileBase logo, int? id, int? page, int? typeId)
        {            
            company.CompanyTypeID = typeId;

            if (ModelState.IsValid)
            {
                if (logo != null)
                    company.Logo = Utility.GetImage(logo);

                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id, page = page, typeId = typeId });
            }

            ViewBag.StateID = new SelectList(db.States, "ID", "Name");
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", company.CountryID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "Name", company.CompanyTypeID);

            return View(company);
        }

        // GET: Airlines/Edit/5
        public ActionResult Edit(int? id, int? page, int? typeId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            var states = db.States.Where(s => s.CountryID == company.CountryID);

            ViewBag.StateID = new SelectList(states, "ID", "Name", company.StateID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", company.CountryID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "Name", company.CompanyTypeID);

            ViewBag.Page = page;
            ViewBag.Title = Utility.GetCompanyTypeName(typeId);
            ViewBag.TypeId = typeId;

            return View(company);
        }

        // POST: Airlines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CountryID,StateID,CompanyTypeID")] Company company, HttpPostedFileBase logo, int? id, int? page, int? typeId)
        {            
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;

                if (logo != null)
                    company.Logo = Utility.GetImage(logo);
                else
                    db.Entry(company).Property(m => m.Logo).IsModified = false;

                db.SaveChanges();

                
                return RedirectToAction("Index", new { id = id, page = page, typeId = typeId });
            }

            var states = db.States.Where(s => s.CountryID == company.CountryID);

            ViewBag.StateID = new SelectList(states, "ID", "Name", company.StateID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Name", company.CountryID);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "ID", "Name", company.CompanyTypeID);

            return View(company);
        }

        // GET: Airlines/Delete/5
        public ActionResult Delete(int? id, int? page, int? typeId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = Utility.GetCompanyTypeName(typeId);
            ViewBag.TypeId = typeId;
            ViewBag.Page = page;

            return View(company);
        }

        // POST: Airlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? page, int? typeId)
        {            
            Company company = db.Companies.Find(id);

            db.Companies.Remove(company);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = id, page = page, typeId = typeId });
        }

               
        public  ActionResult FillState(int countryId)
        {
            var states = db.States
                                .Select(s => new { s.ID, s.Name, s.CountryID })
                                .Where(s => s.CountryID == countryId)
                                .ToList();

            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAircraft(int? id, int? page, int? typeId, int? aircraftId)
        {
            db.AirlineAircrafts.Add(new AirlineAircraft { CompanyID = (int)HttpContext.Session["SelectedCompanyId"], AircraftID = aircraftId });
            db.SaveChanges();

            return RedirectToAction("Index", new { id = id, page = page, typeId = typeId });
        }

        public ActionResult DeleteAircraft(int? id, int? page, int? typeId, int? aircraftId)
        {
            AirlineAircraft airlineAircraft = db.AirlineAircrafts.Find(aircraftId);

            db.AirlineAircrafts.Remove(airlineAircraft);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = id, page = page, typeId = typeId });
        }

        [HttpPost]
        public void SelectedCompany(int companyId)
        {
            HttpContext.Session["SelectedCompanyId"] = companyId;
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
