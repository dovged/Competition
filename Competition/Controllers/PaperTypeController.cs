using Competition.Context;
using Competition.Models;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    [RoutePrefix("api/papertype")]
    public class PaperTypeController : BaseAPIController
    {
        /** Grąžina visų teisėjo lapų tipus sąrašą,
        * tik tuos, kur PaperType.Active == true;*/
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblPaperTypes.AsEnumerable() != null)
            {
                return ToJsonOK(CompetitionDB.TblPaperTypes.ToArray().Where(x => x.Active == true).Select(x => new PaperTypeModel(x)).ToList());
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vieną teisėjo lapo tipą,
         * kuris yra PaperType.Active == true;*/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                return ToJsonOK(CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pridedamas naujas teisėjo lapo tipą;*/
        public HttpResponseMessage Post([FromBody]TblPaperType value)
        {
            CompetitionDB.TblPaperTypes.Add(value);
            CompetitionDB.SaveChanges();
            return ToJsonCreated(value);
        }

        /** Pakoreguojamas tesiėjo lapo tipą;
          * galima koreguoti tesiėjo lapo tipui, kuris yra duomenų bazėje ir yra PaperType.Active == true*/
        public HttpResponseMessage Put(int id, [FromBody]TblPaperType value)
        {
            if (CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Fiktyvus Delete metodas;
          * Teisėjo lapo tipą padarome neaktyviu;
          * CompType.Acrive == false;
          * Tai galima padaryti tik teisėjo lapo tipui, kuris yra duomenų bazėje ir yra PaperType.Active == true*/
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                TblPaperType papertype = CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id);
                papertype.Active = false;
                CompetitionDB.Entry(papertype).State = EntityState.Modified;
                CompetitionDB.SaveChanges();
                return ToJson(papertype);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
