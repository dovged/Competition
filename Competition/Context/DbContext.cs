using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Competition.Context
{
    public class DbContext : IdentityDbContext<IdentityUser>
    {
        public DbContext()
            : base("DbContext")
        {

        }

        public DbSet<TblUser> TblUsers { get; set; }
        public DbSet<TblCompetition> TblCompetitions { get; set; }
        public DbSet<TblCompetitorsKKT> TblCompetitorsKKT { get; set; }
        public DbSet<TblCompetitorsClimb> TblCompetitorsClim { get; set; }
        public DbSet<TblJudgesPaperKKT> TblJudgesPapersKKT { get; set; }
        public DbSet<TblJudgesPaperClim> TblJudgesPapersClimb { get; set; }
        public DbSet<TblPenalty> TblPenalties { get; set; }
        public DbSet<TblPenaltyQuantity> TblPenaltyQuantities { get; set; }
        public DbSet<TblRouteKKT> TblRoutesKKT { get; set; }
        public DbSet<TblRouteClimb> TblRoutesClim { get; set; }
        public DbSet<TblTeam> TblTeams { get; set; }
        public DbSet<TblCompJudge> TblCompJudges { get; set; }
        public DbSet<TblPaperType> TblPaperTypes { get; set; }
        public DbSet<TblClub> TblClubs { get; set; }
        public DbSet<TblRole> TblRoles { get; set; }
        public DbSet<TblUserRole> TblUserRoles { get; set; }
    }
}