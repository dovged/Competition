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
    [RoutePrefix("/api/competition/id/judgesKKT")]
    public class CompJudgeKKTController : BaseAPIController
    {
        /** Grąžina sąrašą teisėjų pagal varžybų id*/
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompJudgesKKT.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeKKTModel(x)).ToList().Count != 0)
            {
                List<CompJudgeKKTModel> judges = CompetitionDB.TblCompJudgesKKT.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeKKTModel(x)).ToList();
                foreach(CompJudgeKKTModel j in judges)
                {
                    j.JudgeName = "" + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).Name.ToString() + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).LastName.ToString();
                    j.RouteName = CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == j.RouteId).Name.ToString();
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
                CompJudgeKKTModel judge = CompetitionDB.TblCompJudgesKKT.Where(x => x.Id == JudgeId).Select(x => new CompJudgeKKTModel(x)).FirstOrDefault();
                judge.JudgeName = "" + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == judge.UserId).Name.ToString() + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).LastName.ToString();
                judge.RouteName = CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == judge.RouteId).Name.ToString();
                
                return ToJsonOK(judge);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuriama naujas teisėjas*/
        public HttpResponseMessage Post([FromBody]TblCompJudgeKKT value)
        {
            CompetitionDB.TblCompJudgesKKT.Add(value);
            CompetitionDB.SaveChanges();

            return ToJsonCreated(value);
        }

        /** Redaguojama teisėjo informacija*/
        public HttpResponseMessage Put(int id, [FromBody]TblCompJudgeKKT value)
        {
            if (CompetitionDB.TblCompJudgesKKT.FirstOrDefault(x => x.Id == id) != null)
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
            if (CompetitionDB.TblCompJudgesKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompJudgesKKT.Remove(CompetitionDB.TblCompJudgesKKT.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();

                return ToJsonOK("Objetkas ištrintas");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
