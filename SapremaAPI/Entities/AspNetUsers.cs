using System;
using System.Collections.Generic;

namespace SapremaAPI.Entities
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            SapClass = new HashSet<SapClass>();
            SapClassComplete = new HashSet<SapClassComplete>();
            SapReviewClass = new HashSet<SapReviewClass>();
            SapReviewMeditation = new HashSet<SapReviewMeditation>();
            SapUserGroups = new HashSet<SapUserGroups>();
            SapUserMeditations = new HashSet<SapUserMeditations>();
            SapUserPoses = new HashSet<SapUserPoses>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<SapClass> SapClass { get; set; }
        public virtual ICollection<SapClassComplete> SapClassComplete { get; set; }
        public virtual ICollection<SapReviewClass> SapReviewClass { get; set; }
        public virtual ICollection<SapReviewMeditation> SapReviewMeditation { get; set; }
        public virtual SapTeachers SapTeachers { get; set; }
        public virtual ICollection<SapUserGroups> SapUserGroups { get; set; }
        public virtual ICollection<SapUserMeditations> SapUserMeditations { get; set; }
        public virtual ICollection<SapUserPoses> SapUserPoses { get; set; }
    }
}
