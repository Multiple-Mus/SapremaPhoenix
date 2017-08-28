using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class ClassInfoModel
    {
        public string ClassCreatedBy { get; set; }
        public string ClassName { get; set; }
        public int ClassLevel { get; set; }
        public string ClassTheme { get; set; }
        public string ClassDescription { get; set; }
        public Guid ClassId { get; set; }
        public Guid GroupId { get; set; }
    }
}
