using Competition.Context;
using Competition.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    [RoutePrefix("api/penalty")]
    public class PenaltyController : BaseAPIController
    {
        /** Grąžina visų baudų sąrašą,
         * tik tuos, kur Penalty.Active == true;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblPenalties.AsEnumerable() != null)
            {
                 return ToJsonOK(CompetitionDB.TblPenalties.ToArray().Where(x => x.Active == true).Select(x => new PenaltyModel(x)).ToList());
            }

             return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vieną baudą,
         * kuri yra Penalty.Active == true;*/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                return ToJsonOK(CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pridedama nauja bauda*/
        public HttpResponseMessage Post([FromBody]TblPenalty value)
        {
            CompetitionDB.TblPenalties.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Pakoreguojama bauda;
          * galima koreguoti baudą, kuri yra duomenų bazėje ir yra Penalty.Active == true*/
        public HttpResponseMessage Put(int id, [FromBody]TblPenalty value)
        {
            if (CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Fiktyvus Delete metodas;
          * Bauda padarome neaktyve;
          * Penalty.Acrive == false;
          * Tai galima padaryti tik daudai, kuri  yra duomenų bazėje ir yra Penalty.Active == true*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                TblPenalty penalty = CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id);
                penalty.Active = false;
                CompetitionDB.Entry(penalty).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJson(penalty);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
