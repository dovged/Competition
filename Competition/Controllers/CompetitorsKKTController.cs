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
    public class CompetitorsKKTController : BaseAPIController
    {
        /** Grąžinamas sąrašas dalyvių vienose varžybose*/
        [Route("api/competitionsKKT/{compId}")]
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
            {
                List<CompetitorsKKTModel> compTeams = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList();
                foreach (CompetitorsKKTModel competitor in compTeams)
                {
                    competitor.TeamName = CompetitionDB.TblTeams.Find(competitor.TeamId).Name.ToString();
                }
                return ToJsonOK(compTeams);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas varžybų į kurias užsiregistravo vartotojas*/
        [Route("api/climKKT/{userName}/{n}")]
        public HttpResponseMessage Get(string userName, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            int team = CompetitionDB.TblTeams.FirstOrDefault(x => x.TeamCaptainId == id).Id;
            List<CompetitorsKKTModel> climKKT = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.TeamId == team).Select(x => new CompetitorsKKTModel(x)).ToList();
            foreach (CompetitorsKKTModel c in climKKT)
            {
                c.CompetitionName = CompetitionDB.TblCompetitions.Find(c.CompId).Name;
            }

            return ToJsonOK(climKKT);
        }

        /** Sukuriamas dalyvaujančios komandos varžybose objektas SUAUGUSIEMS*/
        [Route("api/competitionKKT/{compId}/KKT/{userName}")]
        public HttpResponseMessage Post(int compId, string userName, [FromBody]TblCompetitorsKKT value)
        {
            value.Paid = false;
            value.CompetitionId = compId;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int Id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            value.TeamId = CompetitionDB.TblTeams.FirstOrDefault(x => x.TeamCaptainId == Id).Id;
            CompetitionDB.TblCompetitorsKKT.Add(value);
    
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Ištrinamas objektas - panaikinama registraciją į varžybas*/
        [Route("api/climKKT/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompetitorsKKT.Remove(CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.Id == id));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
