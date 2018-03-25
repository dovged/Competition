using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblPenalty")]
    public class TblPenalty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public bool Yra { get;  set; }
    }
}