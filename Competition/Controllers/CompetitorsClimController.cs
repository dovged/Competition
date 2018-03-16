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
    [RoutePrefix("/api/competition/id/competitorsClim")]
    public class CompetitorsClimController : BaseAPIController
    {
        /** Grąžinamas sąrašas dalyvių vienose varžybose, kurie susimokėjo*/
        public HttpResponseMessage Get(int Compid, int id)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == Compid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == id && x.Paid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = ""+ CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() + CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Sukuriamas dalyvaujančios komandos varžybose objektas*/
        public HttpResponseMessage Post([FromBody]TblCompetitorsClimb value)
        {
            CompetitionDB.TblCompetitorsClim.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Ištrinamas objektas - panaikinama registraciją į varžybas*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompetitorsClim.Remove(CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.Id == id));
                CompetitionDB.SaveChanges();
                return ToJsonOK("Objektas ištrintas.");
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
