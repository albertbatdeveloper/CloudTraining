using CoudTraining.Data.Context;
using CoudTraining.Data.Entities;
using CoudTraining.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoudTraining.Data.Repositories
{

    public class CloudTrainingRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public void InsertCognitiveMeasure(string name, double value, string comments = "")
        {
            var measure = new CognitiveMeasure() { Creation = DateTime.UtcNow, MeasureName = name, Value = value, Comments = comments };
            db.CognitiveMeasures.Add(measure);
            db.SaveChanges();
        }

        public TrainingReport GetTrainingMeasures()
        {
            var result = new TrainingReport();
            result.CognitiveMeasures = db.CognitiveMeasures.OrderByDescending(m => m.Creation).Take(10).OrderBy((m => m.Creation)).ToList();
            result.BiometricMeasures = db.BiometricMeasures.OrderByDescending(m => m.Creation).Take(240).OrderBy((m => m.Creation)).ToList();
            return result;
        }
    }
}
