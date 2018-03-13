using Competition.Context;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    [RoutePrefix("api/club")]
    public class ClubController : BaseAPIController
    {
        /** Grąžina visų klubų sąrašą;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblClubs.AsEnumerable() != null)
            {
                return ToJsonOK(CompetitionDB.TblClubs.AsEnumerable());
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vieną klubą;*/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblClubs.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJsonOK(CompetitionDB.TblClubs.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pridedamas naujas klubas*/
        public HttpResponseMessage Post([FromBody]TblClub value)
        {
            CompetitionDB.TblClubs.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Pakoreguojamas klubas;
          * galima koreguoti klubą, kuris yra duomenų bazėje;*/
        public HttpResponseMessage Put(int id, [FromBody]TblClub value)
        {
            if (CompetitionDB.TblClubs.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}