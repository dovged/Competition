using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class CompetitionKKTModel : CompetitionModel
    {
        List<TeamModel> Teams { get; set; }

        public CompetitionKKTModel(TblCompetition row)
        {
            Id = row.Id;
            Name = row.Name;
            Date = row.Date;
            OrgId = row.OrgId;
            MainJudgeId = row.MainJudgeId;
            MainRouteCreatorId = row.MainRouteCreatorId;
            Type = row.Type;
        }

    }
}