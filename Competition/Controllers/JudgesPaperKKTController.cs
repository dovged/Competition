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
    [RoutePrefix("/api/competition/id/judgespapersKKT")]
    public class JudgesPaperKKTController : BaseAPIController
    {
        /** Grąžinamas sąrašas teisėjo lapų pagal varžybas;
          Surikiuotas pagal Trasas;*/
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblJudgesPapersKKT.AsEnumerable() != null)
            {
                List<RouteKKTModel> routes = CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList();
                List<JudgesPaperKKTModel> judgePapers = new List<JudgesPaperKKTModel>();

                foreach (RouteKKTModel r in routes)
                {
                    List<JudgesPaperKKTModel> papers = CompetitionDB.TblJudgesPapersKKT.ToArray().Where(x => x.RouteId == r.Id).Select(x => new JudgesPaperKKTModel(x)).ToList();

                    foreach (JudgesPaperKKTModel p in papers)
                    {
                        p.RouteName = CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == p.RouteId).Name;
                        p.TeamName = CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == p.TeamId).Name;
                        judgePapers.Add(p);
                    }
                }

                return ToJsonOK(judgePapers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas vienas teisėjo lapas;*/
        public HttpResponseMessage Get(int compId, int paperId)
        {
            if (CompetitionDB.TblJudgesPapersKKT.FirstOrDefault(x => x.Id == paperId) != null)
            {
                JudgesPaperKKTModel paper = CompetitionDB.TblJudgesPapersKKT.ToArray().Where(x => x.Id == paperId).Select(x => new JudgesPaperKKTModel(x)).FirstOrDefault();
                List<PenaltyQuantityModel> quantity = CompetitionDB.TblPenaltyQuantities.ToArray().Where(x => x.JudgesPaperId == paperId).Select(x => new PenaltyQuantityModel(x)).ToList();
                foreach (var q in quantity)
                {
                    q.PenaltyName = CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == q.PenaltyId).Name.ToString();
                }
                paper.Penalties = quantity;
                paper.TeamName = CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == paper.TeamId).Name.ToString();
                paper.RouteName = CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == paper.RouteId).Name.ToString();
                paper.PenaltySum = PointsFromPenalties(paper.Id, quantity);
                /** Kaip padaryti dėl skaičiavimo. Baudos verčiamos į laiką ar minusiuojamos nuo taškų*/
                /***paper.TimeWithPenalty(SecToTime((TimeToSec(paper.Time)) + (paper.PenaltySum * 30)));*/

                return ToJsonOK(paper);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
        
        /** Sukurti teisėjo lapą*/
        public HttpResponseMessage Post([FromBody]TblJudgesPaperKKT value)
        {
            /**PRIDĖTI TEISĖJO ID PRIE LAPO*/
            // TO DO
            /*ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();*/
          /*  string accountId = "3";
            int UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());
            if (CompetitionDB.TblCompJuds.FirstOrDefault(x => x.UserId == UserId) != null)
            {*/
            CompetitionDB.TblJudgesPapersKKT.Add(value);
            CompetitionDB.SaveChanges();

            return ToJsonCreated(value);
           /* }

            return Request.CreateResponse(HttpStatusCode.NotFound, "NO access!");*/
        }

        /** Redaguoti teisėjo lapą*/
        public HttpResponseMessage Put(int id, [FromBody]TblJudgesPaperKKT value)
        {
            /**AR REIKIA TIKRINTI KAS REDAGUOJA???*/

            if (CompetitionDB.TblJudgesPapersKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
        
        /**AR REIKIA IŠTRINTI???*/
       /* public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblJudgesPapers.AsEnumerable() != null)
            {
                CompetitionDB.TblJudgesPapers.Remove(CompetitionDB.TblJudgesPapers.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }*/

        /** Baudos taškų suma gauna iš visų baudų*/
        public int PointsFromPenalties(int id, List<PenaltyQuantityModel> pen)
        {
            int points = 0;
            List<PenaltyQuantityModel> penaltyQ = pen;
            if (penaltyQ.Count != 0)
            {
                foreach (var p in penaltyQ)
                {
                    if (p != null)
                    {
                        points += PointSumFromPenalties(p.Id);
                    }
                }
            }

            return points;
        }

        /** Baudų suma gaunama iš vienos baudų kategorijos*/
        public int PointSumFromPenalties(int id)
        {
            int penaltyId = Convert.ToInt32(CompetitionDB.TblPenaltyQuantities.Find(id).PenaltyId.ToString());

            return Convert.ToInt32(CompetitionDB.TblPenaltyQuantities.Find(id).Quantity.ToString()) *
                Convert.ToInt32(CompetitionDB.TblPenalties.Find(penaltyId).Points.ToString());
        }

        // convert time(string) to seconds
        public int TimeToSec(string time)
        {
            //time format (hh:mm:ss)
            return (Convert.ToInt32(time.Substring(0, 2)) * 3600) + (Convert.ToInt32(time.Substring(3, 2)) * 60) + (Convert.ToInt32(time.Substring(6, 2)));
        }

        // convert setods to time(string)
        public string SecToTime(int time)
        {
            int val = time / 3600;
            int min = (time - (val * 3600)) / 60;
            int s = time - (val * 3600) - (min * 60);
            string timeS = "";
            if (val >= 10)
            {
                timeS = val.ToString() + ":";
            }
            else
            {
                timeS = "0" + val.ToString() + ":";
            }
            if (min >= 10)
            {
                timeS += min.ToString() + ":";
            }
            else
            {
                timeS += "0" + min.ToString() + ":";
            }
            if (s >= 10)
            {
                timeS += s.ToString();
            }
            else
            {
                timeS += "0" + s.ToString();
            }
            return timeS;
        }
    }
}
