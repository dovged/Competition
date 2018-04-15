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
    public class RouteClimController : BaseAPIController
    {
        /** Grąžina visas vienų varžybų laipiojimo trasas, pagal tipą*/
        public HttpResponseMessage Get(int compId, string type)
        {
           
            if (CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteClimbModel(x)).ToList() != null)
            {
                return ToJsonOK(CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteClimbModel(x)).ToList());
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
        [Route("api/routeClim/{compId}")]
        public HttpResponseMessage Post(int compId)
        {
            TblRouteClimb route = new TblRouteClimb();

            for (int i = 0; i < 4; i++)
            {
                route.Number = i + 1;
                route.PointsFlash = 1;
                route.PointsTop = 1;
                route.PointsBonus = 1;
                route.Type = "ATRANKA JAUNIMAS";
                route.CompetitionId = compId;
                CompetitionDB.TblRoutesClim.Add(route);
               // CompetitionDB.SaveChanges();
            }
           // CompetitionDB.TblRoutesClim.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

    }
}
