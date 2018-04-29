using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    [RoutePrefix("api/penalty/")]
    public class PenaltyController : BaseAPIController
    {
        /** Grąžina visų baudų sąrašą,
         * tik tuos, kur Penalty.Yra == true;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblPenalties.AsEnumerable() != null)
            {
                 return ToJsonOK(CompetitionDB.TblPenalties.Where(x => x.Yra).ToList());
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vieną baudą,
         * kuri yra Penalty.Yra == true;*/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id && x.Yra) != null)
            {
                return ToJsonOK(CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pridedama nauja bauda*/
        public HttpResponseMessage Post([FromBody]TblPenalty value)
        {
            value.Yra = true;
            CompetitionDB.TblPenalties.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Pakoreguojama bauda;
          * galima koreguoti baudą, kuri yra duomenų bazėje ir yra Penalty.Yra == true*/
        public HttpResponseMessage Put(int id, [FromBody]TblPenalty value)
        {
            value.Yra = true;
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }

        /** Fiktyvus Delete metodas;
          * Bauda padarome neaktyve;
          * Penalty.Yra == false;
          * Tai galima padaryti tik daudai, kuri  yra duomenų bazėje ir yra Penalty.Yra == true*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id) != null)
            {
                TblPenalty penalty = CompetitionDB.TblPenalties.FirstOrDefault(x => x.Id == id);
                penalty.Yra = false;
                CompetitionDB.Entry(penalty).State = EntityState.Modified;
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}