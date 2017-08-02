using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class TeacherAdminModel
    {
        public string TeacherId { get; set; }
        public bool VerifiedStatus { get; set; }
        public string TeacherName { get; set; }
    }
}
