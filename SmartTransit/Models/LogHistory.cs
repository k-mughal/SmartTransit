using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartTransit.Models
{
    public class LogHistory
    {
        public int ID { get; set; }

        public string DeliveryID { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public virtual Delivery Delivery { get; set; }
    }
}