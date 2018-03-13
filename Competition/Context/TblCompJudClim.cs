using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblCompJudClimb")]
    public class TblCompJudClim
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }
    }
}