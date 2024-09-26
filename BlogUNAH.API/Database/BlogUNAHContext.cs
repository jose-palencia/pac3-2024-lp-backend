using BlogUNAH.API.Database.Entities;
using BlogUNAH.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogUNAH.API.Database
{
    public class BlogUNAHContext : IdentityDbContext<IdentityUser> 
    {
        private readonly IAuthService _authService;

        public BlogUNAHContext(
            DbContextOptions options, 
            IAuthService authService
            ) : base(options)
        {
            this._authService = authService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<TagEntity>()
            .Property(e => e.Name)
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.HasDefaultSchema("security");

            modelBuilder.Entity<IdentityUser>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");


        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                ));

            foreach (var entry in entries) 
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null) 
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _authService.GetUserId();
                        entity.CreatedDate = DateTime.Now;
                    }
                    else 
                    {
                        entity.UpdatedBy = _authService.GetUserId();
                        entity.UpdatedDate = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<PostTagEntity> PostsTags { get; set; }
    }
}
