using Competition.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class PenaltyController : BaseAPIController
    {
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblPenalties.AsEnumerable() != null)
            {
                return ToJson(CompetitionDB.TblPenalties.AsEnumerable());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblPenalties.AsEnumerable() != null)
            {
                return ToJson(CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Post([FromBody]TblPenalty value)
        {
            CompetitionDB.TblPenalties.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Put(int id, [FromBody]TblPenalty value)
        {
            if (CompetitionDB.TblPenalties.AsEnumerable() != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }

        [Authorize(Roles = "Org")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblPenalties.AsEnumerable() != null)
            {
                CompetitionDB.TblPenalties.Remove(CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }
    }
}
