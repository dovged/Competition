using Competition.Context;
using Competition.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    [RoutePrefix("/api/competition")]
    public class CompetitionController : BaseAPIController
    {
        /** Grąžina visų varžybų sąrašą;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblCompetitions.AsEnumerable() != null)
            {
                List<CompetitionModel> comps = CompetitionDB.TblCompetitions.ToArray().Select(x => new CompetitionModel(x)).ToList();
                foreach(CompetitionModel c in comps)
                {
                    c.Club = "kkc";//CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
                }
                return ToJsonOK(comps);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vienų varžybų duomenis*/
        public HttpResponseMessage Get(int id)
        {
            // TO DO
            /** Atrušiuoti pagal prisijungusio vartotojo ID*/
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJsonOK(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuria naują varžybų objektą*/
        public HttpResponseMessage Post([FromBody]TblCompetition value)
        { 
            CompetitionDB.TblCompetitions.Add(value);
            CompetitionDB.SaveChanges();

            return ToJsonCreated(value);
        }

        /** Atnaujina varžybų duomenis*/
        public HttpResponseMessage Put(int id, [FromBody]TblCompetition value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;
            CompetitionDB.SaveChanges();

            return ToJsonOK(value);
        }
    }
}
