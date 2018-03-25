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
      /*  public HttpResponseMessage Get(int id)
        {
            if(CompetitionDB.TblCompetitions.FirstOrDefault(x => x.Id == id) != null)
            {
                if (CompetitionDB.TblCompTeams.ToArray().Where(x => x.CompetitionId == id).Select(x=> new CompetitorsKKTModel(x)).ToList().Count != 0)
                {

                    List<CompetitorsKKTModel> compTeam = new List<CompetitorsKKTModel>();
                    List<ResultModel> results = new List<ResultModel>();
                    List<RouteKKTModel> routes = new List<RouteKKTModel>();

                    compTeam = CompetitionDB.TblCompTeams.ToArray().Where(x => x.CompetitionId == id).Select(x => new CompetitorsKKTModel(x)).ToList();
                    routes = CompetitionDB.TblRoutes.ToArray().Where(x => x.CompetitionId == id).Select(x => new RouteKKTModel(x)).ToList();

                    foreach (var ct in compTeam)
                    {
                        ResultModel result = new ResultModel();
                        int teamId = ct.TeamId;
                        result.SetTeamName(CompetitionDB.TblTeams.Find(teamId).Name.ToString());
                        List<JudgesPaperKKTModel> judgesPapers = new List<JudgesPaperKKTModel>();
                        int sumTime = 0;
                        foreach (var r in routes)
                        {
                            JudgesPaperKKTModel jp = CompetitionDB.TblJudgesPapers.ToArray().Where(x => x.RouteId == r.Id && x.TeamId == teamId).Select(x => new JudgesPaperKKTModel(x)).FirstOrDefault();
                            if (jp != null)
                            {
                                jp.SetRouteName(r.Name);
                                if (TimeCompare(MaxTimeRoute(r.Id), jp.Time))
                                {
                                    jp.SetPoints(r.Points);
                                    int penaltySum = PointsFromPenalties(jp.Id);
                                    jp.SetPenalySum(penaltySum);
                                    int routeTimeFromJP = TimeToSec(jp.Time);
                                    sumTime += (penaltySum * 30) + routeTimeFromJP;
                                    if (penaltySum == 0)
                                    {
                                        jp.SetTimeWithPenalty(jp.Time);
                                    }
                                    else
                                    {
                                        jp.SetTimeWithPenalty(SecToTime((penaltySum * 30) + routeTimeFromJP));
                                    }

                                    result.PlusPoints(r.Points);
                                }
                                else
                                {
                                    jp.SetTime("00:00:00");
                                    jp.SetTimeWithPenalty("00:00:00");
                                }

                                judgesPapers.Add(jp);
                            }
                            else
                            {
                                JudgesPaperKKTModel newJp = new JudgesPaperKKTModel();
                                newJp.SetRouteName(r.Name);
                                judgesPapers.Add(newJp);
                            }
                        }
                        result.SetJudgesPapers(judgesPapers);
                        result.SetTime(SecToTime(sumTime));
                        results.Add(result);


                    }
                    List<ResultModel> resultsOrder = results.OrderByDescending(x => x.PointsSum).ThenBy(x => x.TimeSum).ToList();
                    int i = 1;
                    foreach(ResultModel res in resultsOrder){
                        res.Place = i;
                        i++;
                    }
                    
                    return ToJson(resultsOrder.AsEnumerable());
                }
            }

            List<RouteKKTModel> list = new List<RouteKKTModel>();
            return ToJson(list);

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
      /*  public bool TimeCompare(string time1, string time2)
        {

            return TimeToSec(time1) >= TimeToSec(time2);

        }

        // convert time(string) to seconds
        public int TimeToSec(string time)
        {
            //time format (hh:mm:ss)
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
            return CompetitionDB.TblRoutes.Find(RouteId).MaxTime.ToString();
        }

        // Get points of route
        public int PointsRoute(int RouteId)
        {
            return Convert.ToInt32(CompetitionDB.TblRoutes.Find(RouteId).Points.ToString());
        }

        public int pointSumFromPenalties(int id)
        {

            int penaltyId = Convert.ToInt32(CompetitionDB.TblPenaltyQuantities.Find(id).PenaltyId.ToString());

            return Convert.ToInt32(CompetitionDB.TblPenaltyQuantities.Find(id).Quantity.ToString()) *
                Convert.ToInt32(CompetitionDB.TblPenalties.Find(penaltyId).Points.ToString());
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
        }*/
    }
}
