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
            if (CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList() != null)
            {
                List<RouteKKTModel> routes = CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList();

                return ToJsonOK(routes);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžina vienų varžybų vieną KKT trasą*/
        [Route("api/competition/{compId}/routeKKT/{routeId}")]
        public HttpResponseMessage Get(int compId, int routeId)
        {
            if (CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == routeId) != null)
            {
                return ToJsonOK(CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == routeId));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuria naują KKT trasos objektą*/
        [Route("api/routeKKT")]
        public HttpResponseMessage Post([FromBody]TblRouteKKT value)
        {
            CompetitionDB.TblRoutesKKT.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Redaguojama viena KKT trasa*/
        [Route("api/routeKKT/{routeId}")]
        public HttpResponseMessage Put(int routeId, [FromBody]TblRouteKKT value)
        {
            if (CompetitionDB.TblRoutesKKT.FirstOrDefault(x => x.Id == routeId) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;

                return ToJsonOK(CompetitionDB.SaveChanges());
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
             
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
