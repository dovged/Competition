using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    [Table("TblPenaltyQuantity")]
    public class TblPenaltyQuantity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int PenaltyId { get; set; }
        public int JudgesPaperId { get; set; }
    }
}