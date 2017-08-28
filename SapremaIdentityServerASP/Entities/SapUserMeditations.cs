using System;
using System.Collections.Generic;

namespace SapremaIdentityServerASP.Entities
{
    public partial class SapUserMeditations
    {
        public Guid UserMeditationsId { get; set; }
        public string UserId { get; set; }
        public Guid MeditationId { get; set; }

        public virtual SapMeditations Meditation { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
