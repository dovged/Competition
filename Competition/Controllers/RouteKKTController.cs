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
    public class RouteKKTController : BaseAPIController
    {
        /** Grąžina visas vienų varžybų KKT trasas*/
        [Route("api/routeKKT/{compId}")]
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == compId) != null)
            {
                /*   if (CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList() != null)
                   {*/
                List<RouteKKTModel> routes = CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList();
                    return ToJsonOK(routes);
              /*  }

                return ToJsonNotFound("Tuščias sąrašas.");*/
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžina vienų varžybų vieną KKT trasą*/
        public HttpResponseMessage Get(int compId, int routeId)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == compId) != null)
            {
                if (CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == routeId) != null)
                {
                    return ToJsonOK(CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == routeId));
                }

                return ToJsonNotFound("Objektas nerastas.");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuria naują KKT trasos objektą*/
        public HttpResponseMessage Post([FromBody]TblRouteKKT value)
        {
            CompetitionDB.TblRoutesKKT.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Redaguojama viena KKT trasa*/
        public HttpResponseMessage Put(int routeId, [FromBody]TblRouteKKT value)
        {
            if (CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == routeId) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Ištrinti vienos KKT trasos duomenis*/
        [Route("api/routeKKT/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblRoutesKKT.Remove(CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();
                return ToJsonOK("Objektas ištrintas.");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
