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

        public bool CreateGroup(SapGroups group)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                group.GroupId = Guid.NewGuid();
                dbConn.SapGroups.Add(group);
                dbConn.SaveChanges();

                return true;
            }
        }

        public bool OmitPose(string itemid, string userid)
        {
            SapUserPoses userPose = new SapUserPoses()
            {
                UserId = userid,
                PoseId = itemid,
                UserPoseId = Guid.NewGuid()
            };

            using (var dbConn = new SapremaFinalContext())
            {
                dbConn.SapUserPoses.Add(userPose);
                dbConn.SaveChanges();

                return true;
            }
        }

        public bool CreateFlaggedItem(FlaggedModel flaggedModel)
        {
            using (var dbConn = new SapremaFinalContext())
            {

                if (flaggedModel.FlagType.Equals("class"))
                {
                    SapFlagClasses classFlag = new SapFlagClasses()
                    {
                        FlagId = Guid.NewGuid(),
                        ClassId = flaggedModel.ItemId,
                        ClassReviewId = flaggedModel.ReviewId,
                        UserId = flaggedModel.FlaggedBy,
                        ReasonFlagged = flaggedModel.ReasonFlagged,
                        FlagComment = flaggedModel.FlagComment,
                        FlagResolved = false
                    };

                    dbConn.SapFlagClasses.Add(classFlag);
                    dbConn.SaveChanges();

                    return true;
                }

                else if (flaggedModel.FlagType.Equals("meditation"))
                {
                    SapFlagMeditations meditationFlag = new SapFlagMeditations()
                    {
                        FlagId = Guid.NewGuid(),
                        MeditationId = flaggedModel.ItemId,
                        MeditationReviewId = flaggedModel.ReviewId,
                        UserId = flaggedModel.FlaggedBy,
                        ReasonFlagged = flaggedModel.ReasonFlagged,
                        FlagComment = flaggedModel.FlagComment,
                        FlagResolved = false
                    };

                    dbConn.SapFlagMeditations.Add(meditationFlag);
                    dbConn.SaveChanges();

                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public bool CreateBuyMeditation(string userId, string meditationId)
        {
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                SapUserMeditations sapUserMeditations = new SapUserMeditations()
                {
                    MeditationId = id,
                    UserId = userId,
                    UserMeditationsId = Guid.NewGuid()
                };

                dbConn.SapUserMeditations.Add(sapUserMeditations);
                dbConn.SaveChanges();

                return true;
            }
        }
    }
}
