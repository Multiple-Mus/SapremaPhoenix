using System;
using System.Collections.Generic;

namespace SapremaIdentityServerASP.Entities
{
    public partial class SapFlagClasses
    {
        public Guid FlagId { get; set; }
        public Guid ClassId { get; set; }
        public Guid? ClassReviewId { get; set; }
        public string UserId { get; set; }
        public string ReasonFlagged { get; set; }
        public string FlagComment { get; set; }
        public bool FlagResolved { get; set; }

        public virtual SapClass Class { get; set; }
        public virtual SapReviewClass ClassReview { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
