using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Competition.Controllers
{
    public class CompetitionController : BaseAPIController
    {
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblCompetitions.AsEnumerable() != null)
            {
                return ToJson(CompetitionDB.TblCompetitions.AsEnumerable());
            }

            List<RouteKKTModel> list = new List<RouteKKTModel>();
            return ToJson(list);

        }

        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJson(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id));
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }

        /** */
        public HttpResponseMessage Post([FromBody]TblCompetition value)
        {
            /** Prisikiriamas  TblUser Id */
            /* ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
             string username = identity.Claims.First().Value;
             string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();

             value.UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());
             */
            value.UserId = 3;
            CompetitionDB.TblCompetitions.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        /***/
        public HttpResponseMessage Put(int id, [FromBody]TblCompetition value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;
            return ToJson(CompetitionDB.SaveChanges());

        }
    }
}
