using System;
using System.Collections.Generic;

namespace SapremaIdentityServerASP.Entities
{
    public partial class SapReviewMeditation
    {
        public SapReviewMeditation()
        {
            SapFlagMeditations = new HashSet<SapFlagMeditations>();
        }

        public Guid ReviewMeditationId { get; set; }
        public string UserId { get; set; }
        public Guid MeditationId { get; set; }
        public decimal ReviewStars { get; set; }
        public string ReviewComment { get; set; }

        public virtual ICollection<SapFlagMeditations> SapFlagMeditations { get; set; }
        public virtual SapMeditations Meditation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
