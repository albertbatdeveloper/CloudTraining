using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoudTraining.Data.Models
{
    public class CognitiveMeasure
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CognitiveMeasureId { get; set; }
        public DateTime Creation { get; set; }
        public string MeasureName { get; set; }
        public double Value { get; set; }
        public string Comments { get; set; }
    }
}
