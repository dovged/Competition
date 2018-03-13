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
    [RoutePrefix("api/comptype")]
    public class CompTypeController : BaseAPIController
    {
        /** Grąžina visų laipiojimo varžybų tipus sąrašą,
        * tik tuos, kur CompType.Active == true;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblCompTypes.AsEnumerable() != null)
            {
                return ToJsonOK(CompetitionDB.TblCompTypes.ToArray().Where(x => x.Active == true).Select(x => new CompTypeModel(x)).ToList());
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vieną laipiojimo varžybų tipą,
         * kuris yra CompType.Active == true;*/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblCompTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                return ToJsonOK(CompetitionDB.TblCompTypes.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pridedamas naujas laipiojimo varžybų tipas;*/
        public HttpResponseMessage Post([FromBody]TblCompType value)
        {
            CompetitionDB.TblCompTypes.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Pakoreguojamas laipiojimo varžybų tipas;
          * galima koreguoti laipiojimo varžybų tipui, kuris yra duomenų bazėje ir yra CompType.Active == true*/
        public HttpResponseMessage Put(int id, [FromBody]TblCompType value)
        {
            if (CompetitionDB.TblCompTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Fiktyvus Delete metodas;
          * Laipiojimo varžybų tipą padarome neaktyviu;
          * CompType.Acrive == false;
          * Tai galima padaryti tik teisėjo lapo tipui, kuris yra duomenų bazėje ir yra CompType.Active == true*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                TblCompType CompType = CompetitionDB.TblCompTypes.FirstOrDefault(x => x.Id == id);
                CompType.Active = false;
                CompetitionDB.Entry(CompType).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJson(CompType);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
