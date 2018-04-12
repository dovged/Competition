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
    public class CompetitorsKKTController : BaseAPIController
    {
        /** Grąžinamas sąrašas dalyvių vienose varžybose*/
        [Route("api/competitionsKKT/{compId}")]
        public HttpResponseMessage Get(int compId)
        {
            if (CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
            {
                List<CompetitorsKKTModel> compTeams = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList();
                foreach (CompetitorsKKTModel competitor in compTeams)
                {
                    competitor.TeamName = CompetitionDB.TblTeams.Find(competitor.TeamId).Name.ToString();
                }
                return ToJsonOK(compTeams);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas nesusimokėjusių dalyvių vienose varžybose*/
        [Route("api/competitionsKKTNonPaid/{compId}/{n}")]
        public HttpResponseMessage Get(int compId, string n)
        {
            if (CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => !x.Paid && x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
            {
                List<CompetitorsKKTModel> compTeams = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => !x.Paid && x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList();
                foreach (CompetitorsKKTModel competitor in compTeams)
                {
                    competitor.TeamName = CompetitionDB.TblTeams.Find(competitor.TeamId).Name.ToString();
                }
                return ToJsonOK(compTeams);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas dalyvių vienose varžybose, kurie susimokėjo*/
        [Route("api/competitionsKKT/{compId}/{n}")]
        public HttpResponseMessage Get(int compId, int n)
        {
            if (CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId && x.Paid).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
            {
                List<CompetitorsKKTModel> compTeams = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList();
                foreach (CompetitorsKKTModel competitor in compTeams)
                {
                    competitor.TeamName = CompetitionDB.TblTeams.Find(competitor.TeamId).Name.ToString();
                }
                return ToJsonOK(compTeams);
            }

            return ToJsonNotFound("Tuščias sąrašas.");
        }

        /** Grąžinamas sąrašas varžybų į kurias užsiregistravo vartotojas*/
        [Route("api/climKKT/{userName}/{n}")]
        public HttpResponseMessage Get(string userName, int n)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            int team = CompetitionDB.TblTeams.FirstOrDefault(x => x.TeamCaptainId == id).Id;
            List<CompetitorsKKTModel> climKKT = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.TeamId == team).Select(x => new CompetitorsKKTModel(x)).ToList();
            foreach (CompetitorsKKTModel c in climKKT)
            {
                c.CompetitionName = CompetitionDB.TblCompetitions.Find(c.CompId).Name;
                int ClubId = CompetitionDB.TblUsers.Find(CompetitionDB.TblCompetitions.Find(c.CompId).OrgId).ClubId;
                c.Club = CompetitionDB.TblClubs.Find(ClubId).Name;
                c.Date = CompetitionDB.TblCompetitions.Find(c.CompId).Date;
                c.Update = CompetitionDB.TblCompetitions.Find(c.CompId).Update;
            }

            return ToJsonOK(climKKT);
        }

        /** Grąžinamas dalyvių sąrašas; Naudojama registracijos langui*/
        [Route("api/compKidsKKT/{compId}/{userName}/{m}")]
        public HttpResponseMessage Get(int compId, string userName, int m)
        {
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int Trainerid = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            List<CompKidsKKTModel> kids = new List<CompKidsKKTModel>();
           
            if(CompetitionDB.TblTeams.ToArray().Where(x => x.TeamCaptainId == Trainerid).Select(x => new TeamModel(x)).ToList().Count != 0)
            {
                List<TeamModel> teams = CompetitionDB.TblTeams.ToArray().Where(x => x.TeamCaptainId == Trainerid).Select(x => new TeamModel(x)).ToList();
                foreach (TeamModel t in teams)
                {
                    CompKidsKKTModel k = new CompKidsKKTModel();
                    k.CompId = compId;
                    k.CompName = CompetitionDB.TblCompetitions.Find(compId).Name;
                    k.TeamId = t.Id;
                    k.TeamName = t.Name;

                    if (CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId && x.TeamId == t.Id).Select(x => new CompetitorsKKTModel(x)).ToList().Count != 0)
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

        /** Sukuriamas dalyvaujančios komandos varžybose objektas SUAUGUSIEMS*/
        [Route("api/competitionKKT/{compId}/KKT/{userName}")]
        public HttpResponseMessage Post(int compId, string userName, [FromBody]TblCompetitorsKKT value)
        {
            value.Paid = false;
            value.CompetitionId = compId;
            string accountId = CompetitionDB.Users.FirstOrDefault(x => x.UserName == userName).Id;
            int Id = CompetitionDB.TblUsers.FirstOrDefault(x => x.UserId == accountId).Id;
            value.TeamId = CompetitionDB.TblTeams.FirstOrDefault(x => x.TeamCaptainId == Id).Id;
            CompetitionDB.TblCompetitorsKKT.Add(value);
    
            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Treneris užregistruoja nepilnamečių dalyvių komandą į varžybas*/
        [Route("api/competition/{compId}/climKidsKKT/{id}")]
        public HttpResponseMessage Post(int compId, int id)
        {
            TblCompetitorsKKT value = new TblCompetitorsKKT();
            value.Paid = false;
            value.CompetitionId = compId;
            value.TeamId = id;
            int dateYear = 0;
            int dateNow = 0;
            if (CompetitionDB.TblUsers.Find(id).BirthYear.ToString().Length == 22)
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
            if ((dateNow - dateYear) > 16)
            {
                value.Group = "JAUNIAI";
            }
            else
            {
                value.Group = "JAUNUČIAI";
            }

            CompetitionDB.TblCompetitorsKKT.Add(value);

            return ToJsonCreated(CompetitionDB.SaveChanges());
        }

        /** Pažymima, kad dalyvis susimokėjo*/
        [Route("api/competitionKKTPaid/{id}")]
        public HttpResponseMessage Put(int id)
        {
            TblCompetitorsKKT value = CompetitionDB.TblCompetitorsKKT.Find(id);
            value.Paid = true;
            CompetitionDB.Entry(value).State = EntityState.Modified;

            return ToJsonOK(CompetitionDB.SaveChanges());
        }

        /** Ištrinamas objektas - panaikinama registraciją į varžybas*/
        [Route("api/climKKT/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            if (CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.Id == id) != null)
            {
                CompetitionDB.TblCompetitorsKKT.Remove(CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.Id == id));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }

        /** Trenerio panaikinama registracija nepilnamečio dalyvio į varžybas*/
        [Route("api/climKKT/{compid}/{id}")]
        public HttpResponseMessage Delete(int compid, int id)
        {
            if (CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.TeamId == id && x.CompetitionId == compid) != null)
            {
                CompetitionDB.TblCompetitorsKKT.Remove(CompetitionDB.TblCompetitorsKKT.FirstOrDefault(x => x.TeamId == id && x.CompetitionId == compid));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }

            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
