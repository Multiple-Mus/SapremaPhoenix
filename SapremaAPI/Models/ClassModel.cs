using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class ClassModel
    {
        public Guid ClassId { get; set; }
        public string ClassCreatedBy { get; set; }
        public string ClassName { get; set; }
        public int ClassLevel { get; set; }
        public string ClassTheme { get; set; }
        public string ClassDescription { get; set; }
        public Guid ClassGroupId { get; set; }
        public List<PoseModel> Poses { get; set; }
    }
}
