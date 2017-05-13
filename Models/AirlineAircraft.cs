using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public class AirlineAircraft
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Company")]
        [Display(Name = "Company Name")]
        public int? CompanyID { get; set; }

        [ForeignKey("Aircraft")]
        [Display(Name = "Aircraft Name")]
        public int? AircraftID { get; set; }

        public string CompanyName { get { return this.Aircraft.Company.Name; } }
        public string AircraftName { get { return this.Aircraft.Name; } }
        public string Description { get { return this.Aircraft.Description; } }
        public int Capacity { get { return this.Aircraft.Capacity; } }

        public virtual Company Company { get; set; }
        public virtual Aircraft Aircraft { get; set; }
    }
}