using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class AppContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Department> Departments{ get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TaskProject> TasksProject { get; set; }
    public DbSet<TaskApproval> TasksApprovals { get; set; }
    public DbSet<TaskAssignmentLog> TaskAssignmentLogs { get; set; }
    public DbSet<TaskSkill> TaskSkills { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlite("Data Source = EmployeeAllocation.db");  //initial 
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/EmployeeAllocation.db");   //CHANGE ONCE Initially created+migrated
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    //define primary keys, (and maybe constraints if not in logic)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Username);
        modelBuilder.Entity<User>().Property(user => user.Username).HasMaxLength(4);  
        modelBuilder.Entity<UserSkill>().HasKey(userskill => userskill.UserSkillId);
        modelBuilder.Entity<Skill>().HasKey(skill => skill.Name);
        modelBuilder.Entity<Department>().HasKey(dpt => dpt.Name);
        modelBuilder.Entity<Project>().HasKey(pj => pj.ProjectId);
        modelBuilder.Entity<Tag>().HasKey(t => t.Name);
        modelBuilder.Entity<TaskProject>().HasKey(tp => tp.Id);
        modelBuilder.Entity<TaskApproval>().HasKey(ta => ta.Id);
        modelBuilder.Entity<TaskAssignmentLog>().HasKey(tal => tal.Id);
        modelBuilder.Entity<TaskSkill>().HasKey(ts => ts.Id);
        modelBuilder.Entity<Holiday>().HasKey(a => a.Id);
        
        // modelBuilder.Entity<Company>().HasKey(company => company.Id);
        // modelBuilder.Entity<SubCategory>().HasKey(subCategory => subCategory.Id);
        // modelBuilder.Entity<Category>().HasKey(Category => Category.Id);
        // // modelBuilder.Entity<DayContent>().HasIndex(dc => dc.Date).IsUnique();
        // modelBuilder.Entity<DayContentProduct>().HasKey(DayContentProduct => new
        //     { DayContentProduct.DayContentId, DayContentProduct.ProductId });
    }
}