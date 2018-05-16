using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Competition.Controllers
{
    
    public class JudgesPaperClimController : BaseAPIController
    {
        /** Grąžinamas vienas teisėjo lapas;*/
        [Route("api/judgespapersClim/{routeId}/{userId}/{s}")]
        public HttpResponseMessage Get(int routeId, int userId, string s)
        {

            if (CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.ClimberId == userId && x.RouteId == routeId) != null)
            {
                JudgesPaperClimbModel paper = CompetitionDB.TblJudgesPapersClimb.ToArray().Where(x => x.ClimberId == userId && x.RouteId == routeId).Select(x => new JudgesPaperClimbModel(x)).FirstOrDefault();

                //paper.Climber = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == paper.ClimberId).Name + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == paper.ClimberId).LastName;
               // paper.RouteNumber = CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == paper.RouteId).Number;

                return ToJsonOK(paper);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Redaguoti teisėjo lapą
         n reikšmės -
            1 - flash trasą pralipo;
            2 - redpoint trasą pralipo;
            3 - bonusas;
            4 - pridėti bandymą topui;
            5 - atimpti bandymą topui;
            6 - pridėti bandymą bonusui;
            7 - atimpti bandymą bonusui;*/
        [Route("api/judgespaperClim/{routeId}/{userId}/{n}")]
        public HttpResponseMessage Put(int routeId, int userId, int n)
        {
            if (CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.ClimberId == userId && x.RouteId == routeId) != null)
            {
                TblJudgesPaperClim paper = CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.ClimberId == userId && x.RouteId == routeId);
                switch (n)
                {
                    case 1:
                        paper.TopAttempt = 1;
                        break;
                    case 2:
                        paper.TopAttempt = 2;
                        break;
                    case 3:
                        paper.BonusAttempt = 1;
                        break;
                    case 4:
                        paper.TopAttempt++;
                        break;
                    case 5:
                        paper.TopAttempt--;
                        break;
                    case 6:
                        paper.BonusAttempt++;
                        break;
                    case 7:
                        paper.BonusAttempt--;
                        break;
                }
                paper.Date = DateTime.Now;

                CompetitionDB.Entry(paper).State = EntityState.Modified;

                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

    }
}
