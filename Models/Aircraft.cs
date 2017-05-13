using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public class Aircraft
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Company")]
        [Display(Name = "Manufacturer Name")]
        public int CompanyID { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Aircraft Model")]
        public string Name { get; set; }

        [StringLength(256)]
        [Display(Name = "Aircraft Description")]
        public string Description { get; set; }

        [Display(Name = "Seating Capacity")]
        public int Capacity { get; set; }

        [Display(Name = "Image")]
        public byte[] Airplane { get; set; }

        public string CompanyName { get { return this.Company.Name; } }

        public virtual Company Company { get; set; }
    }
}