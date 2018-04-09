using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int ClubId { get; set; }
        public string Club { get; set; }
        public string Team { get; set; }
        public Boolean Active { get; set; }
        public int TrainerId { get; set; }
        public DateTime BirthYear { get; set; }
        public DateTime BirthYear1 { get; set; }
        public string Lytis { get; set; }
        public string Name2 { get; set; }

        public List<RoleModel> roles { get; set; }

        public UserModel(TblUser row)
        {
            Id = row.Id;
            Name = row.Name;
            LastName = row.LastName;
            Active = row.Active;
            ClubId = row.ClubId;
            TrainerId = row.TrainerId;
            BirthYear = row.BirthYear;
            Lytis = row.Lytis;
            BirthYear1 = BirthYear.Date;
            Name2 = Name + " " + LastName;
        }
    }
}