using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class GroupModel
    {
        public Guid GroupId { get; set; }
        public string GroupStatus { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupAdmin { get; set; }
        public string GroupLevel { get; set; }
    }
}
