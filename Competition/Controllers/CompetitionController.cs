using Competition.Context;
using Competition.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class CompetitionController : BaseAPIController
    {
        /** Grąžina visų varžybų sąrašą;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblCompetitions.AsEnumerable() != null)
            {
                return ToJsonOK(CompetitionDB.TblCompetitions.AsEnumerable());
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vienų varžybų duomenis*/
        public HttpResponseMessage Get(int id)
        {
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
