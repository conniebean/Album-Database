using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamReview.Models
{
    public class Part
    {
        public int PartID { get; set; }
        public string PartName { get; set; }
        public int ItemsInStock { get; set; }

        public double Price { get; set; }

        public Car Car { get; set; }
        public int CarID { get; set; }

    }
}
