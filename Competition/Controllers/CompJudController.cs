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
    public class CompJudController : BaseAPIController
    {
        [Authorize(Roles = "Org")]
        public HttpResponseMessage Get()
        {
            int CompId = Convert.ToInt32(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Open == true).Id.ToString());
            if (CompetitionDB.TblCompJuds.ToArray().Where(x => x.CompId == CompId).Select(x => new CompJudgeKKTModel(x)).ToList().Count != 0)
            {
                return ToJson(CompetitionDB.TblCompJuds.AsEnumerable());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblCompJuds.AsEnumerable() != null)
            {
                return ToJson(CompetitionDB.TblCompJuds.FirstOrDefault(x => x.Id == id));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Post([FromBody]TblCompJudKKT value)
        {
            CompetitionDB.TblCompJuds.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Put(int id, [FromBody]TblCompJudKKT value)
        {
            if (CompetitionDB.TblCompJuds.AsEnumerable() != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompJuds.AsEnumerable() != null)
            {
                CompetitionDB.TblPenalties.Remove(CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }
    }
}
