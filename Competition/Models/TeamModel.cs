using Competition.Context;
using System.Collections.Generic;

namespace Competition.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public TblUser Captain { get; set; }
        public List<UserModel> Teammates { get; set; }

        public TeamModel(TblTeam row)
        {
            Id = row.Id;
            Name = row.Name;
            CaptainId = row.TeamCaptainId;
        }


    }
}