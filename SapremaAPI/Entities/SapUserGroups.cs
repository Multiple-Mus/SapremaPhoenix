using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class SapUserGroups
    {
        public Guid UserGroupId { get; set; }
        public string UserId { get; set; }
        public Guid GroupId { get; set; }

        public virtual SapGroups Group { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
