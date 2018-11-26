using CoudTraining.Data.Models;
using System.Collections.Generic;

namespace CoudTraining.Data.Entities
{
    public class TrainingReport
    {
        public IEnumerable<CognitiveMeasure> CognitiveMeasures { get; set; }
        public IEnumerable<BiometricMeasure> BiometricMeasures { get; set; }
    }
}
