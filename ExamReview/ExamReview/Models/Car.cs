using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReview.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerID { get; set; }
    }
}
