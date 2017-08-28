using System;
using System.Collections.Generic;

namespace SapremaIdentityServerASP.Entities
{
    public partial class SapClass
    {
        public SapClass()
        {
            SapClassPoses = new HashSet<SapClassPoses>();
            SapFlagClasses = new HashSet<SapFlagClasses>();
            SapReviewClass = new HashSet<SapReviewClass>();
        }

        public Guid ClassId { get; set; }
        public string ClassCreatedBy { get; set; }
        public string ClassName { get; set; }
        public int? ClassLevel { get; set; }
        public string ClassTheme { get; set; }
        public string ClassDescription { get; set; }
        public Guid? ClassGroupId { get; set; }
        public DateTime ClassCreatedOn { get; set; }
        public DateTime? ClassModifiedOn { get; set; }

        public virtual SapClassComplete SapClassComplete { get; set; }
        public virtual ICollection<SapClassPoses> SapClassPoses { get; set; }
        public virtual ICollection<SapFlagClasses> SapFlagClasses { get; set; }
        public virtual ICollection<SapReviewClass> SapReviewClass { get; set; }
        public virtual AspNetUsers ClassCreatedByNavigation { get; set; }
        public virtual SapGroups ClassGroup { get; set; }
    }
}
