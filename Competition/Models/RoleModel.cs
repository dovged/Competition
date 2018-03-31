using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public RoleModel(TblUserRole row)
        {
            Id = row.Id;
            RoleId = row.RoleId;
        }
    }
}