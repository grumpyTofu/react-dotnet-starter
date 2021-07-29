using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Role { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Todo> Todo { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.Permissions).IsRequired();

            entity.HasData(
               new Role { Id = 1, Name = "User", Permissions = Permissions.General }
           );
        });

        builder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            entity.HasAlternateKey(u => u.Email);
            entity.HasMany(u => u.Todos).WithOne(t => t.User);
            entity.HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);

            entity.HasData(
                new User { Id = 1, Email = "austin.felix@roche.com", FirstName = "Austin", LastName = "Felix", RoleId = 1 }
            );
        });

        builder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todos");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(t => t.Title).IsRequired().HasMaxLength(80);
            entity.Property(t => t.Description).IsRequired();
            entity.HasOne(t => t.User).WithMany(u => u.Todos).HasForeignKey(t => t.UserId);

            entity.HasData(
                new Todo { Id = 1, Title = "Test", Description = "Test Description", UserId = null }
            );
        });
    }
}
