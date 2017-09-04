using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class GroupStore
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupLevel { get; set; }
        public bool GroupJoined { get; set; }
        public string GroupAdmin {get; set;}
    }
}
