using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class FlaggedListModel
    {
        public Guid FlagId { get; set; }
        public Guid ItemId { get; set; }
        public Guid? ReviewId { get; set; }
        public string FlagType { get; set; }
        public bool FlagResolved { get; set; }
    }
}
