using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class FlaggedModel
    {
        public Guid FlagId { get; set; }
        public Guid ItemId { get; set; }
        public Guid? ReviewId { get; set; }
        public string FlaggedBy { get; set; }
        public string ReasonFlagged { get; set; }
        public string FlagComment { get; set; }
        public string FlagType { get; set; }
        public bool FlagResolved { get; set; }
    }
}
