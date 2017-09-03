using System;
using System.Collections.Generic;

namespace SapremaIdentityServerASP.Entities
{
    public partial class SapClassPoses
    {
        public Guid ClassPoseId { get; set; }
        public Guid ClassId { get; set; }
        public string PoseId { get; set; }
        public int PoseTime { get; set; }
        public int SequenceNumber { get; set; }

        public virtual SapClass Class { get; set; }
        public virtual SapPoses Pose { get; set; }
    }
}
