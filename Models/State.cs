using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public class State
    {
        [Key]
        public int ID { get; set; }

        [StringLength(256)]
        [Display(Name = "State Name")]
        public string Name { get; set; }

        [ForeignKey("Country")]
        [Display(Name = "Country Name")]
        public int? CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}