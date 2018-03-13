using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblRole")]
    public class TblRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}