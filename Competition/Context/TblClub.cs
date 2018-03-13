using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblClub")]
    public class TblClub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adrress { get; set; }
        public string City{ get; set; }
        public string Country { get; set; }
    }
}