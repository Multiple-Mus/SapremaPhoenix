using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SapremaAPI.Entities;
using SapremaAPI.Models;

namespace SapremaAPI.DAL
{
    public class Get
    {
        /*
         * Test get to check API database connection
         * */
        public AspNetUsers GetUser(string userId)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var user = dbConn.AspNetUsers.Where(a => a.Id == userId).SingleOrDefault();
                return user;
            }
        }

        /*
         *  Come back to this: Round review to nearest .5 to work with star rating in CSS
         *  Check for null within reviews
         * */
        public MeditationModel GetSingleMeditation(string meditationId)
        {
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                var meditation = dbConn.SapMeditations.Where(a => a.MeditationId == id).SingleOrDefault();
                var meditationStars = dbConn.SapReviewMeditation.Where(a => a.MeditationId == id).Select(r => r.ReviewStars).DefaultIfEmpty().Average();
                var meditationReturn = new MeditationModel()
                {
                    MeditationName = meditation.MeditationName,
                    MeditationImage = meditation.MeditationImage.ToString(),
                    MeditationCreator = meditation.MeditationCreator,
                    MeditationTheme = meditation.MeditationTheme,
                    MeditationType = meditation.MeditationType,
                    MeditationId = meditation.MeditationId.ToString(),
                    MeditationRating = meditationStars
                };

                return meditationReturn;
            }
        }

        /*
         *  Come back to this: Round review to nearest .5 to work with star rating in CSS
         *  Check for null within reviews
         * */
        public List<MeditationModel> GetAllMeditations()
        {
            List<MeditationModel> meditationsModel = new List<MeditationModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var meditationsList = dbConn.SapMeditations.ToList();

                foreach(var record in meditationsList)
                {
                    var meditationStars = dbConn.SapReviewMeditation.Where(a => a.MeditationId == record.MeditationId).Select(r => r.ReviewStars).DefaultIfEmpty().Average();

                    MeditationModel mModel = new MeditationModel()
                    {
                        MeditationName = record.MeditationName,
                        MeditationImage = record.MeditationImage.ToString(),
                        MeditationCreator = record.MeditationCreator,
                        MeditationTheme = record.MeditationTheme,
                        MeditationType = record.MeditationType,
                        MeditationId = record.MeditationId.ToString(),
                        MeditationRating = meditationStars
                    };

                    meditationsModel.Add(mModel);
                }

                return meditationsModel;
            }
        }

        /*
         * Get all a users meditation reviews
         * */
        public List<MeditationReviewModel> GetAllUserMeditationReviews(string userId)
        {
            List<MeditationReviewModel> mediatationsReviewModel = new List<MeditationReviewModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var reviewList = dbConn.SapReviewMeditation.Where(a => a.UserId == userId).ToList();

                foreach (var record in reviewList)
                {
                    MeditationReviewModel mModel = new MeditationReviewModel()
                    {
                        MeditationReviewId = record.ReviewMeditationId,
                        MeditationId = record.MeditationId,
                        ReviewComment = record.ReviewComment,
                        ReviewStars = record.ReviewStars,
                        userId = record.UserId
                    };

                    mediatationsReviewModel.Add(mModel);
                }

                return mediatationsReviewModel;
            }
        }

        /*
        * Get all reviews of a single meditation
        * */
        public List<MeditationReviewModel> GetAllMeditationReviews(string meditationId)
        {
            List<MeditationReviewModel> meditationsReviewModel = new List<MeditationReviewModel>();
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                var reviewList = dbConn.SapReviewMeditation.Where(a => a.MeditationId == id).ToList();

                foreach (var record in reviewList)
                {
                    MeditationReviewModel mModel = new MeditationReviewModel()
                    {
                        MeditationReviewId = record.ReviewMeditationId,
                        MeditationId = record.MeditationId,
                        ReviewComment = record.ReviewComment,
                        ReviewStars = record.ReviewStars,
                        userId = record.UserId
                    };

                    meditationsReviewModel.Add(mModel);
                }

                return meditationsReviewModel;
            }
        }

        /*
         * Get review of single meditation from user
         * */
        public MeditationReviewModel GetSingleMeditationReview(string userId, string meditationId)
        {
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                var userMeditationReview = dbConn.SapReviewMeditation.Where(a => a.UserId == userId && a.MeditationId == id).SingleOrDefault();

                var reviewReturn = new MeditationReviewModel()
                {
                    MeditationReviewId = userMeditationReview.ReviewMeditationId,
                    userId = userMeditationReview.UserId,
                    MeditationId = userMeditationReview.MeditationId,
                    ReviewComment = userMeditationReview.ReviewComment,
                    ReviewStars = userMeditationReview.ReviewStars
                };

                return reviewReturn;
            }
        }
    }
}
