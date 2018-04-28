﻿using Competition.Context;
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
            if (CompetitionDB.TblCompJudgesClim.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeModel(x)).ToList().Count != 0)
            {
                List<CompJudgeModel> judges = CompetitionDB.TblCompJudgesClim.ToArray().Where(x => x.CompId == compId).Select(x => new CompJudgeModel(x)).ToList();
                foreach(CompJudgeModel j in judges)
                {
                    j.JudgeName = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).Name.ToString() +" " + CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == j.UserId).LastName.ToString();
                }

                return ToJsonOK(judges);
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
            CompetitionDB.TblCompJudgesClim.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /**Naikinamas teisėjas varžyboms*/
        [Route("api/compJudge/{id}")]
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