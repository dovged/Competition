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
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblUsers.AsEnumerable() != null)
            {
                return ToJson(CompetitionDB.TblUsers.AsEnumerable());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        [Authorize]
        /** Grazinama vieno User papildoma informacija*/
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJson(CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id));
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }

        [Authorize]
        /** Uzpildoma User papildoma informacija */
        public HttpResponseMessage Post([FromBody]TblUser value)
        {
            /** Prisikiriamas Uzregistruoto User Id */
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string id = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();
            value.UserId = id;
            CompetitionDB.TblUsers.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        /***/
        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody]TblUser value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;
            return ToJson(CompetitionDB.SaveChanges());

        }

    }
}
