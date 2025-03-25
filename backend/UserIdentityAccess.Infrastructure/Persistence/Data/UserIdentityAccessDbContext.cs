using Microsoft.EntityFrameworkCore;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Infrastructure.Persistence.Data;
public class UserIdentityAccessDbContext(DbContextOptions<UserIdentityAccessDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<GroupPermission> GroupPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).UseIdentityColumn();
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(u => u.Email).IsUnique();
        });
        
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(u => u.Id).UseIdentityColumn();
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Name).IsUnique();
        });
        
        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(ug => new { ug.UserId, ug.GroupId }); 
            entity.HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);
            entity.HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(u => u.Id).UseIdentityColumn();
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Name).IsUnique();
        });
        
        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasKey(ug => new { ug.GroupId, ug.PermissionId }); 
            entity.HasOne(ug => ug.Group)
                .WithMany(u => u.GroupPermissions)
                .HasForeignKey(ug => ug.GroupId);
            entity.HasOne(ug => ug.Permission)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(ug => ug.PermissionId);
        });
        
        modelBuilder.Entity<Permission>().HasData(
            new Permission(1,"Level 1"),
            new Permission(2,"Level 2"),
            new Permission(3,"Admin")
        );

        Group group1 = new Group("Red Team Developers");
        modelBuilder.Entity<Group>().HasData(
            new Group(1,"Red Team Developers"),
            new Group(2,"Admins"),
            new Group(3,"Blue Team Developers"),
            new Group(4,"Viewers")
        );

        modelBuilder.Entity<User>().HasData(
            new User(1,"Bob","Steve","bobsteve@example.org"),
            new User(2,"Sandy","Stevin","sandysteven@example.org"),
            new User(3,"Johny","Robers","johnyrobers@example.org")
        );
        
        modelBuilder.Entity<GroupPermission>().HasData(
            new GroupPermission(1,3),
            new GroupPermission(2,2),
            new GroupPermission(3,1)
        );
        
        modelBuilder.Entity<UserGroup>().HasData(
            new UserGroup(1,1),
            new UserGroup(1,2),
            new UserGroup(2,3),
            new UserGroup(3,4)
        );
    }
}