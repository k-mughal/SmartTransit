using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartTransit.Models
{
    public class Driver
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Invalid Delivery ID")]
        [MinLength(4)]
        [MaxLength(10)]
        public string DriverID { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(2)]
        [MaxLength(25)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Invalid")]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Invalid")]
        [MinLength(6)]
        [MaxLength(15)]
        public string PhoneNo { get; set; }


        [Required(ErrorMessage = "Invalid")]
        [MinLength(4)]
        [MaxLength(25)]
        public string Address { get; set; }

        public string LoginStatus { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
