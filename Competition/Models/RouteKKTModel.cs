using Competition.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competition.Models
{
    public class RouteKKTModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public string Time { get; set; }
        public int CompId { get; set; }
        public string Type { get; set; }

        public RouteKKTModel(TblRouteKKT row)
        {
            Id = row.Id;
            Name = row.Name;
            Points = row.Points;
            Time = row.Time;
            CompId = row.CompetitionId;
            Type = row.Type;
        }
    }
}