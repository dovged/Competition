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
        public DbSet<TblJudgesPaper> TblJudgesPapers { get; set; }
        public DbSet<TblPenalty> TblPenalties { get; set; }
        public DbSet<TblPenaltyQuantity> TblPenaltyQuantities { get; set; }
        public DbSet<TblRoute> TblRoutes { get; set; }
        public DbSet<TblTeam> TblTeams { get; set; }
        public DbSet<TblTeamMember> TblTeamMembers { get; set; }
        public DbSet<TblCompJud> TblCompJuds { get; set; }
    }
}