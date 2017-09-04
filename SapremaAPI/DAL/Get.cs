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
                    MeditationDescription = meditation.MeditationDescription.ToString(),
                    MeditationCreator = new Get().GetTeacherName(meditation.MeditationCreator),
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

                foreach (var record in meditationsList)
                {
                    //var meditationStars = dbConn.SapReviewMeditation.Where(a => a.MeditationId == record.MeditationId).Select(r => r.ReviewStars).DefaultIfEmpty().Average();
                    MeditationModel mModel = new MeditationModel()
                    {
                        MeditationName = record.MeditationName,
                        MeditationDescription = record.MeditationDescription.ToString(),
                        MeditationCreator = record.MeditationCreator.ToString(),
                        MeditationTheme = record.MeditationTheme,
                        MeditationType = record.MeditationType,
                        MeditationId = record.MeditationId.ToString(),
                        MeditationRating = 5
                    };

                    meditationsModel.Add(mModel);
                }

                return meditationsModel;
            }
        }

        public List<MeditationStoreModel> GetUserMeditations(string id)
        {
            List<MeditationStoreModel> meditationStoreModel = new List<MeditationStoreModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var meditationList = dbConn.SapMeditations.ToList();

                foreach (var record in meditationList)
                {
                    var meditationStars = dbConn.SapReviewMeditation.Where(a => a.MeditationId == record.MeditationId).Select(r => r.ReviewStars).DefaultIfEmpty().Average();
                    MeditationStoreModel mModel = new MeditationStoreModel()
                    {
                        MeditationName = record.MeditationName,
                        //MeditationImage = record.MeditationImage.ToString(),
                        MeditationCreator = record.MeditationCreator, //new Get().GetTeacherName(record.MeditationCreator),
                        MeditationTheme = record.MeditationTheme,
                        MeditationType = record.MeditationType,
                        MeditationId = record.MeditationId.ToString(),
                        MeditationRating = Math.Round(meditationStars, 0, MidpointRounding.AwayFromZero),
                        MeditationPurchased = new Get().GetMeditationPurchased(id, record.MeditationId)
                    };

                    meditationStoreModel.Add(mModel);
                }

                return meditationStoreModel;
            }
        }

        public List<GroupStore> GetStoreGroups(string id)
        {
            List<GroupStore> groupStore = new List<GroupStore>();

            using (var dbConn = new SapremaFinalContext())
            {
                var storeList = dbConn.SapGroups.Where(a => a.GroupStatus != true && a.GroupAdmin != id).ToList();

                foreach(var record in storeList)
                {
                    bool status;
                    var groupJoined = dbConn.SapUserGroups.Where(a => a.GroupId == record.GroupId && a.UserId == id).FirstOrDefault();
                    if (groupJoined == null)
                    {
                        status = false;
                    }
                    else
                    {
                        status = true;
                    }

                    GroupStore gModel = new GroupStore()
                    {
                        GroupId = record.GroupId,
                        GroupLevel = record.GroupLevel,
                        GroupAdmin = new Get().GetUserName(record.GroupAdmin),
                        GroupJoined = status,
                        GroupName = record.GroupName
                    };

                    groupStore.Add(gModel);
                }

                return groupStore;
            }
        }

        public List<UserMeditationsModel> GetUserListMeditations(string id)
        {
            List<UserMeditationsModel> userMeditationsModel = new List<UserMeditationsModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var purchasedList = dbConn.SapUserMeditations.Where(a => a.UserId == id).ToList();

                foreach (var record in purchasedList)
                {
                    var meditation = dbConn.SapMeditations.Where(a => a.MeditationId == record.MeditationId).SingleOrDefault();
                    UserMeditationsModel uModel = new UserMeditationsModel()
                    {
                        MeditationId = record.MeditationId,
                        MeditationName = meditation.MeditationName,
                        MeditationCreator = meditation.MeditationCreator
                    };

                    userMeditationsModel.Add(uModel);
                }

                return userMeditationsModel;
            }
        }

        /*
         * Get all a users reviews
         * */
        public List<ReviewModel> GetAllUserReviews(string userId)
        {
            List<ReviewModel> reviewModel = new List<ReviewModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var meditationReviewList = dbConn.SapReviewMeditation.Where(a => a.UserId == userId).ToList();

                foreach (var record in meditationReviewList)
                {
                    ReviewModel mModel = new ReviewModel()
                    {
                        ReviewId = record.ReviewMeditationId,
                        ItemId = record.MeditationId,
                        ReviewComment = record.ReviewComment,
                        ReviewStars = record.ReviewStars,
                        Username = new Get().GetUserName(record.UserId),
                        ReviewType = "meditation"
                    };

                    reviewModel.Add(mModel);
                }

                var classReviewList = dbConn.SapReviewClass.Where(b => b.UserId == userId).ToList();

                foreach (var record in classReviewList)
                {
                    ReviewModel cModel = new ReviewModel()
                    {
                        ReviewId = record.ReviewClassId,
                        ItemId = record.ClassId,
                        ReviewComment = record.ReviewComment,
                        ReviewStars = record.ReviewStars,
                        Username = new Get().GetUserName(record.UserId),
                        ReviewType = "class"
                    };

                    reviewModel.Add(cModel);
                }

                return reviewModel;
            }
        }

        /*
        * Get all reviews of a single meditation
        * */
        public List<ReviewModel> GetAllMeditationReviews(string meditationId)
        {
            List<ReviewModel> meditationsReviewModel = new List<ReviewModel>();
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                var reviewList = dbConn.SapReviewMeditation.Where(a => a.MeditationId == id).ToList();

                foreach (var record in reviewList)
                {
                    ReviewModel mModel = new ReviewModel()
                    {
                        ReviewId = record.ReviewMeditationId,
                        ItemId = record.MeditationId,
                        ReviewComment = record.ReviewComment,
                        ReviewStars = record.ReviewStars,
                        Username = new Get().GetUserName(record.UserId)
                    };

                    meditationsReviewModel.Add(mModel);
                }

                return meditationsReviewModel;
            }
        }

        /*
         * Get review of single meditation from user
         * */
        public ReviewModel GetSingleUserMeditationReview(string userId, string meditationId)
        {
            var id = Guid.Parse(meditationId);

            using (var dbConn = new SapremaFinalContext())
            {
                var userMeditationReview = dbConn.SapReviewMeditation.Where(a => a.UserId == userId && a.MeditationId == id).SingleOrDefault();
                var reviewReturn = new ReviewModel()
                {
                    ReviewId = userMeditationReview.ReviewMeditationId,
                    Username = new Get().GetUserName(userMeditationReview.UserId),
                    ItemId = userMeditationReview.MeditationId,
                    ReviewComment = userMeditationReview.ReviewComment,
                    ReviewStars = userMeditationReview.ReviewStars
                };

                return reviewReturn;
            }
        }

        /*
         * Gets single review from a user
         * this method was broken up into individual sections to allow for reuse of methods
         * */
        public ReviewModel GetSingleUserReview(string userId, string itemId)
        {
            var type = new Get().GetReviewType(userId, itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                if (type == null)
                {
                    return null;
                }

                if (type.Equals("class"))
                {
                    var returnReview = new Get().GetSingleUserClassReview(userId, itemId);
                    return returnReview;
                }

                else if (type.Equals("meditation"))
                {
                    var returnReview = new Get().GetSingleUserMeditationReview(userId, itemId);
                    return returnReview;
                }

                else
                {
                    return null;
                }
            }
        }

        /*
         * Get review of single class from user
         * */
        public ReviewModel GetSingleUserClassReview(string userId, string classId)
        {
            var id = Guid.Parse(classId);

            using (var dbConn = new SapremaFinalContext())
            {
                var userClassReview = dbConn.SapReviewClass.Where(a => a.UserId == userId && a.ClassId == id).SingleOrDefault();
                var returnReview = new ReviewModel()
                {
                    ReviewId = userClassReview.ReviewClassId,
                    Username = new Get().GetUserName(userClassReview.UserId),
                    ItemId = userClassReview.ClassId,
                    ReviewComment = userClassReview.ReviewComment,
                    ReviewStars = userClassReview.ReviewStars
                };

                return returnReview;
            }
        }

        /*
         * Get all teachers for verification
         * */
        public List<TeacherAdminModel> GetAllTeachersAdmin()
        {
            List<TeacherAdminModel> teacherAdminModel = new List<TeacherAdminModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var teacherList = dbConn.SapTeachers.ToList();

                foreach (var record in teacherList)
                {
                    TeacherAdminModel tModel = new TeacherAdminModel()
                    {
                        TeacherId = record.TeachId,
                        TeacherName = record.FullName,
                        VerifiedStatus = record.Verified
                    };

                    teacherAdminModel.Add(tModel);
                }

                return teacherAdminModel;
            }
        }


        /*
         * Get a list of all poses and see if a user has chosen ot omit them
         * */
        public List<UserPosesModel> GetAllUserPoses(string id)
        {
            List<UserPosesModel> userPoseModel = new List<UserPosesModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var poseList = dbConn.SapPoses.ToList();

                foreach (var record in poseList)
                {
                    UserPosesModel uModel = new UserPosesModel()
                    {
                        PoseId = record.PoseId,
                        PoseName = record.PoseName,
                        PoseLevel = record.PoseLevel,
                        PoseTheme = record.PoseTheme,
                        PoseOmit = new Get().CheckOmit(id, record.PoseId)
                    };

                    userPoseModel.Add(uModel);
                }

                return userPoseModel;
            }
        }



        /*
         * Get single flagged item
         * Maybe have a look at the last line "return null" - try and improve on error reporting
         * */
        public FlaggedModel GetSingleFlaggedItem(string itemId)
        {
            var type = new Get().GetFlagType(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                if (type.Equals("class"))
                {
                    var returnFlag = new Get().GetSingleClassFlag(itemId);
                    return returnFlag;
                }

                else if (type.Equals("meditation"))
                {
                    var returnFlag = new Get().GetSingleMeditationFlag(itemId);
                    return returnFlag;
                }

                else
                {
                    return null;
                }
            }
        }

        /*
         * Get a single flagged class
         * */
        public FlaggedModel GetSingleClassFlag(string itemId)
        {
            var id = Guid.Parse(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                var classFlag = dbConn.SapFlagClasses.Where(a => a.FlagId == id).SingleOrDefault();
                var flagReturn = new FlaggedModel()
                {
                    FlagId = classFlag.FlagId,
                    ItemId = classFlag.ClassId,
                    ReviewId = classFlag.ClassReviewId,
                    FlaggedBy = new Get().GetUserName(classFlag.UserId),
                    ReasonFlagged = classFlag.ReasonFlagged,
                    FlagComment = classFlag.FlagComment,
                    FlagType = "class",
                    FlagResolved = classFlag.FlagResolved
                };

                return flagReturn;
            }
        }

        public List<ClassInfoModel> GetClassData(string classId)
        {
            var id = Guid.Parse(classId);
            List<ClassInfoModel> classInfoModelList = new List<ClassInfoModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                IEnumerable<SapClass> sapClassList = dbConn.SapClass.Where(a => a.ClassGroupId == id);
                if (sapClassList == null)
                {
                    return null;
                }

                foreach (SapClass sapClass in sapClassList)
                {
                    ClassInfoModel classInfoModel = new ClassInfoModel()
                    {
                        ClassCreatedBy = sapClass.ClassCreatedBy,
                        ClassName = sapClass.ClassName,
                        ClassLevel = Convert.ToInt32(sapClass.ClassLevel),
                        ClassTheme = sapClass.ClassTheme,
                        ClassDescription = sapClass.ClassDescription,
                        ClassId = sapClass.ClassId,
                        GroupId = sapClass.ClassGroupId.Value
                    };
                    classInfoModelList.Add(classInfoModel);
                }
                return classInfoModelList;
            }
        }

        /*
         * Get a single flagged meditation
         * */
        public FlaggedModel GetSingleMeditationFlag(string itemId)
        {
            var id = Guid.Parse(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                var meditationFlag = dbConn.SapFlagMeditations.Where(a => a.FlagId == id).SingleOrDefault();
                var flagReturn = new FlaggedModel()
                {
                    FlagId = meditationFlag.FlagId,
                    ItemId = meditationFlag.MeditationId,
                    ReviewId = meditationFlag.MeditationReviewId,
                    FlaggedBy = new Get().GetUserName(meditationFlag.UserId),
                    ReasonFlagged = meditationFlag.ReasonFlagged,
                    FlagComment = meditationFlag.FlagComment,
                    FlagType = "meditation",
                    FlagResolved = meditationFlag.FlagResolved
                };

                return flagReturn;
            }
        }

        /*
         * Get all flagged items
         * */
        public List<FlaggedListModel> GetAllFlaggedItems()
        {
            List<FlaggedListModel> flaggedListModel = new List<FlaggedListModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                var meditationFlaggedList = dbConn.SapFlagMeditations.ToList();

                foreach (var record in meditationFlaggedList)
                {
                    FlaggedListModel mModel = new FlaggedListModel()
                    {
                        FlagId = record.FlagId,
                        ItemId = record.MeditationId,
                        ReviewId = record.MeditationReviewId,
                        FlagType = "meditation",
                        FlagResolved = record.FlagResolved
                    };

                    flaggedListModel.Add(mModel);
                }

                var classFlaggedList = dbConn.SapFlagClasses.ToList();

                foreach (var record in classFlaggedList)
                {
                    FlaggedListModel cModel = new FlaggedListModel()
                    {
                        FlagId = record.FlagId,
                        ItemId = record.ClassId,
                        ReviewId = record.ClassReviewId,
                        FlagType = "class",
                        FlagResolved = record.FlagResolved
                    };

                    flaggedListModel.Add(cModel);
                }

                return flaggedListModel;
            }
        }

        /*
         * Get single group
         * */
        public GroupModel GetSingleGroup(string itemId)
        {
            var id = Guid.Parse(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                var record = dbConn.SapGroups.Where(a => a.GroupId == id).SingleOrDefault();
                var groupReturn = new GroupModel()
                {
                    GroupDescription = record.GroupDescription,
                    GroupId = record.GroupId,
                    GroupLevel = record.GroupLevel,
                    GroupName = record.GroupName,
                    GroupStatus = record.GroupStatus.ToString()
                };

                return groupReturn;
            }
        }

        /*
         * Get all groups a teacher has created
         * */
        public List<UserGroupsModel> GetUserGroups(string id)
        {
            List<UserGroupsModel> groupModel = new List<UserGroupsModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                List<SapGroups> groups = dbConn.SapGroups.Where(a => a.GroupAdmin == id).ToList();
                //var groupList = dbConn.SapGroups.Where(a => a.GroupAdmin == id).ToList();

                foreach (var record in groups)
                {
                    UserGroupsModel gModel = new UserGroupsModel()
                    {
                        GroupId = record.GroupId,
                        GroupName = record.GroupName
                    };

                    groupModel.Add(gModel);
                }

                return groupModel;
            }
        }

        /*
         * Get all meditation themes
         * */
        public List<string> GetMeditationThemes()
        {
            using (var dbconn = new SapremaFinalContext())
            {
                var themeList = dbconn.SapMeditations.Select(a => a.MeditationTheme).Distinct().ToList();
                return themeList;
            }
        }

        /*
         * Get all meditation types
         * */
        public List<string> GetMeditationTypes()
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var typeList = dbConn.SapMeditations.Select(a => a.MeditationType).Distinct().ToList();
                return typeList;
            }
        }

        public bool GetOwnership(string userId, string itemId)
        {
            var id = Guid.Parse(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                var ownership = dbConn.SapUserMeditations.Where(a => a.UserId == userId && a.MeditationId == id).FirstOrDefault();
                if (ownership == null)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }

        public List<ClassModel> GetUserclasses(string userId)
        {
            List<ClassModel> classes = new List<ClassModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                List<SapClass> list = dbConn.SapClass.Where(a => a.ClassCreatedBy == userId).ToList();

                foreach (var record in list)
                {
                    ClassModel cModel = new ClassModel()
                    {
                        ClassName = record.ClassName,
                        ClassTheme = record.ClassTheme,
                        ClassLevel = record.ClassLevel.Value,
                        ClassId = record.ClassId
                    };

                    classes.Add(cModel);
                }

                return classes;
            }
        }

        public List<ClassModel> GetSubbedclasses(string userId)
        {
            List<ClassModel> classes = new List<ClassModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                List<SapUserGroups> list = dbConn.SapUserGroups.Where(a => a.UserId == userId).ToList();

                foreach (var record in list)
                {
                    List<SapClass> item = dbConn.SapClass.Where(a => a.ClassGroupId == record.GroupId).ToList();

                    foreach(var rec in item)
                    {
                        ClassModel cModel = new ClassModel()
                        {
                            ClassName = rec.ClassName,
                            ClassTheme = rec.ClassTheme,
                            ClassLevel = rec.ClassLevel.Value,
                            ClassId = rec.ClassId
                        };

                        classes.Add(cModel);
                    }
                }
                return classes;
            }
        }

        public List<PoseModel> GetClassPoses(string itemId)
        {
            var id = Guid.Parse(itemId);

            List<PoseModel> poses = new List<PoseModel>();

            using (var dbConn = new SapremaFinalContext())
            {
                List<SapClassPoses> list = dbConn.SapClassPoses.Where(a => a.ClassId == id).OrderBy(a => a.SequenceNumber).ToList();

                foreach (var record in list)
                {
                    PoseModel pModel = new PoseModel()
                    {
                        PoseId = record.PoseId,
                        PoseLength = record.PoseTime,
                        PoseName = new Get().GetPoseName(record.PoseId),
                        PoseSequence = record.SequenceNumber
                    };

                    poses.Add(pModel);
                }

                return poses;
            }
        }

        public bool CheckClass(string userId, string itemId)
        {
            var id = Guid.Parse(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                var classCheck = dbConn.SapClass.Where(a => a.ClassCreatedBy == userId && a.ClassId == id).FirstOrDefault();

                SapClassComplete cComplete = new SapClassComplete()
                {
                    UserId = userId,
                    ClassId = id,
                    ClassCompletedOn = DateTime.Now,
                    ClassCompleteId = Guid.NewGuid()
                };

                dbConn.SapClassComplete.Add(cComplete);
                dbConn.SaveChanges();

                if (classCheck != null)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }


        /*
         * Helper get methods below
         * */

        public string GetPoseName (string poseId)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var name = dbConn.SapPoses.Where(a => a.PoseId == poseId).FirstOrDefault();
                var poseName = name.PoseName;
                return poseName;
            }
        }

        /*
         * Get the type of review, used only on this sheet for getting user reviews
         * */
        public string GetReviewType(string userId, string itemId)
        {
            var id = Guid.Parse(itemId);

            using (var dbConn = new SapremaFinalContext())
            {
                if (dbConn.SapReviewClass.Any(a => a.ClassId == id && a.UserId == userId))
                {
                    return "class";
                }

                else if (dbConn.SapReviewMeditation.Any(a => a.MeditationId == id && a.UserId == userId))
                {
                    return "meditation";
                }

                else
                {
                    return null;
                }
            }
        }

        /*
         * Check if a meditation is purchased
         * */
        public bool GetMeditationPurchased(string userId, Guid meditationId)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var purchased = dbConn.SapUserMeditations.Where(a => a.UserId == userId && a.MeditationId == meditationId).SingleOrDefault();

                if (purchased != null)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        /*
         * Get type of flag, used only here for getting user flags
         * */
        public string GetFlagType(string flagId)
        {
            var id = Guid.Parse(flagId);

            using (var dbConn = new SapremaFinalContext())
            {
                if (dbConn.SapFlagClasses.Any(a => a.FlagId == id))
                {
                    return "class";
                }

                else if (dbConn.SapFlagMeditations.Any(a => a.FlagId == id))
                {
                    return "meditation";
                }

                else
                {
                    return null;
                }
            }
        }

        /* 
         * Get a teachers name
         * */
        public string GetTeacherName(string id)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var name = dbConn.SapTeachers.Where(a => a.TeachId == id).Select(b => b.FullName).FirstOrDefault();
                return name;
            }
        }

        /*
         * Get a username
         * */
        public string GetUserName(string id)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var username = dbConn.AspNetUsers.Where(a => a.Id == id).Select(b => b.UserName).FirstOrDefault();
                return username;
            }
        }

        /*
         * Check omit status of pose
         * */
        public bool CheckOmit(string userId, string poseId)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var omit = dbConn.SapUserPoses.Where(a => a.UserId == userId && a.PoseId == poseId).FirstOrDefault();

                if (omit == null)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }
    }
}
