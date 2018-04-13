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
                }

                return ToJsonOK(judges);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Sukuriama naujas teisėjas*/
        public HttpResponseMessage Post([FromBody]TblCompJudgeClim value)
        {
            CompetitionDB.TblCompJudgesClim.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /**Naikinamas teisėjas varžyboms*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompJudgesClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompJudgesClim.Remove(CompetitionDB.TblCompJudgesClim.FirstOrDefault(x => x.Id == id));

                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
