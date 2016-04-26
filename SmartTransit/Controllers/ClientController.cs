using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartTransit.DataAccessLayer;
using SmartTransit.Models;

namespace SmartTransit.Controllers
{
    public class ClientController : Controller
    {
        private SmartTransitContext db = new SmartTransitContext();
        public ActionResult Index(string sortOrder, string searchString)
        {

            if (sortOrder != null && searchString != null)
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.PhoneSortParm = sortOrder == "PhoneNo" ? "phoneNo_desc" : "PhoneNo";
                var clients = (from c in db.Clients
                               select c).Take(10);

                if (!String.IsNullOrEmpty(searchString))
                {
                    clients = clients.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstName.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        clients = clients.OrderByDescending(s => s.LastName);
                        break;
                    case "PhoneNo":
                        clients = clients.OrderBy(s => s.PhoneNo);
                        break;
                    case "phoneNo_desc":
                        clients = clients.OrderByDescending(s => s.PhoneNo);
                        break;
                    default:
                        clients = clients.OrderBy(s => s.LastName);
                        break;
                }

                return View(clients.ToList());
            }
            else
            {
                return View(db.Clients.ToList());
            }

        }

        // GET: Clients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,FirstName,LastName,PhoneNo,Address")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client).ClientID =  "CL"+ client.ClientID;
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,FirstName,LastName,PhoneNo,Address")] Client client)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
