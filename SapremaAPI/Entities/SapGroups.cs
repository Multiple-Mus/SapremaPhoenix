using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapGroups
    {
        public SapGroups()
        {
            SapClass = new HashSet<SapClass>();
            SapUserGroups = new HashSet<SapUserGroups>();
        }

        public Guid GroupId { get; set; }
        public bool GroupStatus { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupAdmin { get; set; }
        public int GroupLevel { get; set; }

        public virtual ICollection<SapClass> SapClass { get; set; }
        public virtual ICollection<SapUserGroups> SapUserGroups { get; set; }
        public virtual SapTeachers GroupAdminNavigation { get; set; }
    }
}
