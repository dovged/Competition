using Competition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{

    public class ResultController : BaseAPIController
    {
        /** Bendras rezultatų metodas*/
        [Route("api/results/{compId}/{compType}/{resultType}")]
        public HttpResponseMessage Get(int compId, int compType, int resultType)
        {
            /**LAIPIOJIMO VARŽYBOS*/
            if (CompetitionDB.TblCompetitions.Find(compId).Type)
            {
                switch (compType)
                {
                    case 1:
                        List<ResultsClimbKaboriai> results = KaboriaiResults(compId, resultType);
                        return ToJsonOK(results);
                    case 2:
                        List<ResultsClimLBC> resultsLBC = LBCResults(compId, resultType);
                        return ToJsonOK(resultsLBC);
                    case 3:
                        List<ResultsClim> resultsClim = ResultsClim(compId, resultType);
                        return ToJsonOK(resultsClim);
                }
            }
            /**KKT VARŽYBOS*/
            else
            {
                switch (compType)
                {
                    case 1:
                        List<ResultModel> resultsKKT = ResultsKKT1(compId, resultType);
                        return ToJsonOK(resultsKKT);
                    case 2:
                        List<ResultModel> resultsKKT2 = ResultsKKT2(compId, resultType);
                        return ToJsonOK(resultsKKT2);
                }
            }

            return ToJsonNotFound("Objetkas nerastas");
        }

        /** LAIPIOJIMO VARŽYBŲ KABORIŲ TIPO rezultatų skaičiavimo algoritmas*/
        public List<ResultsClimbKaboriai> KaboriaiResults(int compId, int resultType)
        {
            //PRIDĖTI RŪŠIAVIMĄ PAGAL LYTĮ
            List<ResultsClimbKaboriai> results = new List<ResultsClimbKaboriai>();

            foreach(CompetitorsClimModel u in CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsClimModel(x)).ToList())
            {
                ResultsClimbKaboriai r = new ResultsClimbKaboriai();
                r.Climber = CompetitionDB.TblUsers.Find(u.ClimberId).Name + " " + CompetitionDB.TblUsers.Find(u.ClimberId).LastName;
                int ClimberId = u.ClimberId;

                // PAKEISTI PO TO Į 19, NES 18 TRASŲ ATRANKOJE
                for(int i = 1; i < 6; i++)
                {
                    int RouteId = CompetitionDB.TblRoutesClim.First(x => x.CompetitionId == compId && x.Number == i).Id;
                  /*  if(CompetitionDB.TblJudgesPapersClimb.First(x => x.RouteId == RouteId && x.ClimberId == ClimberId).ToString().Length != 0)
                    {*/
                        JudgesPaperClimbModel paper = new JudgesPaperClimbModel(CompetitionDB.TblJudgesPapersClimb.First(x => x.RouteId == RouteId && x.ClimberId == ClimberId));
                        if (paper.TopAttempt == 1)
                        {
                            r.RoutePonts[i-1] = CompetitionDB.TblRoutesClim.Find(RouteId).PointsFlash;
                            r.PlusPoints(CompetitionDB.TblRoutesClim.Find(RouteId).PointsFlash);
                        }
                        else if(paper.TopAttempt == 2)
                        {
                            r.RoutePonts[i-1] = CompetitionDB.TblRoutesClim.Find(RouteId).PointsTop;
                            r.PlusPoints(CompetitionDB.TblRoutesClim.Find(RouteId).PointsTop);
                        }
                        else if(paper.BonusAttempt > 0)
                        {
                            r.RoutePonts[i-1] = CompetitionDB.TblRoutesClim.Find(RouteId).PointsBonus;
                            r.PlusPoints(CompetitionDB.TblRoutesClim.Find(RouteId).PointsBonus);
                        }
                    //}
                }
                results.Add(r);
            }
            List<ResultsClimbKaboriai> resultsOrder = results.OrderByDescending(x => x.SumPoints).ToList();
            int p = 1;
            foreach (ResultsClimbKaboriai res in resultsOrder)
            {
                res.Place = p;
                p++;
            }

            return resultsOrder;
        }

        /** LAIPIOJIMO VARŽYBŲ LBČ TIPO rezultatų skaičiavimo algoritmas*/
        public List<ResultsClimLBC> LBCResults(int compId, int resultType)
        {
            //PRIDĖTI RŪŠIAVIMĄ PAGAL LYTĮ
            List<ResultsClimLBC> results = new List<ResultsClimLBC>();

            foreach (CompetitorsClimModel u in CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsClimModel(x)).ToList())
            {
                ResultsClimLBC r = new ResultsClimLBC();
                r.Climber = CompetitionDB.TblUsers.Find(u.ClimberId).Name + " " + CompetitionDB.TblUsers.Find(u.ClimberId).LastName;
                int ClimberId = u.ClimberId;

                // PAKEISTI PO TO Į 51, NES 50 TRASŲ ATRANKOJE
                for (int i = 1; i < 6; i++)
                {
                    int RouteId = CompetitionDB.TblRoutesClim.First(x => x.CompetitionId == compId && x.Number == i).Id;
                    /*  if(CompetitionDB.TblJudgesPapersClimb.First(x => x.RouteId == RouteId && x.ClimberId == ClimberId).ToString().Length != 0)
                      {*/
                    JudgesPaperClimbModel paper = new JudgesPaperClimbModel(CompetitionDB.TblJudgesPapersClimb.First(x => x.RouteId == RouteId && x.ClimberId == ClimberId));
                    if (paper.TopAttempt == 1)
                    {
                        r.PlusFlash();
                        r.PlusTop();
                        r.PlusBonus();
                    }
                    else if (paper.TopAttempt == 2)
                    {
                        r.PlusTop();
                        r.PlusBonus();
                    }
                    else if (paper.BonusAttempt > 0)
                    {
                        r.PlusBonus();
                    }
                    //}
                }
                results.Add(r);
            }
            List<ResultsClimLBC> resultsOrder = results.OrderByDescending(x => x.SumAttemptFlash).ThenByDescending(x => x.SumAttemptTop).ThenByDescending(x => x.SumAttemptBonus).ToList();
            int p = 1;
            foreach (ResultsClimLBC res in resultsOrder)
            {
                res.Place = p;
                p++;
            }

            return resultsOrder;
        }

        /** LAIPIOJIMO VARŽYBŲ LBJT IR FINALO TIPO rezultatų skaičiavimo algoritmas*/
        public List<ResultsClim> ResultsClim(int compId, int resultType)
        {
            //PRIDĖTI RŪŠIAVIMĄ PAGAL LYTĮ
            List<ResultsClim> results = new List<ResultsClim>();

            foreach (CompetitorsClimModel u in CompetitionDB.TblCompetitorsClim.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsClimModel(x)).ToList())
            {
                ResultsClim r = new ResultsClim();
                r.Climber = CompetitionDB.TblUsers.Find(u.ClimberId).Name + " " + CompetitionDB.TblUsers.Find(u.ClimberId).LastName;
                int ClimberId = u.ClimberId;

                // PAKEISTI PO TO Į 5, NES 4 TRASŲ ATRANKOJE
                for (int i = 1; i < 4; i++)
                {
                    int RouteId = CompetitionDB.TblRoutesClim.First(x => x.CompetitionId == compId && x.Number == i).Id;
                    /*  if(CompetitionDB.TblJudgesPapersClimb.First(x => x.RouteId == RouteId && x.ClimberId == ClimberId).ToString().Length != 0)
                      {*/
                    JudgesPaperClimbModel paper = new JudgesPaperClimbModel(CompetitionDB.TblJudgesPapersClimb.First(x => x.RouteId == RouteId && x.ClimberId == ClimberId));
                    if (paper.TopAttempt > 0)
                    {
                        r.AddTop(paper.TopAttempt);
                    }

                    if (paper.BonusAttempt > 0)
                    {
                        r.AddBonus(paper.BonusAttempt);
                    }
                    //}
                }
                results.Add(r);
            }
            List<ResultsClim> resultsOrder = results.OrderByDescending(x => x.TopNumber).ThenByDescending(x => x.BonusNumber).ThenBy(x => x.TopAttempt).ThenBy(x => x.BonusAttempt).ToList();
            int p = 1;
            foreach (ResultsClim res in resultsOrder)
            {
                res.Place = p;
                p++;
            }

            return resultsOrder;
        }

        /** KKT VARŽYBŲ PIRMAS REZULTATŲ SKAIČIAVIMO ALGORITMAS*/
        public List<ResultModel> ResultsKKT1(int compId, int resultType)
        {
            List<ResultModel> results = new List<ResultModel>();

            List<CompetitorsKKTModel> compTeam = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList();
            List<RouteKKTModel> routes = CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList();

            foreach (var ct in compTeam)
            {
                ResultModel result = new ResultModel();
                int teamId = ct.TeamId;
                result.TeamName = CompetitionDB.TblTeams.Find(teamId).Name.ToString();
                List<JudgesPaperKKTModel> judgesPapers = new List<JudgesPaperKKTModel>();
                int sumTime = 0;
                foreach (var r in routes)
                {
                    JudgesPaperKKTModel jp = CompetitionDB.TblJudgesPapersKKT.ToArray().Where(x => x.RouteId == r.Id && x.TeamId == teamId).Select(x => new JudgesPaperKKTModel(x)).FirstOrDefault();
                    jp.RouteName = r.Name;
                    if (TimeCompare(MaxTimeRoute(r.Id), jp.Time) && jp.Time != "00:00:00")
                    {
                        jp.Points = r.Points;
                        int penaltySum = PointsFromPenalties(jp.Id);
                        jp.PenaltySum = penaltySum;
                        int routeTimeFromJP = TimeToSec(jp.Time);
                        sumTime += (penaltySum * 30) + routeTimeFromJP;
                        if (penaltySum == 0)
                        {
                            jp.TimewithPenalty = jp.Time;
                        }
                        else
                        {
                            jp.TimewithPenalty = SecToTime((penaltySum * 30) + routeTimeFromJP);
                        }

                        result.PlusPoints(r.Points);
                    }
                    else
                    {
                        jp.Time = "00:00:00";
                        jp.TimewithPenalty = "00:00:00";
                    }

                    judgesPapers.Add(jp);
                }
                result.JudgesPapers = judgesPapers;
                result.TimeSum = SecToTime(sumTime);
                results.Add(result);


            }
            List<ResultModel> resultsOrder = results.OrderByDescending(x => x.PointsSum).ThenBy(x => x.TimeSum).ToList();
            int i = 1;
            foreach (ResultModel res in resultsOrder)
            {
                res.Place = i;
                i++;
            }

            return resultsOrder;
        }

        /** KKT VARŽYBŲ PIRMAS REZULTATŲ SKAIČIAVIMO ALGORITMAS*/
        public List<ResultModel> ResultsKKT2(int compId, int resultType)
        {
            List<ResultModel> results = new List<ResultModel>();

            List<CompetitorsKKTModel> compTeam = CompetitionDB.TblCompetitorsKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new CompetitorsKKTModel(x)).ToList();
            List<RouteKKTModel> routes = CompetitionDB.TblRoutesKKT.ToArray().Where(x => x.CompetitionId == compId).Select(x => new RouteKKTModel(x)).ToList();

            foreach (var ct in compTeam)
            {
                ResultModel result = new ResultModel();
                int teamId = ct.TeamId;
                result.TeamName = CompetitionDB.TblTeams.Find(teamId).Name.ToString();
                List<JudgesPaperKKTModel> judgesPapers = new List<JudgesPaperKKTModel>();
                int sumTime = 0;
                foreach (var r in routes)
                {
                    JudgesPaperKKTModel jp = CompetitionDB.TblJudgesPapersKKT.ToArray().Where(x => x.RouteId == r.Id && x.TeamId == teamId).Select(x => new JudgesPaperKKTModel(x)).FirstOrDefault();
                    jp.RouteName = r.Name;
                    if (TimeCompare(MaxTimeRoute(r.Id), jp.Time) && jp.Time != "00:00:00")
                    {
                        jp.Points = r.Points;
                        int penaltySum = PointsFromPenalties(jp.Id);
                        jp.PenaltySum = penaltySum;
                        int routeTimeFromJP = TimeToSec(jp.Time);
                        sumTime += routeTimeFromJP;

                        result.PlusPoints(r.Points);
                        result.MinusPoints(penaltySum);
                    }
                    else
                    {
                        jp.Time = "00:00:00";
                        jp.TimewithPenalty = "00:00:00";
                    }

                    judgesPapers.Add(jp);
                }
                result.JudgesPapers = judgesPapers;
                result.TimeSum = SecToTime(sumTime);
                results.Add(result);
            }
            List<ResultModel> resultsOrder = results.OrderByDescending(x => x.TimeSum).ThenByDescending(x => x.PointsSum).ToList();
            int i = 1;
            foreach (ResultModel res in resultsOrder)
            {
                res.Place = i;
                i++;
            }

            return resultsOrder;
        }

        // get penalties for the judgesPaper
        public List<PenaltyQuantityModel> AllPenaltyQuantities(int JudgesPaperId)
        {
            return CompetitionDB.TblPenaltyQuantities.ToArray().Where(x => x.JudgesPaperId == JudgesPaperId).Select(x => new PenaltyQuantityModel(x)).ToList();
        }

        /** compare two times (string)
        * @param time1 - routes maxtime;
        * @param time2 - teams time;
        */
        public bool TimeCompare(string time1, string time2)
        {
            return TimeToSec(time1) >= TimeToSec(time2);
        }

        // convert time(string) to seconds time format (hh:mm:ss)
        public int TimeToSec(string time)
        {
            return (Convert.ToInt32(time.Substring(0, 2)) * 3600) + (Convert.ToInt32(time.Substring(3, 2)) * 60) + (Convert.ToInt32(time.Substring(6, 2)));
        }

        // convert setods to time(string)
        public string SecToTime(int time)
        {
            int val = time / 3600;
            int min = (time - (val * 3600)) / 60;
            int s = time - (val * 3600) - (min * 60);
            string timeS = "";
            if (val >= 10)
            {
                timeS = val.ToString() + ":";
            }
            else
            {
                timeS = "0" + val.ToString() + ":";
            }
            if (min >= 10)
            {
                timeS += min.ToString() + ":";
            }
            else
            {
                timeS += "0" + min.ToString() + ":";
            }
            if (s >= 10)
            {
                timeS += s.ToString();
            }
            else
            {
                timeS += "0" + s.ToString();
            }
            return timeS;
        }

        // Get maxtime of route
        public string MaxTimeRoute(int RouteId)
        {
            return CompetitionDB.TblRoutesKKT.Find(RouteId).Time;
        }

        // Get points of route
        public int PointsRoute(int RouteId)
        {
            return CompetitionDB.TblRoutesKKT.Find(RouteId).Points;
        }

        public int pointSumFromPenalties(int id)
        {
            int penaltyId = CompetitionDB.TblPenaltyQuantities.Find(id).PenaltyId;
            return CompetitionDB.TblPenaltyQuantities.Find(id).Quantity * CompetitionDB.TblPenalties.Find(penaltyId).Points;
        }

        public int PointsFromPenalties(int id)
        {
            int points = 0;
            List<PenaltyQuantityModel> penaltyQ = AllPenaltyQuantities(id);
            if (penaltyQ.Count != 0)
            {
                foreach (var p in penaltyQ)
                {
                    if (p != null)
                    {
                        points += pointSumFromPenalties(p.Id);
                    }
                }
            }

            return points;
        }
    }
}
