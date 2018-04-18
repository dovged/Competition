using Competition.Context;
using System;
using System.Collections.Generic;

namespace Competition.Models
{
    public class CompetitionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public int MainRouteCreatorId { get; set; }
        public string MainRouteCreatorName { get; set; }
        public int MainJudgeId { get; set; }
        public string MainJudgeName { get; set; }
        public string Club { get; set; }
        //Nustatyti ar laipiojimo varžybos (true), ar KKT varžybos (false);
        public Boolean Type { get; set; }
        public Boolean Open { get; set; }
        public Boolean Update { get; set; }
        public int ClimbType { get; set; }
        public string Date2 { get; set; }

        public CompetitionModel(TblCompetition row)
        {
            Id = row.Id;
            Name = row.Name;
            Date = row.Date;
            OrgId = row.OrgId;
            MainJudgeId = row.MainJudgeId;
            MainRouteCreatorId = row.MainRouteCreatorId;
            Type = row.Type;
            Open = row.Open;
            Update = row.Update;
            ClimbType = row.ClimbType;
            if (Date.ToString().Length == 22)
            {
                Date2 = Date.ToString().Substring(0, 10);
            }
            else if (Date.ToString().Length == 20)
            {
                Date2 = Date.ToString().Substring(0, 8);
            }
            else
            {
                Date2 = Date.ToString().Substring(0, 9);
            }

            String[] datearray = Date2.Split('/');
            Date2 = datearray[2] + "-";
            if(datearray[0].Length == 1)
            {
                Date2 += "0" + datearray[0] + "-";
            }
            else
            {
                Date2 += datearray[0] + "-";
            }

            if (datearray[1].Length == 1)
            {
                Date2 += "0" + datearray[1];
            }
            else
            {
                Date2 += datearray[1];
            }
        }

    }
}