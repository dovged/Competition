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
        /** Ar vartotojas turi tokią rolę;*/
        [Route("api/role/{user}/{roleId}")]
        public HttpResponseMessage Get(string user, int roleId)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == user).Id;
            if(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString() != null)
            {
                int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
                if (CompetitionDB.TblUserRoles.FirstOrDefault(x => x.UserId == id && x.RoleId == roleId) != null)
                {
                    return ToJsonOK("Rolė yra.");
                }
                else
                {
                    return ToJsonNotFound("Objektas nerastas");
                }
            }
            
            return ToJsonNotFound("Objektas nerastas");
        }

        /** Pridedamas naujas teisėjo lapo tipą;*/
        public HttpResponseMessage Post([FromBody]TblRole value)
        {
            CompetitionDB.TblRoles.Add(value);
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }


        /** Ištrinama vartotojui rolė*/
        [Route("api/userRole/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblUserRoles.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblUserRoles.Remove(CompetitionDB.TblUserRoles.FirstOrDefault(x => x.Id == id));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
