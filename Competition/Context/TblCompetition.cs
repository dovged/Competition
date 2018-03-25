using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Competition.Context
{
    [Table("TblCompetition")]
    public class TblCompetition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int OrgId { get; set; }
        public int MainRouteCreatorId { get; set; }
        public int MainJudgeId { get; set; }
        //Nustatyti ar laipiojimo varžybos (true), ar KKT varžybos (false);
        public Boolean Type { get; set; }
    }
}