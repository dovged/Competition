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
    [RoutePrefix("/api/competition/id/routeClim")]
    public class RouteClimController : BaseAPIController
    {
        /** Grąžina visas vienų varžybų laipiojimo trasas*/
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == compId) != null)
            {
                if (CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteClimbModel(x)).ToList() != null)
                {
                    return ToJsonOK(CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteClimbModel(x)).ToList());
                }

                return ToJsonNotFound("Tuščias sąrašas.");
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžina vienų varžybų vieną laipiojimo trasą*/
        public HttpResponseMessage Get(int compId, int routeId)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == compId) != null)
            {
                if (CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == routeId) != null)
                {
                    return ToJsonOK(CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == routeId));
                }

                return ToJsonNotFound("Objektas nerastas.");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuria naują laipiojimo trasos objektą*/
        public HttpResponseMessage Post([FromBody]TblRouteClimb value)
        {
            CompetitionDB.TblRoutesClim.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Redaguojama viena laipiojimo trasa*/
        public HttpResponseMessage Put(int routeId, [FromBody]TblRouteClimb value)
        {
            if (CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == routeId) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Ištrinti vienos laipiojimo trasos duomenis*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblRoutesClim.Remove(CompetitionDB.TblRoutesClim.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();
                return ToJsonOK("Objektas ištrintas.");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
