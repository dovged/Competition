using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblPenalties")]
    public class TblPaperType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public bool Active { get; set; }
    }
}