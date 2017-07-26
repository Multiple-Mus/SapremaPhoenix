using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SapremaAPI.Entities;

namespace SapremaAPI.DAL
{
    public class Delete
    {
        public bool DeleteMeditation(string meditaitonId)
        {
            var id = Guid.Parse(meditaitonId);

            using (var dbConn = new SapremaFinalContext())
            {
                var meditation = dbConn.SapMeditations.Where(a => a.MeditationId == id).SingleOrDefault();
                if (meditation != null)
                {
                    dbConn.SapMeditations.Remove(meditation);
                    dbConn.SaveChanges();
                }

                return true;
            }
        }

        public bool DeleteMeditationReview(string meditationReviewId)
        {
            var id = Guid.Parse(meditationReviewId);

            using (var dbConn = new SapremaFinalContext())
            {
                var meditationReview = dbConn.SapReviewMeditation.Where(a => a.ReviewMeditationId == id).SingleOrDefault();
                if (meditationReview != null)
                {
                    dbConn.SapReviewMeditation.Remove(meditationReview);
                    dbConn.SaveChanges();
                }

                return true;
            }
        }
    }
}
