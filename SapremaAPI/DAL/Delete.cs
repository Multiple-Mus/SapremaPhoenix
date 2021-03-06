﻿using System;
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
                    return true;
                }

                else
                {
                    return false;
                }
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
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public bool DeleteGroup(string groupId, string userid)
        {
            var id = Guid.Parse(groupId);

            using (var dbConn = new SapremaFinalContext())
            {
                var group = dbConn.SapGroups.Where(a => a.GroupId == id && a.GroupAdmin == userid).SingleOrDefault();
                if (group != null)
                {
                    dbConn.SapGroups.Remove(group);
                    dbConn.SaveChanges();
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public bool DeleteUserMeditation(string userId, string meditationId)
        {
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                var meditation = dbConn.SapUserMeditations.Where(a => a.UserId == userId && a.MeditationId == id).FirstOrDefault();
                if (meditation != null)
                {
                    dbConn.SapUserMeditations.Remove(meditation);
                    dbConn.SaveChanges();
                    return true;
                }

                else
                {
                    return false;
                }
                
            }
        }

        public bool IncludePose(string poseid, string userid)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var pose = dbConn.SapUserPoses.Where(a => a.UserId == userid && a.PoseId == poseid).FirstOrDefault();
                if (pose != null)
                {
                    dbConn.SapUserPoses.Remove(pose);
                    dbConn.SaveChanges();
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        public bool DeleteGroupClass(string groupId)
        {
            Guid id = Guid.Parse(groupId);
            using (var dbConn = new SapremaFinalContext())
            {
                SapClass sapClass = dbConn.SapClass.Where(a => a.ClassGroupId == id).FirstOrDefault();
                if(sapClass != null)
                {
                    //dbConn.SapClass.Remove(sapClass);
                    //dbConn.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
