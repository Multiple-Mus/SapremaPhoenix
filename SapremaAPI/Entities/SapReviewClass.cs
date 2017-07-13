using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapReviewClass
    {
        public Guid ReviewClassId { get; set; }
        public string UserId { get; set; }
        public Guid ClassId { get; set; }
        public decimal ReviewStars { get; set; }
        public string ReviewComment { get; set; }

        public virtual SapClass Class { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
