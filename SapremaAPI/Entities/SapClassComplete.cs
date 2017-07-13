using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapClassComplete
    {
        public Guid ClassCompleteId { get; set; }
        public Guid ClassId { get; set; }
        public string UserId { get; set; }
        public DateTime ClassCompletedOn { get; set; }

        public virtual SapClass ClassComplete { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
