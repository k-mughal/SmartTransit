using SmartTransit.DataAccessLayer;
using SmartTransit.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartTransit.Controllers
{
    public class LogHistoryController : Controller
    {
        private SmartTransitContext db = new SmartTransitContext();

        // GET: LogHistories
        public ActionResult Index()
        {
            //var logsHistory = db.LogsHistory.Include(l => l.Delivery);
            //return View(logsHistory.ToList());

            var logsHistory = db.Deliveries.OrderBy(d => d.Date); //Include(l => l.Delivery);
            return View(logsHistory.ToList());


            //var deliveries = db.Deliveries.OrderBy(d => d.DeliveryID).Where(d => d.CurrentStatus != "Delivered"); //.Include(d => d.Delivery);;
            //return View(deliveries.ToList());


        }

        // GET: LogHistories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //LogHistory logHistory = db.LogsHistory.Find(id);
            var logsHistory = db.LogsHistory.Where(d => d.DeliveryID == id).OrderBy(d => d.ID); //.Include(l => l.Delivery);
            if (logsHistory == null)
            {
                return HttpNotFound();
            }
            return View(logsHistory);
        }

        // GET: LogHistories/Create
        public ActionResult Create()
        {
            // ViewBag.DeliveryID = new SelectList(db.Deliveries, "DeliveryID", "ClientID");
            ViewBag.Status = new SelectList(LogHistory.StatusType);
            ViewBag.DeliveryID = new SelectList(db.Deliveries.Where(d => d.CurrentStatus != "Delivered").Select(d => d.DeliveryID));
            return View();
        }

        // POST: LogHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DeliveryID,Date,Status")] LogHistory logHistory)
        {
            if (ModelState.IsValid)
            {

                if (logHistory.Status == "Delivered")
                {
                    string id = logHistory.DeliveryID;

                    Delivery delivery = db.Deliveries.Find(id);

                    delivery.CurrentStatus = "Delivered";

                    db.Entry(delivery).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    string id = logHistory.DeliveryID;

                    Delivery delivery = db.Deliveries.Find(id);

                    delivery.CurrentStatus = logHistory.Status;

                    db.Entry(delivery).State = EntityState.Modified;
                    db.SaveChanges();
                }


                db.LogsHistory.Add(logHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeliveryID = new SelectList(db.Deliveries, "DeliveryID", "ClientID", logHistory.DeliveryID);
            return View(logHistory);
        }

        // GET: LogHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogHistory logHistory = db.LogsHistory.Find(id);
            if (logHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliveryID = new SelectList(db.Deliveries, "DeliveryID", "ClientID", logHistory.DeliveryID);
            return View(logHistory);
        }

        // POST: LogHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DeliveryID,Date,Status")] LogHistory logHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeliveryID = new SelectList(db.Deliveries, "DeliveryID", "ClientID", logHistory.DeliveryID);
            return View(logHistory);
        }

        // GET: LogHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogHistory logHistory = db.LogsHistory.Find(id);
            if (logHistory == null)
            {
                return HttpNotFound();
            }
            return View(logHistory);
        }

        // POST: LogHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogHistory logHistory = db.LogsHistory.Find(id);
            db.LogsHistory.Remove(logHistory);
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
