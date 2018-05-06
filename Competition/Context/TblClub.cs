using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblClub")]
    public class TblClub
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}