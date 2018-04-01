using Competition.Context;
using Competition.Models;
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
        /** Grąžinami visi aktyvūs vartotojais su jų pagrindine informacija*/
        [Route("api/user")]
        public HttpResponseMessage Get()
        {            
            if(CompetitionDB.TblUsers.AsEnumerable() != null)
            {
                List<UserModel> users = CompetitionDB.TblUsers.ToArray().Where(x => x.Active == true).Select(x => new UserModel(x)).ToList();
                foreach(UserModel u in users)
                {
                    u.Club = CompetitionDB.TblClubs.Find(u.ClubId).Name;
                    List<RoleModel> roles = CompetitionDB.TblUserRoles.ToArray().Where(x => x.UserId == u.Id).Select(x => new RoleModel(x)).ToList();
                    foreach(RoleModel r in roles)
                    {
                        r.RoleName = CompetitionDB.TblRoles.Find(r.RoleId).Name;
                    }
                    u.roles = roles;
                }
                return ToJsonOK(users);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty list.");

        }

        /** Grazinama vieno User papildoma informacija*/
        public HttpResponseMessage Get(int i)
        {
             ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
             string username = identity.Claims.First().Value;
             string id = CompetitionDB.Users.FirstOrDefault(x => x.UserName == username).Id.ToString();
            if (CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == id) != null)
            {
                return ToJsonOK(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == id));
            }
            else
            {
                TblUser user = new TblUser();
                user.Email = CompetitionDB.Users.FirstOrDefault(x => x.Id == id).UserName;
                user.UserId = id;

                return ToJsonOK(user);
            }
        }
        /** Grąžinami visi nepilnamečiai dalyviai, pagal vieną trenerį (LAIPIOJIMAS)*/
        [Route("api/user/{user}/{n}")]
        public HttpResponseMessage Get(string user, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == user).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            List<UserModel> users = CompetitionDB.TblUsers.ToArray().Where(x => x.TrainerId == id).Select(x => new UserModel(x)).ToList();
           
            return ToJsonOK(users);
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
