using Competition.Context;
using System;

namespace Competition.Models
{
    public class CompetitorsKKTModel
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int TeamId { get; set; }
        public string Group { get; set; }
        public bool Paid { get; set; }
        public string TeamName { get; set; }
        public string CompetitionName { get; set; }
        public string Club { get; set; }
        public DateTime Date { get; set; }
        public string Date2 { get; set; }
        public Boolean Update { get; set; }

        public CompetitorsKKTModel(TblCompetitorsKKT row)
        {
            Id = row.Id;
            CompId = row.CompetitionId;
            TeamId = row.TeamId;
            Group = row.Group;
            Paid = row.Paid;
        }

        public void setDate2(DateTime Date)
        {
            if (Date.ToString().Length == 22)
            {
                Date2 = Date.ToString().Substring(0, 10);
            }
            else if (Date.ToString().Length == 20)
            {
                Date2 = Date.ToString().Substring(0, 8);
            }
            else
            {
                Date2 = Date.ToString().Substring(0, 9);
            }

            String[] datearray = Date2.Split('/');
            Date2 = datearray[2] + "-";
            if (datearray[0].Length == 1)
            {
                Date2 += "0" + datearray[0] + "-";
            }
            else
            {
                Date2 += datearray[0] + "-";
            }

            if (datearray[1].Length == 1)
            {
                Date2 += "0" + datearray[1];
            }
            else
            {
                Date2 += datearray[1];
            }
        }
     }
}