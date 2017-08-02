using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class ReviewModel
    {
        public Guid ReviewId { get; set; }
        public string userId { get; set; }
        public Guid ItemId { get; set; }
        public decimal ReviewStars { get; set; }
        public string ReviewComment { get; set; }
        public string ReviewType { get; set; }
    }
}
