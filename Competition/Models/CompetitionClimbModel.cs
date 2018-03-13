using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class CompetitionClimbModel : CompetitionModel
    {
        List<UserModel> Climbers { get; set; }
        List<RouteClimbModel> Routes { get; set; }

        public CompetitionClimbModel(TblCompetition row)
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