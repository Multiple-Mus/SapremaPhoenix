using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaClient.Models
{
    public class GroupModel
    {
        public Guid GroupId { get; set; }
        public bool GroupStatus { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupAdmin { get; set; }
        public int GroupLevel { get; set; }
    }
}
