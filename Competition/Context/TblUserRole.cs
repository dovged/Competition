using System.ComponentModel.DataAnnotations.Schema;

namespace Competition.Context
{
    [Table("TblUserRole")]
    public class TblUserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

    }
}