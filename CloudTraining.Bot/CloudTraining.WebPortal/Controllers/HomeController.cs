using CloudTraining.WebPortal.Models;
using CoudTraining.Data.Entities;
using CoudTraining.Data.Models;
using CoudTraining.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace CloudTraining.WebPortal.Controllers
{
    public class HomeController : Controller
    {
        CloudTrainingRepository repository = new CloudTrainingRepository();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var result = new ChartResult();
            var measures = repository.GetTrainingMeasures();
            result.BiometricUrl = GetBiometricChart(measures.BiometricMeasures);
            result.CognitiveUrl = GetCognitiveChart(measures.CognitiveMeasures);

            return View(result);
        }

        private string GetCognitiveChart(IEnumerable<CognitiveMeasure> measures)
        {
            try
            {
                var chart = new Chart { Height = 350, Width = 500, Titles = { new Title("Opinion") } };

                foreach (var measure in measures)
                {
                    Series series = chart.Series.FirstOrDefault(s => s.Name == measure.MeasureName);
                    if (series == null)
                    {
                        series = new Series(measure.MeasureName) { ChartType = SeriesChartType.Column,  IsValueShownAsLabel = true, IsVisibleInLegend = true };
                        chart.Series.Add(series);
                      
                    }
                    series.Points.AddXY(measure.Creation, Math.Round(measure.Value * 10,2));
                }
                var legend = new Legend("Legend");
                chart.Legends.Add(legend);
                var area = new ChartArea("Area");
                chart.ChartAreas.Add(area);


                // Save it to a stream
                var imageStream = new System.IO.MemoryStream();
                chart.SaveImage(imageStream, ChartImageFormat.Png);

                // Convert stream to base64 string
                var base64 = Convert.ToBase64String(imageStream.ToArray());

                // Return base64 string prefixed with the relevant data URL parameters
                return $"data:image/png;base64,{base64}";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetBiometricChart(IEnumerable<BiometricMeasure> measures)
        {
            try
            {
                var chart = new Chart { Height = 350, Width = 500, Titles = { new Title("Pulso") } };
                var series = new Series("Pulse") { ChartType = SeriesChartType.Line, IsValueShownAsLabel = false };
                chart.Series.Add(series);
                foreach (var measure in measures)
                {
                    series.Points.AddXY(measure.Creation, measure.Value);
                }

                var area = new ChartArea("Area");
                chart.ChartAreas.Add(area);


                // Save it to a stream
                var imageStream = new System.IO.MemoryStream();
                chart.SaveImage(imageStream, ChartImageFormat.Png);

                // Convert stream to base64 string
                var base64 = Convert.ToBase64String(imageStream.ToArray());

                // Return base64 string prefixed with the relevant data URL parameters
                return $"data:image/png;base64,{base64}";
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
