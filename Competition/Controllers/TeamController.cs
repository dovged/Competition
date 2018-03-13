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
    public class TeamController : BaseAPIController
    {
        public HttpResponseMessage Get()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();

            int UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());


            if (CompetitionDB.TblTeams.ToArray().Where(x => x.UserId == UserId).Select(x => new TeamModel(x)).ToList().Count != 0)
            {
                return ToJson(CompetitionDB.TblTeams.ToArray().Where(x => x.UserId == UserId).Select(x => new TeamModel(x)).ToList());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJson(CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == id));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");

        }

        [Authorize]
        public HttpResponseMessage Post([FromBody]TblTeam value)
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();

            value.UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());

            CompetitionDB.TblTeams.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody]TblTeam value)
        {
            if (CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
        }


        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblTeams.Remove(CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found");
        }
    }
}
