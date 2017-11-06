using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class CompTeamModel
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int TeamId { get; set; }

        public CompTeamModel(TblCompTeam row)
        {
            Id = row.Id;
            CompId = row.CompetitionId;
            TeamId = row.TeamId;

        }
    }
}