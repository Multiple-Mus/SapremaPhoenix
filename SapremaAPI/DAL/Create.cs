using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SapremaAPI.Entities;
using SapremaAPI.Models;

namespace SapremaAPI.DAL
{
    public class Create
    {
        public bool CreateMeditation(SapMeditations meditation)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                meditation.MeditationId = Guid.NewGuid();
                dbConn.SapMeditations.Add(meditation);
                dbConn.SaveChanges();

                return true;
            }
        }

        public bool CreateMeditationReview(SapReviewMeditation meditationReview)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                meditationReview.ReviewMeditationId = Guid.NewGuid();
                dbConn.SapReviewMeditation.Add(meditationReview);
                dbConn.SaveChanges();

                return true;
            }
        }
    }
}
