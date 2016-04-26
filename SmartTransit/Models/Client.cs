using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartTransit.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required(ErrorMessage = "Invalid Client ID")]
        [MinLength(4)]
        [MaxLength(10)]
        public string ClientID { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Invalid")]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(6)]
        [MaxLength(18)]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(6)]
        [MaxLength(25)]
        public string Address { get; set; }
     
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}