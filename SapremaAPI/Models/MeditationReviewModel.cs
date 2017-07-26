using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class MeditationReviewModel
    {
        public Guid? MeditationReviewId { get; set; }
        public string userId { get; set; }
        public Guid MeditationId { get; set; }
        public decimal ReviewStars { get; set; }
        public string ReviewComment { get; set; }
    }
}
