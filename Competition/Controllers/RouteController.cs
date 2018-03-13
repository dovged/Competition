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
    public class RouteController : BaseAPIController
    {

        public HttpResponseMessage Get()
        {
            int CompId = Convert.ToInt32(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Open == true).Id.ToString());

            if (CompetitionDB.TblRoutes.ToArray().Where(x => x.CompetitionId == CompId).Select(x => new RouteKKTModel(x)).ToList().Count != 0)
            {
                return ToJson(CompetitionDB.TblRoutes.ToArray().Where(x => x.CompetitionId == CompId).Select(x => new RouteKKTModel(x)).ToList());
            }
            List<RouteKKTModel> list = new List<RouteKKTModel>();
            return ToJson(list);
        }


        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblRoutes.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJson(CompetitionDB.TblRoutes.FirstOrDefault(x => x.Id == id));
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");
        }


        public HttpResponseMessage Post([FromBody]TblRouteKKT value)
        {
            value.CompetitionId = Convert.ToInt32(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Open == true).Id.ToString());
            CompetitionDB.TblRoutes.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }


        public HttpResponseMessage Put(int id, [FromBody]TblRouteKKT value)
        {
            if (CompetitionDB.TblRoutes.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");
        }


        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblRoutes.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblRoutes.Remove(CompetitionDB.TblRoutes.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }
    }
}
