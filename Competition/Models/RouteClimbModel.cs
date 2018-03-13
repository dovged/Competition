using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class RouteClimbModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int PointsBonus { get; set; }
        public int PointsTop { get; set; }
        public int PointsFlash { get; set; }
        public int CompId { get; set; }

        public RouteClimbModel(TblRouteClimb row)
        {
            Id = row.Id;
            PointsBonus = row.PointsBonus;
            PointsTop = row.PointsTop;
            PointsFlash = row.PointsFlash;
            CompId = row.CompetitionId;
        }
    }
}