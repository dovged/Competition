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
    public class TeamMemberController : BaseAPIController
    {
        [Authorize]
        public HttpResponseMessage Get()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();

            int UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());
            int teamId = Convert.ToInt32(CompetitionDB.TblTeams.FirstOrDefault(x => x.UserId == UserId).Id.ToString());
            
            if (CompetitionDB.TblTeamMembers.ToArray().Where(x => x.TeamId == teamId).Select(x => new TeamMemberModel(x)).ToList().Count != 0)
            {
                return ToJson(CompetitionDB.TblTeamMembers.ToArray().Where(x => x.TeamId == teamId).Select(x => new TeamMemberModel(x)).ToList());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblTeamMembers.FirstOrDefault(x => x.Id == id) != null)
            {
                return ToJson(CompetitionDB.TblTeamMembers.FirstOrDefault(x => x.Id == id));
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }

        [Authorize]
        public HttpResponseMessage Post([FromBody]TblTeamMember value)
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();

            int UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());
            value.TeamId = Convert.ToInt32(CompetitionDB.TblTeams.FirstOrDefault(x => x.UserId == UserId).Id.ToString());
            CompetitionDB.TblTeamMembers.Add(value);
            return ToJson(CompetitionDB.SaveChanges());
        }

        [Authorize]
        public HttpResponseMessage Put(int id, [FromBody]TblTeamMember value)
        {
            if (CompetitionDB.TblTeamMembers.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                return ToJson(CompetitionDB.SaveChanges());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");

        }

        [Authorize]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblTeamMembers.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblTeamMembers.Remove(CompetitionDB.TblTeamMembers.FirstOrDefault(x => x.Id == id));
                return ToJson(CompetitionDB.SaveChanges());
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Item not found.");
        }
    }
}
