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
    
    public class JudgesPaperKKTController : BaseAPIController
    {
        /** Grąžinamas vienas teisėjo lapas;*/
        [Route("api/judgespapersKKT/{routeId}/{teamId}")]
        public HttpResponseMessage Get(int routeId, int teamId)
        {
            if (CompetitionDB.TblJudgesPapersKKT.FirstOrDefault(x => x.TeamId == teamId && x.RouteId == routeId) != null)
            {
                JudgesPaperKKTModel paper = CompetitionDB.TblJudgesPapersKKT.ToArray().Where(x => x.TeamId == teamId && x.RouteId == routeId).Select(x => new JudgesPaperKKTModel(x)).FirstOrDefault();
                List<PenaltyQuantityModel> quantity = CompetitionDB.TblPenaltyQuantities.ToArray().Where(x => x.JudgesPaperId == paper.Id).Select(x => new PenaltyQuantityModel(x)).ToList();
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


        /** Redaguoti teisėjo lapą*/
        [Route("api/judgespapersKKT/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]TblJudgesPaperKKT value)
        {
            value.Date = DateTime.Now;

            CompetitionDB.Entry(value).State = EntityState.Modified;
            return ToJsonOK(CompetitionDB.SaveChanges());
        }
        
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
