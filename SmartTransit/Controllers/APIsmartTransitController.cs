using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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


            // Show All Post in Database
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

            [Route("ShowAllDeliveries")]
            [HttpGet]
            public IHttpActionResult AllDeliveris()
            {
                //fillingstations.OrderBy(ft => ft.PetrolPrice).Where(ft => ft.Eircode.StartsWith(postelcode)).Select(ft => new { ft.Eircode, ft.Location, ft.PetrolPrice });
                var clientRec = db.Deliveries.OrderBy(d => d.DeliveryID).Select(d => new { d.DeliveryID, d.CurrentStatus });
                //var clientRec = (from client in db.Clients
                //                 join delivery in db.Deliveries
                //                     on client.ClientID equals delivery.ClientID
                //                 where delivery.CurrentStatus.ToString() != "Delivered"
                //                 select (client.FirstName));

                if (clientRec == null)
                {
                    return NotFound();
                }
                return Ok(clientRec.ToList());

            }
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



        }
    }
}
