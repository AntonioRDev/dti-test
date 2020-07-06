using System;

namespace ProvaDTI.Models
{
    public class Query
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public DateTime DateTime { get; set; }
        public double Weight { get; set; }
        public double FatPercentage { get; set; }
        public string FisicSensation { get; set; }
        public string FoodRestritions { get; set; }

        public Query()
        {
            DateTime = DateTime.Now;
        }
    }
}
