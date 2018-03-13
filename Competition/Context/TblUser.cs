using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblUser")]
    public class TblUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string TelNumber { get; set; }
        public string Email { get; set; }
        public int ClubId { get; set; }
    }
}