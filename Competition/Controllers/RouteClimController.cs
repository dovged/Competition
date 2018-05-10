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
        [Route("api/routeClim/{compId}/{type}")]
        public HttpResponseMessage Get(int compId, string type)
        {
            if (CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId && x.Type == type).Select(x => new RouteClimbModel(x)).ToList() != null)
            {
                return ToJsonOK(CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId && x.Type == type).Select(x => new RouteClimbModel(x)).ToList());
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }
    }
}
