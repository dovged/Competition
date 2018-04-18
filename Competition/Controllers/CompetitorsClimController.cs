using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class CompetitorsClimController : BaseAPIController
    {
        /** Grąžinamas sąrašas dalyvių vienose varžybose*/
        [Route("api/competitionsclim/{compid}")]
        public HttpResponseMessage Get(int compid)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = ""+ CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() +" "+ CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas nesusimokėjusių dalyvių vienose varžybose*/
        [Route("api/competitionsclimNonPaid/{compid}/{n}")]
        public HttpResponseMessage Get(int compid, string n)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid && !x.Paid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid && !x.Paid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = "" + CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() + " " + CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas dalyvių vienose varžybose, kurie susimokėjo*/
        [Route("api/competitionsclim/{compid}/{n}")]
        public HttpResponseMessage Get(int compid, int n)
        {
            if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid && x.Paid).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> compUsers = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compid).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach (CompetitorsClimModel competitor in compUsers)
                {
                    competitor.ClimberName = "" + CompetitionDB.TblUsers.Find(competitor.ClimberId).Name.ToString() + " " + CompetitionDB.TblUsers.Find(competitor.ClimberId).LastName.ToString();
                }
                return ToJsonOK(compUsers);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas varžybų į kurias užsiregistravo vartotojas*/
        [Route("api/clim/{userName}/{n}")]
        public HttpResponseMessage Get(string userName, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            if(CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.UserId == id).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
            {
                List<CompetitorsClimModel> clim = CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.UserId == id).Select(x => new CompetitorsClimModel(x)).ToList();
                foreach(CompetitorsClimModel c in clim)
                {
                    c.CompetitionName = CompetitionDB.TblCompetitions.Find(c.CompId).Name;
                    int ClubId = CompetitionDB.TblUsers.Find(CompetitionDB.TblCompetitions.Find(c.CompId).OrgId).ClubId;
                    c.Date = CompetitionDB.TblCompetitions.Find(c.CompId).Date;
                    c.Club = CompetitionDB.TblClubs.Find(ClubId).Name;
                    c.Update = CompetitionDB.TblCompetitions.Find(c.CompId).Update;
                }

                return ToJsonOK(clim);
            }
            return ToJsonNotFound("Objektas nerastas");
        }

        /** Grąžinamas dalyvių sąrašas; Naudojama registracijos langui*/
        [Route("api/compKidsClim/{compId}/{userName}/{m}")]
        public HttpResponseMessage Get(int compId, string userName, int m)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int Trainerid = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            List<CompKidsClimbModel> kids = new List<CompKidsClimbModel>();

            if (CompetitionDB.TblUsers.ToArray().Where(x => x.TrainerId == Trainerid).Select(x => new UserModel(x)).ToList().Count != 0)
            {
                List<UserModel> teams = CompetitionDB.TblUsers.ToArray().Where(x => x.TrainerId == Trainerid).Select(x => new UserModel(x)).ToList();
                foreach (UserModel t in teams)
                {
                    CompKidsClimbModel k = new CompKidsClimbModel();
                    k.CompId = compId;
                    k.CompName = CompetitionDB.TblCompetitions.Find(compId).Name;
                    k.ClimberId = t.Id;
                    k.ClimberName = t.Name + " " + t.LastName;

                    if (CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compId && x.UserId == t.Id).Select(x => new CompetitorsClimModel(x)).ToList().Count != 0)
                    {
                        k.Register = true;
                    }
                    else
                    {
                        k.Register = false;
                    }
                    kids.Add(k);
                }

                return ToJsonOK(kids);
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Sukuriamas dalyvaujančios vartotojo varžybose objektas SUAGUSIEMS*/
        [Route("api/competition/{compId}/clim/{userName}")]
        public HttpResponseMessage Post(int compId, string userName, [FromBody]TblCompetitorsClimb value)
        {
            value.Paid = false;
            value.CompetitionId = compId;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            value.UserId = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            value.Paid = false;
            value.Tag = false;
            CompetitionDB.TblCompetitorsClim.Add(value);
            CompetitionDB.SaveChanges();
            List<RouteClimbModel> routes = CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId && x.Type == "ATRANKA").Select(x => new RouteClimbModel(x)).ToList();
            TblJudgesPaperClim paper = new TblJudgesPaperClim();
            foreach (RouteClimbModel r in routes)
            {
                paper.JudgeId = 0;
                paper.RouteId = r.Id;
                paper.TopAttempt = 0;
                paper.BonusAttempt = 0;
                paper.ClimberId = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
                paper.Date = DateTime.Now;
                paper.TypeId = 0;
                CompetitionDB.TblJudgesPapersClimb.Add(paper);
                CompetitionDB.SaveChanges();
            }

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Treneris užregistruoja nepilnametį dalyvį į varžybas*/
        [Route("api/competition/{compId}/climKids/{id}")]
        public HttpResponseMessage Post(int compId, int id)
        {
            TblCompetitorsClimb value = new TblCompetitorsClimb();
            value.Paid = false;
            value.CompetitionId = compId;
            value.UserId = id;
            int dateYear = 0;
            int dateNow = 0;
            if(CompetitionDB.TblUsers.Find(id).BirthYear.ToString().Length == 22)
            {
                dateYear = Convert.ToInt32(CompetitionDB.TblUsers.Find(id).BirthYear.ToString().Substring(6, 4));
            }
            else if (CompetitionDB.TblUsers.Find(id).BirthYear.ToString().Length == 20)
            {
                dateYear = Convert.ToInt32(CompetitionDB.TblUsers.Find(id).BirthYear.ToString().Substring(4, 4));
            }
            else
            {
                dateYear = Convert.ToInt32(CompetitionDB.TblUsers.Find(id).BirthYear.ToString().Substring(5, 4));
            }

            if (DateTime.Now.ToString().Length == 22)
            {
                dateNow = Convert.ToInt32(DateTime.Now.ToString().Substring(6, 4));
            }
            else if (DateTime.Now.ToString().Length == 20)
            {
                dateNow = Convert.ToInt32(DateTime.Now.ToString().Substring(5, 4));
            }
            else
            {
                dateNow = Convert.ToInt32(DateTime.Now.ToString().Substring(4, 4));
            }

            if((dateNow - dateYear) > 17)
            {
                value.Group = "JAUNIMAS";
            }
            else if((dateNow - dateYear) > 15)
            {
                value.Group = "JAUNIAI";
            }
            else if((dateNow - dateYear) > 13)
            {
                value.Group = "JAUNUOLIAI";
            }
            else if((dateNow - dateYear) > 11)
            {
                value.Group = "JAUNUČIAI";
            }
            else
            {
                value.Group = "VAIKAI";
            }

            CompetitionDB.TblCompetitorsClim.Add(value);

            List<RouteClimbModel> routes = CompetitionDB.TblRoutesClim.ToArray().Where(x => x.CompetitionId == compId && x.Type == value.Group).Select(x => new RouteClimbModel(x)).ToList();
            TblJudgesPaperClim paper = new TblJudgesPaperClim();
            foreach (RouteClimbModel r in routes)
            {
                paper.JudgeId = 0;
                paper.RouteId = r.Id;
                paper.TopAttempt = 0;
                paper.BonusAttempt = 0;
                paper.ClimberId = id;
                paper.Date = DateTime.Now;
                paper.TypeId = 0;
                CompetitionDB.TblJudgesPapersClimb.Add(paper);
                CompetitionDB.SaveChanges();
            }

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Ištrinamas objektas - panaikinama registraciją į varžybas*/
        [Route("api/clim/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompetitorsClim.Remove(CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.Id == id));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Trenerio panaikinama registracija nepilnamečio dalyvio į varžybas*/
        [Route("api/clim/{compid}/{id}")]
        public HttpResponseMessage Delete(int compid, int id)
        {
            if (CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.UserId == id && x.CompetitionId == compid) != null)
            {
                CompetitionDB.TblCompetitorsClim.Remove(CompetitionDB.TblCompetitorsClim.FirstOrDefault(x => x.UserId == id && x.CompetitionId == compid));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Pažymima, kad dalyvis susimokėjo*/
        [Route("api/competitionClimPaid/{id}")]
        public HttpResponseMessage Put(int id)
        {
            TblCompetitorsClimb value = CompetitionDB.TblCompetitorsClim.Find(id);
            value.Paid = true;
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }
    }
}
