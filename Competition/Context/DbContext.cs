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
        public DbSet<TblCompTeam> TblCompTeams { get; set; }
        public DbSet<TblJudgesPaperKKT> TblJudgesPapers { get; set; }
        public DbSet<TblPenalty> TblPenalties { get; set; }
        public DbSet<TblPenaltyQuantity> TblPenaltyQuantities { get; set; }
        public DbSet<TblRouteKKT> TblRoutes { get; set; }
        public DbSet<TblTeam> TblTeams { get; set; }
        public DbSet<TblTeamMember> TblTeamMembers { get; set; }
        public DbSet<TblCompJudKKT> TblCompJuds { get; set; }
        public DbSet<TblPaperType> TblPaperTypes { get; set; }
        public DbSet<TblCompType> TblCompTypes { get; set; }
        public DbSet<TblClub> TblClubs { get; set; }
    }
}