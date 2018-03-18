using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    [RoutePrefix("/api/competition/id/judgesClimb")]
    public class CompJudgeClimbController : BaseAPIController
    {
        /** Grąžina sąrašą teisėjų pagal varžybų id*/
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompJudgesClim.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeClimModel(x)).ToList().Count != 0)
            {
                List<CompJudgeClimModel> judges = CompetitionDB.TblCompJudgesClim.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeClimModel(x)).ToList();
                foreach(CompJudgeClimModel j in judges)
                {
                    j.JudgeName = "" + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).Name.ToString() + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).LastName.ToString();
                    j.Routes = CompetitionDB.TblJudgeRoutes.ToArray().Where(x => x.JudgeId == j.Id).Select(x => new JudgeRouteModel(x)).ToList();
                    foreach(JudgeRouteModel r in j.Routes)
                    {
                        r.RouteNumber = CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == r.RouteId).Number;
                    }
                }

                return ToJsonOK(judges);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžina vieno teisėjo informaciją*/
        public HttpResponseMessage Get(int JudgeId, int CompId)
        {
            if (CompetitionDB.TblCompJudgesKKT.FirstOrDefault(x => x.Id == JudgeId) != null)
            {
                CompJudgeClimModel judge = CompetitionDB.TblCompJudgesClim.Where(x => x.Id == JudgeId).Select(x => new CompJudgeClimModel(x)).FirstOrDefault();
                judge.JudgeName = "" + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == judge.UserId).Name.ToString() + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == judge.UserId).LastName.ToString();
                judge.Routes = CompetitionDB.TblJudgeRoutes.ToArray().Where(x => x.JudgeId == judge.Id).Select(x => new JudgeRouteModel(x)).ToList();
                foreach (JudgeRouteModel r in judge.Routes)
                {
                    r.RouteNumber = CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == r.RouteId).Number;
                }

                return ToJsonOK(judge);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuriama naujas teisėjas*/
        public HttpResponseMessage Post([FromBody]TblCompJudgeClim value)
        {
            CompetitionDB.TblCompJudgesClim.Add(value);
            CompetitionDB.SaveChanges();

            return ToJsonCreated(value);
        }

        /** Redaguojama teisėjo informacija*/
        public HttpResponseMessage Put(int id, [FromBody]TblCompJudgeClim value)
        {
            if (CompetitionDB.TblCompJudgesClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();

                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /**Naikinamas teisėjas varžyboms*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompJudgesClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompJudgesClim.Remove(CompetitionDB.TblCompJudgesClim.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();

                return ToJsonOK("Objetkas ištrintas");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
