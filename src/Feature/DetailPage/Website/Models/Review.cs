using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltudoBtc1.Feature.DetailPage.Models
{
    public class Review
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ReviewComments { get; set; }

        public DateTime PostedDate { get; set; }
        public string Rating { get; set; }
    }
}