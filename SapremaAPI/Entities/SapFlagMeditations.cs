using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapFlagMeditations
    {
        public Guid FlagId { get; set; }
        public Guid MeditationId { get; set; }
        public Guid? MeditationReviewId { get; set; }
        public string UserId { get; set; }
        public string ReasonFlagged { get; set; }
        public string FlagComment { get; set; }
        public bool FlagResolved { get; set; }

        public virtual SapMeditations Meditation { get; set; }
        public virtual SapReviewMeditation MeditationReview { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
