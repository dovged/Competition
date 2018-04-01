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
        [Route("api/user/{userName}")]
        public HttpResponseMessage Get(string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            if(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id != 0)
            {
                int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
           
                return ToJsonOK(CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id));
            }

            return ToJsonNotFound("Objektas nerastas.");
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
        [Route("api/user/{userName}")]
        public HttpResponseMessage Post(string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            TblUser user = new TblUser();
            user.Email = userName;
            user.UserId = accountId;
            user.Active = true;
            CompetitionDB.TblUsers.Add(user);
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Atnujinama vartotojo informacija*/
        [Route("api/user/{user}")]
        public HttpResponseMessage Put(string user, [FromBody]TblUser value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;
            return ToJsonOK(CompetitionDB.SaveChanges());

        }

        /** Fiktyvus Delete metodas;
          * Tai galima padaryti tik vartotojui, kuris yra duomenų bazėje ir yra User.Active == true*/
          [Route("api/user/{userName}")]
        public HttpResponseMessage Delete(string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;

            if (CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id && x.Active == true) != null)
            {                
                TblUser user = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id);
                user.Active = false;
                CompetitionDB.Entry(user).State = EntityState.Modified;
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
