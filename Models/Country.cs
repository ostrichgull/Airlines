using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public class Country
    {
        public int ID { get; set; }

        [StringLength(256)]
        [Display(Name = "Country Name")]
        public string Name { get; set; }
    }
}