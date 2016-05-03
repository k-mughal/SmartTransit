using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartTransit.Models
{
    public class LogHistory
    {

        public static String[] StatusType
        {
            get
            {
                return new String[] { "Dispatched", "On the way to destination", "Delayed", "Delivered", "Undelivered" };
            }
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "Invalid Delivery ID")]
        [MinLength(4)]
        [MaxLength(10)]
        public string DeliveryID { get; set; }

        [Required(ErrorMessage = "Required ! ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public virtual Delivery Delivery { get; set; }
    }
}