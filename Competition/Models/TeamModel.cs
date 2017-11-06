using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TeamModel(TblTeam row)
        {
            Id = row.Id;
            Name = row.Name;
        }
    }
}