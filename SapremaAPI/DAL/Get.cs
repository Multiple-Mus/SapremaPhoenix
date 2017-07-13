using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SapremaAPI.Entities;

namespace SapremaAPI.DAL
{
    public class Get
    {
        public AspNetUsers GetUser(string userId)
        {
            using (var dbConn = new SapremaFinalContext())
            {
                var user = dbConn.AspNetUsers.Where(a => a.Id == userId).SingleOrDefault();
                //var userName = user.UserName;
                return user;
            }
        }
    }
}
