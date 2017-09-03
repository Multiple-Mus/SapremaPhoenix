using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SapremaAPI.Entities;
using SapremaAPI.Models;

namespace SapremaAPI.DAL
{
    public class Update
    {
        /*
         * Come back to this need to create proper saving for audio and image
         * */
        public bool UpdateMeditation(SapMeditations meditation)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                SapMeditations sapMeditation = dbConn.SapMeditations.Where(a => a.MeditationId == meditation.MeditationId).FirstOrDefault();

                sapMeditation.MeditationName = meditation.MeditationName;
                sapMeditation.MeditationTheme = meditation.MeditationTheme;
                sapMeditation.MeditationCreator = meditation.MeditationCreator;
                sapMeditation.MeditationType = meditation.MeditationType;
                sapMeditation.MeditationDescription = meditation.MeditationDescription;

                dbConn.SaveChanges();
            };

            return true;
        }

        /*
         * Update teacher verification status
         * */
        public bool UpdateTeacherVerification(string id, string status)
        {
            if (status.Equals("true") || status.Equals("false"))
            {
                bool verification = Boolean.Parse(status);

                using (var dbConn = new SapremaFinalContext())
                {
                    SapTeachers sapTeachers = dbConn.SapTeachers.Where(a => a.TeachId == id).FirstOrDefault();

                    sapTeachers.Verified = verification;

                    dbConn.SaveChanges();
                };

                return true;
            }

            else
            {
                return false;
            }
        }

        /*
         * Update flagged item status
         * Come back to this for error reporting
         * */
        public bool ResolveFlaggedItem(string itemId, string status)
        {
            if (status.Equals("true") || status.Equals("false"))
            {
                var type = new Get().GetFlagType(itemId);
                var id = Guid.Parse(itemId);
                bool resolution = Boolean.Parse(status);

                using (var dbConn = new SapremaFinalContext())
                {
                    if (type.Equals("meditation"))
                    {
                        SapFlagMeditations sapFlagMeditations = dbConn.SapFlagMeditations.Where(a => a.FlagId == id).FirstOrDefault();

                        sapFlagMeditations.FlagResolved = resolution;

                        dbConn.SaveChanges();

                        return true;
                    }

                    else if (type.Equals("class"))
                    {
                        SapFlagClasses sapFlagClasses = dbConn.SapFlagClasses.Where(a => a.FlagId == id).FirstOrDefault();

                        sapFlagClasses.FlagResolved = resolution;

                        dbConn.SaveChanges();

                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }
            }

            else
            {
                return false;
            }
        }

        public bool UpdateGroup(SapGroups group)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                SapGroups sapGroups = dbConn.SapGroups.Where(a => a.GroupId == group.GroupId && a.GroupAdmin == group.GroupAdmin).FirstOrDefault();

                if (sapGroups == null)
                {
                    return false;
                }

                else
                {
                    sapGroups.GroupName = group.GroupName;
                    sapGroups.GroupStatus = group.GroupStatus;
                    sapGroups.GroupDescription = group.GroupDescription;
                    sapGroups.GroupLevel = group.GroupLevel;
                    dbConn.SaveChanges();
                    return true;
                }
            } 
        }

        public bool UpdateMeditationReview(SapReviewMeditation review)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                SapReviewMeditation sapReview = dbConn.SapReviewMeditation.Where(a => a.MeditationId == review.MeditationId && a.UserId == review.UserId).FirstOrDefault();

                if (sapReview == null)
                {
                    return false;
                }

                else
                {
                    sapReview.ReviewStars = review.ReviewStars;
                    sapReview.ReviewComment = review.ReviewComment;
                    dbConn.SaveChanges();
                    return true;
                }
            }
        }
    }
}
