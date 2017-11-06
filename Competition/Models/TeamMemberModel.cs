using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class TeamMemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public int TeamId { get; set; }

        public TeamMemberModel(TblTeamMember row)
        {
            Id = row.Id;
            Name = row.Name;
            LastName = row.LastName;
            BirthYear = row.BirthYear;
            TeamId = row.TeamId;
        }
    }
}