using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NTAP.WebUI.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext() : base("NTAP") { }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("App");
            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserID");
            modelBuilder.Entity<Role>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleID");
            modelBuilder.Entity<UserRole>().ToTable("UserRole").Property(p => p.UserId).HasColumnName("UserID");
            modelBuilder.Entity<UserRole>().ToTable("UserRole").Property(p => p.RoleId).HasColumnName("RoleID");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimID");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim").Property(p => p.UserId).HasColumnName("UserID");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin").Property(p => p.UserId).HasColumnName("UserID");
        }
    }

    public class Role : IdentityRole<int, UserRole>
    {
        [Required(AllowEmptyStrings = true), StringLength(255)]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }

        public Role() { }
        public Role(string name)
        {
            Name = name;
        }
    }
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        [StringLength(maximumLength: 50)]
        [Required]
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class UserRole : IdentityUserRole<int> { }
    public class UserClaim : IdentityUserClaim<int> { }
    public class UserLogin : IdentityUserLogin<int> { }
    public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context) : base(context) { }
    }
    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(ApplicationDbContext context) : base(context) { }
    }
}