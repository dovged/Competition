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
    public class CompJudgeKKTController : BaseAPIController
    {
        /** Grąžina sąrašą teisėjų pagal varžybų id*/
        [Route("api/judgesKKT/{compId}")]
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompJudgesKKT.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeKKTModel(x)).ToList().Count != 0)
            {
                List<CompJudgeKKTModel> judges = CompetitionDB.TblCompJudgesKKT.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeKKTModel(x)).ToList();
                foreach(CompJudgeKKTModel j in judges)
                {
                    j.JudgeName = "" + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).Name.ToString() + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).LastName.ToString();
                }

                return ToJsonOK(judges);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Sukuriama naujas teisėjas*/
        public HttpResponseMessage Post([FromBody]TblCompJudgeKKT value)
        {
            CompetitionDB.TblCompJudgesKKT.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /**Naikinamas teisėjas varžyboms*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompJudgesKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompJudgesKKT.Remove(CompetitionDB.TblCompJudgesKKT.FirstOrDefault(x => x.Id == id));
                
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
