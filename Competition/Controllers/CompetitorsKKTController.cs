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
    [RoutePrefix("api/competitnio/id/competitorsKKT")]
    public class CompetitorsKKTController : BaseAPIController
    {
        /** Grąžinamas sąrašas dalyvių vienose varžybose, kurie susimokėjo*/
        public HttpResponseMessage Get(int Compid, int id)
        {
            if (CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == Compid && x.Paid).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
            {
                List<CompetitorsKKTModel> compTeams = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == id).Select(x => new CompetitorsKKTModel(x)).ToList();
                foreach (CompetitorsKKTModel competitor in compTeams)
                {
                    competitor.TeamName = CompetitionDB.TblTeams.Find(competitor.TeamId).Name.ToString();
                }
                return ToJsonOK(compTeams);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Sukuriamas dalyvaujančios komandos varžybose objektas*/
        public HttpResponseMessage Post([FromBody]TblCompetitorsKKT value)
        {
            CompetitionDB.TblCompetitorsKKT.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Ištrinamas objektas - panaikinama registraciją į varžybas*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompetitorsKKT.Remove(CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();
                return ToJsonOK("Objektas ištrintas.");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
