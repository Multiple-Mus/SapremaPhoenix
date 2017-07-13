using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapUserPoses
    {
        public string UserId { get; set; }
        public string PoseId { get; set; }
        public Guid UserPoseId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
