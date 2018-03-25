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
    [RoutePrefix("api/penaltyquantity")]
    public class PenaltyQuantityController : BaseAPIController
    {
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJson(CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == id));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");
        }

        [Authorize]
        public HttpResponseMessage Post([FromBody]TblPenaltyQuantity value)
        {
            CompetitionDB.TblPenaltyQuantities.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody]TblPenaltyQuantity value)
        {
            if (CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");
        }

        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblPenaltyQuantities.Remove(CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");
        }
    }
}
