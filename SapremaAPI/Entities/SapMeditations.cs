using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapMeditations
    {
        public SapMeditations()
        {
            SapReviewMeditation = new HashSet<SapReviewMeditation>();
            SapUserMeditations = new HashSet<SapUserMeditations>();
        }

        public Guid? MeditationId { get; set; }
        public string MeditationName { get; set; }
        public string MeditationTheme { get; set; }
        public string MeditationCreator { get; set; }
        public Guid? MeditationImage { get; set; }
        public string MeditationType { get; set; }

        public virtual ICollection<SapReviewMeditation> SapReviewMeditation { get; set; }
        public virtual ICollection<SapUserMeditations> SapUserMeditations { get; set; }
    }
}
