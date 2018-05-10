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
    public class CompJudgeController : BaseAPIController
    {
        /** Grąžina sąrašą teisėjų pagal varžybų id*/
        [Route("api/compJudge/{compId}")]
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompJudges.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeModel(x)).ToList().Count != 0)
            {
                List<CompJudgeModel> judges = CompetitionDB.TblCompJudges.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeModel(x)).ToList();
                foreach(CompJudgeModel j in judges)
                {
                    j.JudgeName = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).Name.ToString() +" " + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).LastName.ToString();
                }

                return ToJsonOK(judges);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžina varžybų sąrašą kuriose vartotojas gali teisėjauti*/
        [Route("api/judgeCompetitions/{user}")]
        public HttpResponseMessage Get(string user)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == user).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            if (CompetitionDB.TblCompJudges.ToArray().Where(x => x.UserId == id).Select(x => new CompJudgeModel(x)).ToList().Count != 0)
            {
                List<CompJudgeModel> judges = CompetitionDB.TblCompJudges.ToArray().Where(x => x.UserId == id).Select(x => new CompJudgeModel(x)).ToList();
                List<CompetitionModel> comp = new List<CompetitionModel>();
                foreach (CompJudgeModel j in judges)
                {
                    CompetitionModel c = new CompetitionModel(CompetitionDB.TblCompetitions.Find(j.CompId));
                    comp.Add(c);
                }

                return ToJsonOK(comp);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Sukuriama naujas teisėjas*/
        [Route("api/compJudge/{compId}/{id}")]
        public HttpResponseMessage Post(int compId, int id)
        {
            TblCompJudge value = new TblCompJudge();
            value.CompId = compId;
            value.UserId = id;
            CompetitionDB.TblCompJudges.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /**Naikinamas teisėjas varžyboms*/
        [Route("api/compJudge/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompJudges.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompJudges.Remove(CompetitionDB.TblCompJudges.FirstOrDefault(x => x.Id == id));

                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
