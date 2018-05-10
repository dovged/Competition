using Competition.Context;
using Competition.Models;
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
        /** Grąžina visų varžybų sąrašą;*/
        [Route("api/competition")]
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblCompetitions.ToArray().Select(x => new CompetitionModel(x)).ToList().Count != 0)
            {
                List<CompetitionModel> comps = CompetitionDB.TblCompetitions.ToArray().Select(x => new CompetitionModel(x)).ToList();
                foreach(CompetitionModel c in comps)
                {
                    c.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
                }
                return ToJsonOK(comps);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Grąžina vienų varžybų duomenis*/
        [Route("api/competition/{id}")]
        public HttpResponseMessage Get(int id)
        {
            if (CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionModel comp = new CompetitionModel(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id));
                comp.MainJudgeName = CompetitionDB.TblUsers.Find(comp.MainJudgeId).Name + " " + CompetitionDB.TblUsers.Find(comp.MainJudgeId).LastName;
                comp.MainRouteCreatorName = CompetitionDB.TblUsers.Find(comp.MainRouteCreatorId).Name + " " + CompetitionDB.TblUsers.Find(comp.MainRouteCreatorId).LastName;
                comp.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(comp.OrgId).ClubId).Name;

                return ToJsonOK(comp);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Grąžina varžybų duomenis, pagal Org*/
        [Route("api/competition/{user}/{n}")]
        public HttpResponseMessage Get(string user, int n)
        {
            /** Atrušiuoti pagal prisijungusio vartotojo ID*/
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == user).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            if(CompetitionDB.TblCompetitions.ToArray().Where(x => x.OrgId == id).Select(x => new CompetitionModel(x)).ToList().Count != 0)
            {
                List<CompetitionModel> comps = CompetitionDB.TblCompetitions.ToArray().Where(x => x.OrgId == id).Select(x => new CompetitionModel(x)).ToList();
                foreach (CompetitionModel c in comps)
                {
                    c.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
                }

                return ToJsonOK(comps);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
           
          }

        /** Grąžina varžybų sąrašas, kuriose gali dalyvauti nepilnamečiai dalyviai*/
        [Route("api/competitionKids/{userName}")]
        public HttpResponseMessage Get(string userName)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;

            if (CompetitionDB.TblCompetitions.AsEnumerable() != null)
            {
                List<CompetitionModel> comps = new List<CompetitionModel>();
                List<RoleModel> roles = CompetitionDB.TblUserRoles.ToArray().Where(x => x.UserId == id).Select(x => new RoleModel(x)).ToList();
                if(roles.Any(x => x.RoleId == 4))
                {
                    List<CompetitionModel> compsKKT = CompetitionDB.TblCompetitions.ToArray().Where(x => !x.Type).Select(x => new CompetitionModel(x)).ToList();

                    foreach (CompetitionModel c in compsKKT)
                    {
                        c.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
                        comps.Add(c);
                    }
                }

                if(roles.Any(x => x.RoleId == 3))
                {
                    List<CompetitionModel> compsClim = CompetitionDB.TblCompetitions.ToArray().Where(x => x.ClimbType == 3 && x.Type).Select(x => new CompetitionModel(x)).ToList();

                    foreach (CompetitionModel c in compsClim)
                    {
                        c.Club = CompetitionDB.TblClubs.Find(CompetitionDB.TblUsers.Find(c.OrgId).ClubId).Name;
                        comps.Add(c);
                    }
                }
                
                return ToJsonOK(comps);
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }

        /** Sukuria naują varžybų objektą*/
        [Route("api/competition/{userName}")]
        public HttpResponseMessage Post(string userName, [FromBody]TblCompetition value)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            value.OrgId = id;
            value.Open = true;
            value.Update = true;
            CompetitionDB.TblCompetitions.Add(value);
            
            /** Sukuriamos trasos LAIPIOJIMO VARŽYBOMS*/
            if (value.Type)
            {
                CompetitionDB.SaveChanges();

                int compId = CompetitionDB.TblCompetitions.First(x => x.Name == value.Name && x.Date == value.Date).Id;
                
                TblRouteClimb route = new TblRouteClimb();
                if(value.ClimbType == 1)
                {
                    int i;
                    int p = 2;

                    for(i = 0; i < 6; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = p + (p / 2);
                        route.PointsTop = p;
                        route.PointsBonus = p / 2;
                        route.Type = "ATRANKA";
                        route.CompetitionId = compId;
                        p += 2;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }
                }
                else if(value.ClimbType == 2)
                {
                    int i;
                    for (i = 0; i < 6; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = 1;
                        route.PointsTop = 1;
                        route.PointsBonus = 1;
                        route.Type = "ATRANKA";
                        route.CompetitionId = compId;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }
                }
                else
                {
                    int i;
                    for (i = 0; i < 4; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = 1;
                        route.PointsTop = 1;
                        route.PointsBonus = 1;
                        route.Type = "ATRANKA JAUNIMAS";
                        route.CompetitionId = compId;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }

                    for (i = 0; i < 4; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = 1;
                        route.PointsTop = 1;
                        route.PointsBonus = 1;
                        route.Type = "ATRANKA JAUNIAI";
                        route.CompetitionId = compId;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }

                    for (i = 0; i < 4; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = 1;
                        route.PointsTop = 1;
                        route.PointsBonus = 1;
                        route.Type = "ATRANKA JAUNUČIAI";
                        route.CompetitionId = compId;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }
                    for (i = 0; i < 4; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = 1;
                        route.PointsTop = 1;
                        route.PointsBonus = 1;
                        route.Type = "ATRANKA JAUNUOLIAI";
                        route.CompetitionId = compId;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }

                    for (i = 0; i < 4; i++)
                    {
                        route.Number = i + 1;
                        route.PointsFlash = 1;
                        route.PointsTop = 1;
                        route.PointsBonus = 1;
                        route.Type = "ATRANKA VAIKAI";
                        route.CompetitionId = compId;
                        CompetitionDB.TblRoutesClim.Add(route);
                        CompetitionDB.SaveChanges();
                    }
                }
            }
            
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Atnaujina varžybų duomenis*/
        [Route("api/competition/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]TblCompetition value)
        {
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }
    }
}
