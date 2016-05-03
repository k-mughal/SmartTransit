using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SmartTransit.Models;
using SmartTransit.DataAccessLayer;
using System.Web.Http;

namespace SmartTransit.Controllers
{
    public class APIsmartTransitController : ApiController
    {


        [RoutePrefix("TransitWebServices")]
        public class TansitController : ApiController
        {
            private SmartTransitContext db = new SmartTransitContext();

            public TansitController()
            {
                db.Configuration.ProxyCreationEnabled = false;
            }


            //  /TransitWebServices/ShowTrackingHistory
            [Route("ShowTrackingHistory")]
            [HttpGet]
            public IHttpActionResult AllTrackingHistory()
            {

                //  var clientRec = db.Clients.Select(d => d.FirstName);
                return Ok(db.LogsHistory);

                //if (clientRec == null)
                //{
                //    return NotFound();
                //}
                //return Ok(clientRec.ToList());

            }

            //[Route("ShowAllDeliveries")]
            //[HttpGet]
            //public IHttpActionResult AllDeliveris()
            //{
            //    //fillingstations.OrderBy(ft => ft.PetrolPrice).Where(ft => ft.Eircode.StartsWith(postelcode)).Select(ft => new { ft.Eircode, ft.Location, ft.PetrolPrice });
            //    var clientRec = db.Deliveries.OrderBy(d => d.DeliveryID).Select(d => new { d.DeliveryID, d.CurrentStatus });
            //    //var clientRec = (from client in db.Clients
            //    //                 join delivery in db.Deliveries
            //    //                     on client.ClientID equals delivery.ClientID
            //    //                 where delivery.CurrentStatus.ToString() != "Delivered"
            //    //                 select (client.FirstName));

            //    if (clientRec == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(clientRec.ToList());

           // }
            //      /TransitWebServices/ShowLogHistory/UNQ1001
            [Route("ShowLogHistory/{ShowLogHistory}")]
            [HttpGet]
            public IHttpActionResult LogHistory(string ShowLogHistory)
            {
                var clientRec = (from logHistory in db.LogsHistory
                                 where logHistory.DeliveryID == ShowLogHistory
                                 orderby logHistory.ID
                                 select (logHistory));
                if (clientRec == null)
                {
                    return NotFound();
                }
                return Ok(clientRec.ToList());
            }

            //      /TransitWebServices/DriverOnJob/DR1001
         //   [Route("DriverOnJob/{DriverOnJob}")]
            [Route("DeliveryDetail")]
            [HttpGet]
            public IHttpActionResult ShowDriverOnJob()
            {

                var driverRec = (from driver in db.Drivers
                                 join delivery in db.Deliveries
                                 on driver.DriverID equals delivery.DriverID
                                 where delivery.CurrentStatus.ToString() != "Delivered"
                               //   && delivery.DriverID == DriverOnJob
                                 select (delivery));
                if (driverRec == null)
                {
                    return NotFound();
                }
                return Ok(driverRec);
            }
            // TransitWebServices/ShowDeliveries
            [Route("ShowDeliveries")]
            [HttpGet]
            public IHttpActionResult ShowDeliveries()
            {


                var deliveryRed = (from driver in db.Drivers
                                   join delivery in db.Deliveries
                                   on driver.DriverID equals delivery.DriverID
                                   //join client in db.Clients
                                   //on delivery.Client equals client.ClientID
                                //   where delivery.CurrentStatus.ToString() != "Delivered"

                                   select (delivery));
                if (deliveryRed == null)
                {
                    return NotFound();
                }
                return Ok(deliveryRed);
            }

        }
        //[Route("areacode/{areacode}/cheapest/{fueltype:alpha}")]
        //[Route("AddLogHistory/{AddLogHistory}")]
        //[Route("AddLogHistory")]
        //[HttpPost]
        //public IHttpActionResult PostAddLogHistory()
        //{
        //    var logrec = db.LogsHistory.FirstOrDefault(log => log.Status == "Delivered");//&& log.Status != "Delivered");
        //                                                                                 //if (logrec == null)
        //                                                                                 //{
        //                                                                                 //logrec.Status = status;

        //    db.LogsHistory.Add(logrec).DeliveryID = "UNQ1005";
        //    db.LogsHistory.Add(logrec).Status = "test status";
        //    db.SaveChanges();
        //    string uri = Url.Link("DefaultApi", new { ticker = logrec.DeliveryID });
        //    return Ok();

        //    //}
        //    //else
        //    //{
        //    //    return NotFound();
        //    //}
        }





    }


