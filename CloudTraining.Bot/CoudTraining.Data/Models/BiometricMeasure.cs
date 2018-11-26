using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoudTraining.Data.Models
{
    public class BiometricMeasure
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BiometricMeasureId { get; set; }
        public DateTime Creation { get; set; }
        public string MeasureName { get; set; }
        public double Value { get; set; }
    }
}
