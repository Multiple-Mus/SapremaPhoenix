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
                sapMeditation.MeditationImage = meditation.MeditationImage;

                dbConn.SaveChanges();
            };

            return true;
        }
    }
}
