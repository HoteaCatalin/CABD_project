using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.CABD.Models
{
    public class CarStatistics
    {
        public int MinPret { get; set; }

        public double MinDurata { get; set; }

        public List<KeyValuePair<int, DateTime>> Variatii { get; set; }
    }
}