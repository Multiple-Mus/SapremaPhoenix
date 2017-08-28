using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SapremaAPI.Models
{
    public class MeditationStoreModel
    {
        public string MeditationId { get; set; }
        public string MeditationName { get; set; }
        public string MeditationTheme { get; set; }
        public string MeditationCreator { get; set; }
        public string MeditationImage { get; set; }
        public string MeditationType { get; set; }
        public decimal MeditationRating { get; set; }
        public bool MeditationPurchased { get; set; }
    }
}
