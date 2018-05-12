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
                    u.Roles = roles;
                }
                return ToJsonOK(users);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grazinama vieno User papildoma informacija*/
        [Route("api/user/{userName}")]
        public HttpResponseMessage Get(string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            if(CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id.ToString() != null)
            {
                int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
                TblUser user = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id);
                UserModel u = new UserModel(user);
                return ToJsonOK(u);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Grąžinama nepilnamečio dalyvio informacija*/
        [Route("api/userKids/{id}/{n}")]
        public HttpResponseMessage Get(int id, int n)
        {
            
            if (CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id).Id.ToString() != null)
            {
                TblUser user = CompetitionDB.TblUsers.FirstOrDefault(x => x.Id == id);
                UserModel u = new UserModel(user);
                return ToJsonOK(u);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Grąžinami visi nepilnamečiai dalyviai, pagal vieną trenerį*/
        [Route("api/user/{user}/{n}")]
        public HttpResponseMessage Get(string user, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == user).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            List<UserModel> users = CompetitionDB.TblUsers.ToArray().Where(x => x.TrainerId == id).Select(x => new UserModel(x)).ToList();
           
            return ToJsonOK(users);
        }

        /** Grąžinamas sąrašas vartotojų be komandos*/
        [Route("api/userNoTeam/{n}")]
        public HttpResponseMessage Get(int n)
        {
            if (CompetitionDB.TblUsers.AsEnumerable() != null)
            {
                List<UserModel> users = CompetitionDB.TblUsers.ToArray().Where(x => x.Active == true && x.TeamId == 0 && x.UserId != "0").Select(x => new UserModel(x)).ToList();
                
                return ToJsonOK(users);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžinamas sąrašas nepilnamečių dalyvių be komandos, pagal jų trenerį*/
        [Route("api/userNoTeam/{userName}/{n}/{m}")]
        public HttpResponseMessage Get(string userName, int n, int m)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.Email == userName).Id;
            if (CompetitionDB.TblUsers.ToArray().Where(x => x.Active == true && x.TeamId == 0 && x.TrainerId == id).Select(x => new UserModel(x)).ToList().Count != 0)
            {
                List<UserModel> users = CompetitionDB.TblUsers.ToArray().Where(x => x.Active == true && x.TeamId == 0 && x.TrainerId == id).Select(x => new UserModel(x)).ToList();

                return ToJsonOK(users);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vartotojų sąrašą pagal klubą*/
        [Route("api/userClub/{n}/{userName}")]
        public HttpResponseMessage Get(int n, string userName)
        {
            if (CompetitionDB.TblUsers.AsEnumerable() != null)
            {
                string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
                int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
                int clubId = CompetitionDB.TblUsers.Find(id).ClubId;
                List<UserModel> users = CompetitionDB.TblUsers.ToArray().Where(x => x.Active == true && x.ClubId == clubId).Select(x => new UserModel(x)).ToList();

                return ToJsonOK(users);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Uzpildoma User papildoma informacija */
        [Route("api/user/{userName}")]
        public HttpResponseMessage Post(string userName, [FromBody]TblUser user)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            user.UserId = accountId;
            user.Active = true;
            CompetitionDB.TblUsers.Add(user);
            CompetitionDB.SaveChanges();
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            TblUserRole role = new TblUserRole();
            role.RoleId = 1;
            role.UserId = id;
            CompetitionDB.TblUserRoles.Add(role);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Treneris užregistruoja naują vartotoją*/
        [Route("api/userTrainee/{userName}/{n}")]
        public HttpResponseMessage Post(string userName, [FromBody]TblUser value, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;

            TblUser user = new TblUser();
            user.Name = value.Name;
            user.LastName = value.LastName;
            user.BirthYear = value.BirthYear;
            user.Lytis = value.Lytis;
            user.Active = true;
            user.UserId = "0";
            user.TrainerId = id;
            user.ClubId = CompetitionDB.TblUsers.Find(id).ClubId;
            user.Email = "";
            user.TelNumber = "";
            user.TeamId = 0;
            CompetitionDB.TblUsers.Add(user);
            CompetitionDB.SaveChanges();
            int idUser = CompetitionDB.TblUsers.FirstOrDefault(x => x.Name == user.Name && x.LastName == user.LastName).Id;
            TblUserRole role = new TblUserRole();
            //Pridedama "NEPILNAMEČIO DALYVIO" rolė
            role.RoleId = 6;
            role.UserId = idUser;
            CompetitionDB.TblUserRoles.Add(role);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Atnaujinama vartotojo informacija*/
        [Route("api/user/{user}")]
        public HttpResponseMessage Put(string user, [FromBody]TblUser value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }

        /** Atnaujinama nepilnamečio dalyvio informacija*/
        [Route("api/userKid/{id}/{trainerName}")]
        public HttpResponseMessage Put(int id, string trainerName, [FromBody]TblUser value)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == trainerName).Id;
            int trainerId = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;

            value.TrainerId = trainerId;
            value.TelNumber = "";
            value.Email = "";
            value.UserId = "0";
            value.Active = true;
            value.ClubId = CompetitionDB.TblUsers.Find(trainerId).ClubId;
            value.TeamId = CompetitionDB.TblUsers.Find(id).TeamId;
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }

        /** Fiktyvus Delete metodas;
          * Tai galima padaryti tik vartotojui, kuris yra duomenų bazėje ir yra User.Active == true*/
        [Route("api/user/{id}")]
        public HttpResponseMessage Delete(int id)
        {
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
