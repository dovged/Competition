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
        public string TelNumber { get; set; }
        public string Email { get; set; }
        public int ClubId { get; set; }
        public string Club { get; set; }
        public string Team { get; set; }
        public Boolean Active { get; set; }
        public int TrainerId { get; set; }
        public DateTime BirthYear { get; set; }
        public string Lytis { get; set; }
        public string Name2 { get; set; }
        public string BirthYear2 { get; set; }

        public List<RoleModel> Roles { get; set; }

        public UserModel(TblUser row)
        {
            Id = row.Id;
            Name = row.Name;
            LastName = row.LastName;
            TelNumber = row.TelNumber;
            Email = row.Email;
            Active = row.Active;
            ClubId = row.ClubId;
            TrainerId = row.TrainerId;
            BirthYear = row.BirthYear;
            Lytis = row.Lytis;
            Name2 = Name + " " + LastName;

            if (BirthYear.ToString().Length == 22)
            {
                BirthYear2 = BirthYear.ToString().Substring(0, 10);
            }
            else if (BirthYear.ToString().Length == 20)
            {
                BirthYear2 = BirthYear.ToString().Substring(0, 8);
            }
            else
            {
                BirthYear2 = BirthYear.ToString().Substring(0, 9);
            }

            String[] datearray = BirthYear2.Split('/');
            BirthYear2 = datearray[2] + "-";
            if (datearray[0].Length == 1)
            {
                BirthYear2 += "0" + datearray[0] + "-";
            }
            else
            {
                BirthYear2 += datearray[0] + "-";
            }

            if (datearray[1].Length == 1)
            {
                BirthYear2 += "0" + datearray[1];
            }
            else
            {
                BirthYear2 += datearray[1];
            }

        }
    }
}