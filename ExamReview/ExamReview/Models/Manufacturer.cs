using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReview.Models
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
