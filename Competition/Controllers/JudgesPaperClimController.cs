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
    [RoutePrefix("/api/competition/id/judgespapersClim")]
    public class JudgesPaperClimController : BaseAPIController
    {
        /** Grąžinamas sąrašas teisėjo lapų pagal varžybas;
          Sugrupuotas pagal trasas;*/
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompetitions.ToArray().Where(x => x.Id == compId) != null)
            {
                List<RouteClimbModel> routes = CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteClimbModel(x)).ToList();
                List<JudgesPaperClimbModel> judgePapers = new List<JudgesPaperClimbModel>();

                foreach (RouteClimbModel r in routes)
                {
                    JudgesPaperClimbModel paper = new JudgesPaperClimbModel();
                    paper.RouteNumber = r.Number;
                    paper.RouteId = r.Id;
                    int TypeId = CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.RouteId == r.Id).TypeId;
                    paper.TypeName = CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == TypeId).Name;

                    judgePapers.Add(paper);
                }
                if (judgePapers.Count != 0)
                {
                    return ToJsonOK(judgePapers);
                }
                return ToJsonNotFound("Tuščias sąrašas.");
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas teisėjo lapų pagal varžybas ir pagal trasą;*/
        public HttpResponseMessage Get(int compId, int routeId)
        {
            if (CompetitionDB.TblCompetitions.ToArray().Where(x => x.Id == compId) != null)
            {
                List<JudgesPaperClimbModel> judgePapers = CompetitionDB.TblJudgesPapersClimb.ToArray().Where(x => x.RouteId == routeId).Select(x => new JudgesPaperClimbModel(x)).ToList();

                foreach (JudgesPaperClimbModel paper in judgePapers)
                {
                    paper.Climber = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == paper.ClimberId).Name + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == paper.ClimberId).LastName;
                }

                if (judgePapers.Count != 0)
                {
                    return ToJsonOK(judgePapers);
                }
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }


        /** Grąžinamas vienas teisėjo lapas;*/
        public HttpResponseMessage Get(int compId, int routeId, int paperId)
        {
            if (CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.Id == paperId) != null)
            {
                JudgesPaperClimbModel paper = CompetitionDB.TblJudgesPapersClimb.ToArray().Where(x => x.Id == paperId).Select(x => new JudgesPaperClimbModel(x)).FirstOrDefault();

                paper.Climber = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == paper.ClimberId).Name + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == paper.ClimberId).LastName;
                paper.RouteNumber = CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == paper.RouteId).Number;
                paper.TypeName = CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == paper.PaperTypeId).Name;

                return ToJsonOK(paper);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
        
        /** Sukurti teisėjo lapą*/
        public HttpResponseMessage Post([FromBody]TblJudgesPaperClim value)
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
            CompetitionDB.TblJudgesPapersClimb.Add(value);
            CompetitionDB.SaveChanges();

            return ToJsonCreated(value);
           /* }

            return Request.CreateResponse(HttpStatusCode.NotFound, "NO access!");*/
        }

        /** Redaguoti teisėjo lapą*/
        public HttpResponseMessage Put(int id, [FromBody]TblJudgesPaperClim value)
        {
            /**AR REIKIA TIKRINTI KAS REDAGUOJA???*/

            if (CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
        
        /**Teisėjo lapo ištrinimas*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblJudgesPapersClimb.Remove(CompetitionDB.TblJudgesPapersClimb.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();
                return ToJsonOK("Objektas ištrintas.");
            }

            return ToJsonNotFound("Objektas nerastas.");

        }

    }
}
