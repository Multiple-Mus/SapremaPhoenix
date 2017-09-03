using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class UserPosesModel
    {
        public string PoseId { get; set; }
        public string PoseName { get; set; }
        public int PoseLevel { get; set; }
        public string PoseTheme { get; set; }
        public bool PoseOmit { get; set; }
    }
}
