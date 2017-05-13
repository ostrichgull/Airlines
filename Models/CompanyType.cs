using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public enum CompanyTypeValue
    {
        Airline = 1,
        AircraftManufacturer = 2
    }

    public class CompanyType
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Type")]
        public string Name { get; set; }
    }
}