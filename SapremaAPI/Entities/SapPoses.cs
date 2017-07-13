using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapPoses
    {
        public SapPoses()
        {
            SapClassPoses = new HashSet<SapClassPoses>();
        }

        public string PoseId { get; set; }
        public string PoseName { get; set; }
        public int PoseLevel { get; set; }
        public string PoseTheme { get; set; }

        public virtual ICollection<SapClassPoses> SapClassPoses { get; set; }
    }
}
