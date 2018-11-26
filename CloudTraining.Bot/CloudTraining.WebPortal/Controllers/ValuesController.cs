using CoudTraining.Data.Entities;
using CoudTraining.Data.Models;
using CoudTraining.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.DataVisualization.Charting;

namespace CloudTraining.WebPortal.Controllers
{
    public class ValuesController : ApiController
    {
        CloudTrainingRepository repository = new CloudTrainingRepository();
        // GET api/values
        public TrainingReport Get()
        {
            return repository.GetTrainingMeasures();
            //return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
