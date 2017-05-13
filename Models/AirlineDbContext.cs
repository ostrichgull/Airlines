using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airlines.Models
{
    public class AirlineDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }        
        public DbSet<AirlineAircraft> AirlineAircrafts { get; set; }
    }
}