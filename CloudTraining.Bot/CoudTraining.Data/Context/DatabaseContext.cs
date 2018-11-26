using CoudTraining.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoudTraining.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection") {

        }

        public DbSet<BiometricMeasure> BiometricMeasures { get; set; }
        public DbSet<CognitiveMeasure> CognitiveMeasures { get; set; }

    }
}
