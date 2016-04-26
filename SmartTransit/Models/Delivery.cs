using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartTransit.Models
{
    public class Delivery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Invalid Delivery ID")]
        [MinLength(4)]
        [MaxLength(10)]
        public string DeliveryID { get; set; }

        public string ClientID { get; set; }
        public string DriverID { get; set; }
            
        public DateTime Date { get; set; }

        public string CurrentStatus { get; set; }
        [Required(ErrorMessage = "Invalid Type")]
        [MinLength(2)]
        [MaxLength(10)]
        public string Type { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(4)]
        [MaxLength(20)]
        public string PickUpLocation { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(4)]
        [MaxLength(20)]
        public string DeliverTo { get; set; }


        public virtual Client Client { get; set; }
        public virtual Driver Driver { get; set; }


        public virtual ICollection<LogHistory> LogsHistory { get; set; }

    }
}