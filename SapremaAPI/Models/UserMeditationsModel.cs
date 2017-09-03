using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class UserMeditationsModel
    {
        public Guid MeditationId { get; set; }
        public string MeditationCreator { get; set; }
        public string MeditationName { get; set; }
    }
}
