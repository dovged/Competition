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
    
    public class CompetitorsClimController : BaseAPIController
    {
        /** Grąžinamas sąrašas dalyvių vienose varžybose*/
        [Route("api/competitionsclim/{compid}")]
        public HttpResponseMessage Get(int compid)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = ""+ CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() +" "+ CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas dalyvių vienose varžybose*/
        [Route("api/competitionsclimFalse/{compid}/{n}")]
        public HttpResponseMessage Get(int compid, string n)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid && !x.Paid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid && !x.Paid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = "" + CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() + " " + CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas dalyvių vienose varžybose, kurie susimokėjo*/
        [Route("api/competitionsclim/{compid}/{n}")]
        public HttpResponseMessage Get(int compid, int n)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid && x.Paid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = "" + CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() + " " + CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas varžybų į kurias užsiregistravo vartotojas*/
        [Route("api/clim/{userName}/{n}")]
        public HttpResponseMessage Get(string userName, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;

            List<CompetitorsClimModel> clim = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.UserId == id).Select(x => new CompetitorsClimModel(x)).ToList();
            foreach(CompetitorsClimModel c in clim)
            {
                c.CompetitionName = CompetitionDB.TblCompetitions.Find(c.CompId).Name;
            }

            return ToJsonOK(clim);
        }


        /** Sukuriamas dalyvaujančios vartotojo varžybose objektas SUAGUSIEMS*/
        [Route("api/competition/{compId}/clim/{userName}")]
        public HttpResponseMessage Post(int compId, string userName, [FromBody]TblCompetitorsClimb value)
        {
            value.Paid = false;
            value.CompetitionId = compId;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            value.UserId = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            CompetitionDB.TblCompetitorsClim.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Ištrinamas objektas - panaikinama registraciją į varžybas*/
        [Route("api/clim/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompetitorsClim.Remove(CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.Id == id));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pažymima, kad dalyvis susimokėjo*/
        [Route("api/competitionKKTPaid/{id}")]
        public HttpResponseMessage Put(int id)
        {
            TblCompetitorsClimb value = CompetitionDB.TblCompetitorsClim.Find(id);
            value.Paid = false;
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }
    }
}
