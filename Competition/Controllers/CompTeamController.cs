using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class CompTeamController : BaseAPIController
    {
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblCompTeams.ToArray().Where(x => x.CompetitionId == id).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
            {
                List<CompetitorsKKTModel> compTeams = CompetitionDB.TblCompTeams.ToArray().Where(x => x.CompetitionId == id).Select(x => new CompetitorsKKTModel(x)).ToList();
                foreach (CompetitorsKKTModel mod in compTeams)
                {
                    mod.TeamName = CompetitionDB.TblTeams.Find(mod.TeamId).Name.ToString();
                }
                return ToJson(compTeams);
            }
            List<RouteKKTModel> list = new List<RouteKKTModel>();
            return ToJson(list);

        }

        public HttpResponseMessage Post([FromBody]TblCompTeam value)
        {
            CompetitionDB.TblCompTeams.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }
    }
}
