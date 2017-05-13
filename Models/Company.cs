using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public class Company
    {
        [Key]        
        public int ID { get; set; }

        [StringLength(256)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        [StringLength(256)]
        [Display(Name = "Company Description")]
        public string Description { get; set; }

        [ForeignKey("Country")]
        [Display(Name = "Country Name")]        
        public int? CountryID { get; set; }

        [ForeignKey("State")]
        [Display(Name = "State Name")]
        public int? StateID { get; set; }

        [ForeignKey("CompanyType")]
        [Display(Name = "Company Type")]        
        public int? CompanyTypeID { get; set; }

        public byte[] Logo { get; set; }

        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual CompanyType CompanyType { get; set; }
    }
}