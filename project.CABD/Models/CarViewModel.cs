using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.CABD.Models
{
    public class CarViewModel
    {
        public IEnumerable<Car> Cars { get; set; }

        public CarStatistics Stats { get; set; }
    }
}