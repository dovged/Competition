using Competition.Context;
using System;

namespace Competition.Models
{
    public class PenaltyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public Boolean Active { get; set; }

        public PenaltyModel(TblPenalty row)
        {
            Id = row.Id;
            Name = row.Name;
            Points = row.Points;
            Active = row.Yra;
        }
    }
}