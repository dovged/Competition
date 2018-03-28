using Competition.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Competition.Controllers
{
    public class UserController : BaseAPIController
    {
        public HttpResponseMessage Get()
        {            
            if(CompetitionDB.TblUsers.AsEnumerable() != null)
            {
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                string username = identity.Claims.First().Value;
                string id = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();
                // return ToJson(CompetitionDB.TblUsers.AsEnumerable());
                return ToJsonOK(id);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        /** Grazinama vieno User papildoma informacija*/
        public HttpResponseMessage Get(int i)
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string id = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();
            /* if (CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == id) != null)
             {
                 return ToJsonOK(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == id));
             }*/

            // return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
            return ToJsonOK(id);

        }


        /** Uzpildoma User papildoma informacija */
        public HttpResponseMessage Post([FromBody]TblUser value)
        {
            /** Prisikiriamas Uzregistruoto User Id */
           /* ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string id = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();
            value.UserId = id;*/
            CompetitionDB.TblUsers.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        /***/
        public HttpResponseMessage Put(int id, [FromBody]TblUser value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;
            return ToJson(CompetitionDB.SaveChanges());

        }

    }
}
