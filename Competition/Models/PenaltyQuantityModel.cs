﻿using Competition.Context;

namespace Competition.Models
{
    public class PenaltyQuantityModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int PenaltyId { get; set; }
        public string PenaltyName { get; set; }

        public PenaltyQuantityModel(TblPenaltyQuantity row)
        {
            Id = row.Id;
            Quantity = row.Quantity;
            PenaltyId = row.PenaltyId;
        }
    }
}