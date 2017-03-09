using SmartTransit.DataAccessLayer;
using SmartTransit.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartTransit.Controllers
{
    public class DriverController : Controller
    {
        private SmartTransitContext db = new SmartTransitContext();

        // GET: Drivers
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (sortOrder != null && searchString != null)
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.PhoneSortParm = sortOrder == "PhoneNo" ? "phoneNo_desc" : "PhoneNo";
                var drivers = (from d in db.Drivers
                               select d).Take(10);
                if (!String.IsNullOrEmpty(searchString))
                {
                    drivers = drivers.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        drivers = drivers.OrderByDescending(s => s.LastName);
                        break;
                    case "PhoneNo":
                        drivers = drivers.OrderBy(s => s.PhoneNo);
                        break;
                    case "phoneNo_desc":
                        drivers = drivers.OrderByDescending(s => s.PhoneNo);
                        break;
                    default:
                        drivers = drivers.OrderBy(s => s.LastName);
                        break;
                }
                return View(drivers.ToList());
            }
            else
            {

            }


            return View(db.Drivers.ToList());
        }

        // GET: Drivers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverID,FirstName,LastName,PhoneNo,Address")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Drivers.Add(driver).DriverID = "DR" + driver.DriverID;
                driver.LoginStatus = "YES";
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(driver);
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverID,FirstName,LastName,PhoneNo,Address")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
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
