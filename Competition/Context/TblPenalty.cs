using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Competition.Context
{
    [Table("TblPenalty")]
    public class TblPenalty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Yra { get;  set; }
    }
}