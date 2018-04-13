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
        [Route("api/teams/{userName}")]
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

        /** Grąžinama vienos komandos informacija*/
        [Route("api/team/{userName}/{n}")]
        public HttpResponseMessage Get(string userName, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            int TeamId = CompetitionDB.TblUsers.Find(id).TeamId;
            if (TeamId != 0)
            {
                TeamModel team = new TeamModel(CompetitionDB.TblTeams.Find(TeamId));
                team.Captain = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == team.CaptainId);
                if(CompetitionDB.TblUsers.ToArray().Where(x => x.TeamId == team.Id).Select(x => new UserModel(x)).ToList().Count != 0)
                {
                    team.Teammates = CompetitionDB.TblUsers.ToArray().Where(x => x.TeamId == team.Id).Select(x => new UserModel(x)).ToList();
                }
                
                return ToJsonOK(team);
           }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukurti komandą*/
        [Route("api/team/{userName}")]
        public HttpResponseMessage Post([FromBody]TblTeam value, string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            value.TeamCaptainId = id;
            CompetitionDB.TblTeams.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /**Pridedamas dalyvis į komandą*/
        [Route("api/addMember/{userId}/{teamId}")]
        public HttpResponseMessage Put(int userId, int teamId)
        {
            TblUser user = CompetitionDB.TblUsers.Find(userId);
            user.TeamId = teamId;
            CompetitionDB.Entry(user).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }

        /**Ištrinamas dalyvis iš komandą*/
        [Route("api/removeMember/{userId}")]
        public HttpResponseMessage Put(int userId)
        {
            TblUser user = CompetitionDB.TblUsers.Find(userId);
            user.TeamId = 0;
            CompetitionDB.Entry(user).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }

    }
}
