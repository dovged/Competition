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
    public class JudgesPaperController : BaseAPIController
    {
        /***/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblJudgesPapers.AsEnumerable() != null)
            {
                return ToJson(CompetitionDB.TblJudgesPapers.AsEnumerable());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");
        }

 
        [Authorize]
        /***/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblJudgesPapers.FirstOrDefault(x => x.Id == id) != null)
            {
                JudgesPaperKKTModel paper = CompetitionDB.TblJudgesPapers.ToArray().Where(x => x.Id == id).Select(x => new JudgesPaperKKTModel(x)).FirstOrDefault();
                List<PenaltyQuantityModel> quantity = CompetitionDB.TblPenaltyQuantities.ToArray().Where(x => x.JudgesPaperId == id).Select(x => new PenaltyQuantityModel(x)).ToList();
                foreach (var q in quantity)
                {
                    q.setPenaltyName(CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == q.PenaltyId).Name.ToString());
                }
                paper.SetPenalties(quantity);
                paper.SetTeamName(CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == paper.TeamId).Name.ToString());
                paper.SetRouteName(CompetitionDB.TblRoutes.FirstOrDefault(x => x.Id == paper.RouteId).Name.ToString());
                paper.SetPenalySum(PointsFromPenalties(paper.Id, quantity));
                paper.SetTimeWithPenalty(SecToTime((TimeToSec(paper.Time)) + (paper.PenaltySum * 30)));

                return ToJson(paper);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
        }
        [Authorize]
        /***/
        public HttpResponseMessage Post([FromBody]TblJudgesPaperKKT value)
        {
            /*ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();*/
          /*  string accountId = "3";
            int UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());
            if (CompetitionDB.TblCompJuds.FirstOrDefault(x => x.UserId == UserId) != null)
            {*/
                CompetitionDB.TblJudgesPapers.Add(value);
                return ToJson(CompetitionDB.SaveChanges());
           /* }

            return Request.CreateResponse(HttpStatusCode.NotFound, "NO access!");*/
        }

 
        /***/
        public HttpResponseMessage Put(int id, [FromBody]TblJudgesPaperKKT value)
        {

            if (CompetitionDB.TblJudgesPapers.AsEnumerable() != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }
        
        /***/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblJudgesPapers.AsEnumerable() != null)
            {
                CompetitionDB.TblJudgesPapers.Remove(CompetitionDB.TblJudgesPapers.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }

        /***/
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
                        points += pointSumFromPenalties(p.Id);
                    }

                }
            }

            return points;
        }

        /***/
        public int pointSumFromPenalties(int id)
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
