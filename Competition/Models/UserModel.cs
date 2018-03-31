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

        public List<RoleModel> roles { get; set; }

        public UserModel(TblUser row)
        {
            Id = row.Id;
            Name = row.Name;
            LastName = row.LastName;
            Active = row.Active;
            ClubId = row.ClubId;

        }
    }
}