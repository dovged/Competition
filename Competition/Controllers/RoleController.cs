using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class RoleController : BaseAPIController
    {
        /** Grąžina vieną teisėjo lapo tipą,
         * kuris yra PaperType.Active == true;*/
       /* public HttpResponseMessage Get(int id)
        {
            /*if (CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {
                return ToJsonOK(CompetitionDB.TblPaperTypes.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");*/
      //  }

        /** Pridedamas naujas teisėjo lapo tipą;*/
        public HttpResponseMessage Post([FromBody]TblRole value)
        {
            CompetitionDB.TblRoles.Add(value);
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

       
        /** Fiktyvus Delete metodas;
          * Teisėjo lapo tipą padarome neaktyviu;
          * CompType.Acrive == false;
          * Tai galima padaryti tik teisėjo lapo tipui, kuris yra duomenų bazėje ir yra PaperType.Active == true*/
      /*  public HttpResponseMessage Delete(int id)*/
       // {
            
       // }
    }
}
