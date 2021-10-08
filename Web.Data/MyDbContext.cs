using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models;
using Web.Data.Models.IdentityUser;

namespace Web.Data
{
    public class MyDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Compaign> Compaigns { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompaignGroupMapping> CompaignGroupMappings { get; set; }
        public DbSet<CompaignUserMapping> CompaignUserMappings { get; set; }
        public DbSet<DealUserMapping> DealUserMappings { get; set; }
        public DbSet<GroupUserMapping> GroupUserMappings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TransferJoy> TransferJoys { get; set; }
        public DbSet<TransferScore> TransferScores { get; set; }
        public DbSet<SharedDealTracking> SharedDealTrackings { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GiftUserMapping> GiftUserMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
            OnIdentityCreating(builder);
        }
        private static void OnIdentityCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasMany(e => e.Claims)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
                user.HasMany(e => e.Tokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
                user.HasMany(e => e.Roles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
                user.HasMany(x => x.CompaignUserMappings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
                user.HasMany(x => x.DealUserMappings)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
                user.HasMany(x => x.GroupMappings)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .IsRequired();
            });
            modelBuilder.Entity<Role>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}
