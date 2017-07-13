using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapTeachers
    {
        public SapTeachers()
        {
            SapGroups = new HashSet<SapGroups>();
        }

        public string TeachId { get; set; }
        public string Studio { get; set; }
        public string Cert { get; set; }
        public string Bio { get; set; }
        public string Site { get; set; }
        public bool Verified { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<SapGroups> SapGroups { get; set; }
        public virtual AspNetUsers Teach { get; set; }
    }
}
