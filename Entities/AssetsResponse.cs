using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Models
{
    public class AssetsResponse
    {
        public string date { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double average { get; set; }

  
        public AssetsResponse(string date,string dateRow)
        {
            this.date = date;
            ApplyDate(dateRow);
        }

        private void ApplyDate(string dateRow)
        {
            try
            {
                Dictionary<string, double> ratesDic = null;
                ratesDic = JsonConvert.DeserializeObject<Dictionary<string, double>>(dateRow);

                min = ratesDic.Values.Min();
                max = ratesDic.Values.Max();
                average = ratesDic.Values.Average();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
