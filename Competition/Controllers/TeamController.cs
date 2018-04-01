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
        /**Grąžinamas sąrašas komandų, pagal ID vadovo;
         naduojama treneriams, nes paprasti dalyviai turi tik po vieną komandą*/
        [Route("api/team/{userName}")]
        public HttpResponseMessage Get(string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;

            if (CompetitionDB.TblTeams.ToArray().Where(x => x.TeamCaptainId == id).Select(x => new TeamModel(x)).ToList().Count != 0)
            {
                List<TeamModel> teams = CompetitionDB.TblTeams.ToArray().Where(x => x.TeamCaptainId == id).Select(x => new TeamModel(x)).ToList();

                foreach(TeamModel team in teams)
                {
                    team.Teammates = CompetitionDB.TblUsers.ToArray().Where(x => x.TeamId == team.Id).Select(x => new UserModel(x)).ToList();
                }

                return ToJsonOK(teams);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinama vienos komandos inforamcija*/
        [Route("api/team/{TeamId}")]
        public HttpResponseMessage Get(int TeamId)
        {

            /*if (CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == TeamId) != null)
            {*/
                TeamModel team = CompetitionDB.TblTeams.Where(x => x.Id == TeamId).Select(x => new TeamModel(x)).FirstOrDefault();
                team.Captain = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == team.CaptainId);
                team.Teammates = CompetitionDB.TblUsers.ToArray().Where(x => x.TeamId == team.Id).Select(x => new UserModel(x)).ToList();

                return ToJson(team);
          /*  }

            return ToJsonNotFound("Objektas nerastas.");*/
        }

        /** Sukurti komandą*/
        public HttpResponseMessage Post([FromBody]TblTeam value, int CaptainId)
        {
            /**paimti komandos Id išprisijungusio vartotojo*/
            // TO DO
           /* ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            string username = identity.Claims.First().Value;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();

            value.UserId = Convert.ToInt32(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString());*/

            CompetitionDB.TblTeams.Add(value);
            CompetitionDB.SaveChanges();

            return ToJsonCreated(value);
        }

        /** Redaguoti komandos duomenis*/
        public HttpResponseMessage Put(int id, [FromBody]TblTeam value)
        {
            if (CompetitionDB.TblTeams.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.Entry(value).State = EntityState.Modified;
                CompetitionDB.SaveChanges();

                return ToJsonOK(value);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Ištrinti komandą*/
        /** NUSPRĘSTI AR REIKIA TRINTI*/
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
