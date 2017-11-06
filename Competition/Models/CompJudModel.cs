using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class CompJudModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompId { get; set; }

        public CompJudModel(TblCompJud row)
        {
            Id = row.Id;
            UserId = row.UserId;
            CompId = row.CompId;
        }
    }
}