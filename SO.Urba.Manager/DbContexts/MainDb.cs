
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SO.Urba.Models.ValueObjects;
using SO.Utility.Base;

namespace SO.Urba.DbContexts
{
    public partial class MainDb : BaseDbContext
    {
        public MainDb()
            : base("name=UrbaDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
        }
    
        public DbSet<CompanyCategoryTypeVo> companyCategoryTypes { get; set; }
        public DbSet<MemberRoleTypeVo> memberRoleTypes { get; set; }
        public DbSet<QuestionTypeVo> questionTypes { get; set; }
        public DbSet<CompanyVo> companies { get; set; }
        public DbSet<CompanyCategoryLookupVo> companyCategoryLookups { get; set; }
        public DbSet<OrganizationVo> organizations { get; set; }
        public DbSet<ContactInfoVo> contactInfos { get; set; }
        public DbSet<ClientVo> clients { get; set; }
        public DbSet<MemberVo> members { get; set; }
        public DbSet<MemberRoleLookupVo> memberRoleLookups { get; set; }
        public DbSet<ReferralVo> referrals { get; set; }
        public DbSet<SurveyAnswerVo> surveyAnswers { get; set; }
        public DbSet<ClientOrganizationLookupVo> clientOrganizationLookups { get; set; }
    }
}
