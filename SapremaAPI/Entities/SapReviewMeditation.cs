using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapReviewMeditation
    {
        public Guid ReviewMeditationId { get; set; }
        public string UserId { get; set; }
        public Guid MeditationId { get; set; }
        public decimal ReviewStars { get; set; }
        public string ReviewComment { get; set; }

        public virtual SapMeditations Meditation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
