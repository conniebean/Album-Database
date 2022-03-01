using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumsApp.Models
{
    public class Studio
    {
        public int StudioId { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [Required]
        [Url(ErrorMessage ="Must be a valid URL")]
        public string Url { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"(^\d{5}$)", ErrorMessage = "Must be in proper US Zip Code format: 90210")]
        public string ZipCode { get; set; }
    }
}
