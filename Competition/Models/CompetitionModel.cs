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

        public CompetitionModel(TblCompetition row)
        {
            Id = row.Id;
            Name = row.Name;
            Date = row.Date;
            OrgId = row.OrgId;
            MainJudgeId = row.MainJugdeId;
            MainRouteCreatorId = row.MainRouteCreatorId;
            Type = row.Type;
            Open = row.Open;
        }

    }
}