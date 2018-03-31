using Competition.Context;
using Competition.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Competition.Controllers
{
    
    public class CompetitionController : BaseAPIController
    {
        /** Grąžina visų varžybų sąrašą;*/
        [Route("api/competition")]
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblCompetitions.AsEnumerable() != null)
            {
                List<CompetitionModel> comps = CompetitionDB.TblCompetitions.ToArray().Select(x => new CompetitionModel(x)).ToList();
                foreach(CompetitionModel c in comps)
                {
                    c.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
                }
                return ToJsonOK(comps);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vienų varžybų duomenis*/
        [Route("api/competition/{id}")]
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJsonOK(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Grąžina vienų varžybų duomenis, pagal Org*/
        [Route("api/competition/{user}/{n}")]
        public HttpResponseMessage Get(string user, int n)
        {
            /** Atrušiuoti pagal prisijungusio vartotojo ID*/
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == user).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            List<CompetitionModel> comps = CompetitionDB.TblCompetitions.ToArray().Where(x => x.OrgId == id).Select(x => new CompetitionModel(x)).ToList();
            foreach (CompetitionModel c in comps)
            {
                c.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
            }
            return ToJsonOK(comps);
          
          }



          /** Sukuria naują varžybų objektą*/
        [Route("api/competition")]
        public HttpResponseMessage Post([FromBody]TblCompetition value)
        { 
            CompetitionDB.TblCompetitions.Add(value);
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Atnaujina varžybų duomenis*/
        public HttpResponseMessage Put(int id, [FromBody]TblCompetition value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }
    }
}
