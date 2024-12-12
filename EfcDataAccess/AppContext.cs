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
    
    
    // Default constructor for production setup (using SQLite)
    public AppContext() { }
    // Default constructor (for production use) -Testing
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            // optionsBuilder.UseSqlite("Data Source = EmployeeAllocation.db");  //initial 
            optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/EmployeeAllocation.db"); //CHANGE ONCE Initially created+migrated
              optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
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
        
        
        //SKILL
        modelBuilder.Entity<Skill>().HasData(new List<Skill>
        {
            new Skill("C#", "Programming"),
            new Skill("Python", "Programming"),
            new Skill("JavaScript", "Programming"),
            new Skill("Cloud Management", "Cloud Computing"),
            new Skill("AWS", "Cloud Computing"),
            new Skill("Docker", "DevOps"),
            new Skill("Kubernetes", "DevOps"),
            new Skill("Data Analysis", "Data Science"),
            new Skill("Machine Learning", "Data Science"),
            new Skill("Database Optimization", "Data Science"),
            new Skill("Figma", "UI/UX Design"),
            new Skill("HTML/CSS", "UI/UX Design"),
            new Skill("Manual Testing", "Quality Assurance"),
            new Skill("Effective Communication", "Soft Skill"),
            new Skill("Team Collaboration", "Soft Skill"),
            new Skill("Critical Thinking", "Soft Skill"),
            new Skill("Project Management", "Management"),
            new Skill("Cost Management", "Management"),
            new Skill("Agile Practices", "Management"),
            new Skill("Cloud Security", "Cloud Computing"),
            new Skill("Cloud Monitoring", "Cloud Computing"),
            new Skill("Network Security", "Cloud Computing"),
            new Skill("Firewall Configuration", "Cloud Computing"),
            new Skill("Cloud Infrastructure", "Cloud Computing"),
            new Skill("Security Policies", "Security"),
            new Skill("Risk Assessment", "Management"),
            new Skill("Cloud Optimization", "Cloud Computing"),
            new Skill("Infrastructure Scaling", "Cloud Computing"),
            new Skill("Cloud Platform Knowledge", "Cloud Computing"),
            new Skill("Cloud Cost Analysis", "Cloud Computing"),
            new Skill("Cloud Performance", "Cloud Computing"),
            new Skill("Service Management", "Cloud Computing"),
            new Skill("Containerization", "DevOps"),
            new Skill("Cloud Deployment", "Cloud Computing"),
            new Skill("Container Security", "DevOps"),
            new Skill("Load Balancing", "DevOps"),
            new Skill("Kubernetes Networking", "DevOps"),
            new Skill("Kubernetes Security", "Security")
        });

        
        //DEPARTMENT
        modelBuilder.Entity<Department>().HasData(new List<Department>
{
    // Development & Engineering
    new Department { Name = "Frontend Development" },          // Specializes in UI/UX and web interface development
    new Department { Name = "Backend Development" },           // Focused on server-side and database development
    new Department { Name = "Mobile Development" },            // For iOS and Android app development
    new Department { Name = "Embedded Systems Engineering" },  // Specializes in firmware and IoT systems

    // IT & Operations
    new Department { Name = "Cloud Infrastructure" },          // Manages AWS, Azure, GCP
    new Department { Name = "Network Engineering" },           // Focused on enterprise network solutions
    new Department { Name = "IT Security" },                   // Specializes in cybersecurity and compliance
    new Department { Name = "DevOps and Automation" },         // Builds CI/CD pipelines and deployment automation
    new Department { Name = "Database Administration" },       // Manages SQL, NoSQL databases

    // Business & Support
    new Department { Name = "Customer Success" },              // Ensures customer satisfaction and retention
    new Department { Name = "Sales Engineering" },             // Bridges technical solutions with client needs
    new Department { Name = "HR - Talent Acquisition" },       // Specialized in hiring and onboarding
    new Department { Name = "HR - Employee Relations" },       // Focuses on employee welfare and engagement
    new Department { Name = "Legal Compliance" },              // Handles data privacy and legal obligations

    // Quality & Testing
    new Department { Name = "Test Automation Engineering" },   // Automates quality assurance testing
    new Department { Name = "Manual Testing" },                // Conducts user-driven exploratory testing
    new Department { Name = "Performance Testing" },           // Ensures scalability and performance benchmarks
    
});

        //TAGS
        modelBuilder.Entity<Tag>().HasData(new List<Tag>
{
    // Security Tags
    new Tag("Cloud Security", "Security"),
    new Tag("Network Security", "Security"),
    new Tag("Authentication Mechanisms", "Security"),
    new Tag("Penetration Testing", "Security"),
    new Tag("Incident Response", "Security"),
    new Tag("Data Security", "Security"),

    // Cloud Computing Tags
    new Tag("Cloud Management", "Cloud Computing"),
    new Tag("Serverless Architectures", "Cloud Computing"),
    new Tag("Multi-Cloud Strategies", "Cloud Computing"),
    new Tag("Cloud Migration", "Cloud Computing"),
    new Tag("Disaster Recovery", "Cloud Computing"),
    new Tag("Cloud Orchestration", "Cloud Computing"),
    new Tag("Cloud Networking", "Cloud Computing"),
    

    // DevOps Tags
    new Tag("CI/CD Pipelines", "DevOps"),
    new Tag("Infrastructure as Code", "DevOps"),
    new Tag("Container Orchestration", "DevOps"),
    new Tag("Monitoring and Logging", "DevOps"),
    new Tag("Configuration Management", "DevOps"),
    new Tag("DevOps", "DevOps"),
    
    // AI/ML Tags
    new Tag("AI Model Deployment", "AI/ML"),
    new Tag("Natural Language Processing", "AI/ML"),
    new Tag("Image Recognition", "AI/ML"),
    new Tag("Machine Learning Pipelines", "AI/ML"),
    new Tag("Reinforcement Learning", "AI/ML"),

    // Database Tags
    new Tag("Database Optimization", "Databases"),
    new Tag("Data Warehousing", "Databases"),
    new Tag("Database Migration", "Databases"),
    new Tag("SQL Performance Tuning", "Databases"),
    new Tag("NoSQL Design Patterns", "Databases"),
    new Tag("Data Management", "Databases"),

    // Containerization Tags
    new Tag("Dockerization", "Containerization"),
    new Tag("Kubernetes Configuration", "Containerization"),
    new Tag("Container Security", "Containerization"),
    new Tag("Service Mesh Implementation", "Containerization"),
    new Tag("Scalability Testing", "Containerization"),

    // Infrastructure Tags
    new Tag("High Availability Systems", "Infrastructure"),
    new Tag("Load Balancing", "Infrastructure"),
    new Tag("Edge Computing", "Infrastructure"),
    new Tag("Hybrid Cloud Deployment", "Infrastructure"),
    new Tag("Network Load Optimization", "Infrastructure"),

    // Miscellaneous Tags
    new Tag("Agile Practices", "Project Management"),
    new Tag("Scrum Methodology", "Project Management"),
    new Tag("Test Automation Frameworks", "Quality Assurance"),
    new Tag("API Testing Tools", "Quality Assurance"),
    new Tag("User Experience Optimization", "UI/UX Design")
});
    
    
    //USERS
    modelBuilder.Entity<User>().HasData(new List<User>
{
    // Project Manager
    new User(
        username: "dojo",
        password: "password",
        name: "John Doe",
        role: "Project Manager",
        department: null,
        email: "johndoe@example.com",
        projectHoursPerDay: 0,
        userSkills: null,
        holidays: null,
        tasks: null),

    // Manager
    new User(
        username: "doja",
        password: "password",
        name: "Jane Doe",
        role: "Manager",
        department: null,
        email: "janedoe@example.com",
        projectHoursPerDay: 2,
        userSkills: null,
        holidays: null,
        tasks: null),

    // Employees (in the same department)
    new User(
        username: "rosj",
        password: "password",
        name: "Jack Rose",
        role: "Employee",
        department: null,
        email: "jackrose@example.com",
        projectHoursPerDay: 2,
        userSkills: null,
        holidays: null,
        tasks: null),
    new User(
        username: "tayl",
        password: "password",
        name: "Lily Taylor",
        role: "Employee",
        department: null,
        email: "lilytaylor@example.com",
        projectHoursPerDay: 2,
        userSkills: null,
        holidays: null,
        tasks: null),
    new User(
        username: "howm",
        password: "password",
        name: "Mark Howard",
        role: "Employee",
        department: null,
        email: "markhoward@example.com",
        projectHoursPerDay: 2,
        userSkills: null,
        holidays: null,
        tasks: null),
    new User(
        username: "patj",
        password: "password",
        name: "John Patel",
        role: "Employee",
        department: null,
        email: "johnpatel@example.com",
        projectHoursPerDay: 2,
        userSkills: null,
        holidays: null,
        tasks: null),
    new User(
        username: "vanj",
        password: "password",
        name: "Jane Van",
        role: "Employee",
        department: null,
        email: "janevan@example.com",
        projectHoursPerDay: 2,
        userSkills: null,
        holidays: null,
        tasks: null)
});

    
// UserSkills
modelBuilder.Entity<UserSkill>().HasData(new List<UserSkill>
{
    // Project Manager - John Doe (dojo)
    new UserSkill("dojo", "C#", Proficiency.Advanced, "Extensive experience in software development") { UserSkillId = 1 },
    new UserSkill("dojo", "JavaScript", Proficiency.Intermediate, null) { UserSkillId = 2 },
    new UserSkill("dojo", "Project Management", Proficiency.Advanced, "Managed multiple large-scale projects") { UserSkillId = 3 },

    // Manager - Jane Doe (doja)
    new UserSkill("doja", "Team Collaboration", Proficiency.Advanced, "Led a team of developers") { UserSkillId = 4 },
    new UserSkill("doja", "Project Management", Proficiency.Advanced, "Managed projects with agile methodologies") { UserSkillId = 5 },
    new UserSkill("doja", "Agile Practices", Proficiency.Advanced, "Expert in Scrum and Kanban") { UserSkillId = 6 },

    // Employee - Jack Rose (rosj)
    new UserSkill("rosj", "C#", Proficiency.Intermediate, null) { UserSkillId = 7 },
    new UserSkill("rosj", "JavaScript", Proficiency.Advanced, "Works on SPAs") { UserSkillId = 8 },
    new UserSkill("rosj", "HTML/CSS", Proficiency.Advanced, "Experienced in building responsive layouts") { UserSkillId = 9 },

    // Employee - Lily Taylor (tayl)
    new UserSkill("tayl", "Figma", Proficiency.Advanced, "Specialized in UI design") { UserSkillId = 10 },
    new UserSkill("tayl", "Manual Testing", Proficiency.Intermediate, null) { UserSkillId = 11 },
    new UserSkill("tayl", "JavaScript", Proficiency.Intermediate, null) { UserSkillId = 12 },

    // Employee - Mark Howard (howm)
    new UserSkill("howm", "Docker", Proficiency.Intermediate, null) { UserSkillId = 13 },
    new UserSkill("howm", "Kubernetes", Proficiency.Intermediate, "Manages orchestration") { UserSkillId = 14 },
    new UserSkill("howm", "AWS", Proficiency.Intermediate, "AWS Certified") { UserSkillId = 15 },

    // Employee - John Patel (patj)
    new UserSkill("patj", "AWS", Proficiency.Intermediate, "AWS Certified") { UserSkillId = 16 },
    new UserSkill("patj", "Cloud Management", Proficiency.Advanced, null) { UserSkillId = 17 },
    new UserSkill("patj", "Database Optimization", Proficiency.Intermediate, "Worked with MySQL and PostgreSQL") { UserSkillId = 18 },

    // Employee - Jane Van (vanj)
    new UserSkill("vanj", "Critical Thinking", Proficiency.Advanced, "Strong analytical skills") { UserSkillId = 19 },
    new UserSkill("vanj", "Team Collaboration", Proficiency.Advanced, null) { UserSkillId = 20 },
    new UserSkill("vanj", "Figma", Proficiency.Intermediate, "UI/UX design for responsive apps") { UserSkillId = 21 }
});


    
//HOLIDAY 
modelBuilder.Entity<Holiday>().HasData(new List<Holiday>
{
    // Holidays for Project Manager - John Doe (dojo)
    new Holiday("dojo", new DateOnly(2025, 12, 25)) { Id = 1 }, // Christmas
    new Holiday("dojo", new DateOnly(2025, 01, 01)) { Id = 2 }, // New Year's Day
    new Holiday("dojo", new DateOnly(2025, 05, 15)) { Id = 3 }, // Mid-year break (1 day)

    // Holidays for Manager - Jane Doe (doja)
    new Holiday("doja", new DateOnly(2025, 12, 24)) { Id = 4 }, // Christmas Eve
    new Holiday("doja", new DateOnly(2025, 12, 31)) { Id = 5 }, // New Year's Eve
    new Holiday("doja", new DateOnly(2025, 06, 01)) { Id = 6 }, // Summer holiday (1 day)
    new Holiday("doja", new DateOnly(2025, 06, 02)) { Id = 7 }, // Summer holiday (2nd day)

    // Holidays for Employee - Jack Rose (rosj)
    new Holiday("rosj", new DateOnly(2025, 12, 25)) { Id = 8 }, // Christmas
    new Holiday("rosj", new DateOnly(2025, 12, 28)) { Id = 9 }, // Extra holiday
    new Holiday("rosj", new DateOnly(2025, 04, 10)) { Id = 10 }, // Spring holiday (1 day)
    new Holiday("rosj", new DateOnly(2025, 04, 11)) { Id = 11 }, // Spring holiday (2nd day)

    // Holidays for Employee - Lily Taylor (tayl)
    new Holiday("tayl", new DateOnly(2025, 12, 24)) { Id = 12 }, // Christmas Eve
    new Holiday("tayl", new DateOnly(2025, 12, 29)) { Id = 13 }, // Extra holiday
    new Holiday("tayl", new DateOnly(2025, 05, 01)) { Id = 14 }, // May Day (1 day)
    new Holiday("tayl", new DateOnly(2025, 05, 02)) { Id = 15 }, // May Day holiday (2nd day)

    // Holidays for Employee - Mark Howard (howm)
    new Holiday("howm", new DateOnly(2025, 12, 26)) { Id = 16 }, // Day after Christmas
    new Holiday("howm", new DateOnly(2025, 12, 30)) { Id = 17 }, // Extra holiday
    new Holiday("howm", new DateOnly(2025, 06, 15)) { Id = 18 }, // Summer break (1 day)
    new Holiday("howm", new DateOnly(2025, 06, 16)) { Id = 19 }, // Summer break (2nd day)

    // Holidays for Employee - John Patel (patj)
    new Holiday("patj", new DateOnly(2025, 12, 25)) { Id = 20 }, // Christmas
    new Holiday("patj", new DateOnly(2025, 12, 28)) { Id = 21 }, // Extra holiday
    new Holiday("patj", new DateOnly(2025, 04, 01)) { Id = 22 }, // Spring break (1 day)
    new Holiday("patj", new DateOnly(2025, 04, 02)) { Id = 23 }, // Spring break (2nd day)

    // Holidays for Employee - Jane Van (vanj)
    new Holiday("vanj", new DateOnly(2025, 12, 27)) { Id = 24 }, // Day after Christmas
    new Holiday("vanj", new DateOnly(2025, 12, 29)) { Id = 25 }, // Extra holiday
    new Holiday("vanj", new DateOnly(2025, 03, 30)) { Id = 26 }, // Spring holiday (1 day)
    new Holiday("vanj", new DateOnly(2025, 03, 31)) { Id = 27 }, // Spring holiday (2nd day)
});

    
    //PROJECTS+TASKS
    modelBuilder.Entity<Project>().HasData(new List<Project>
{
    // Project 1: Cloud Security
    new Project("dojo", "Cloud Security", "Strengthening the security of cloud infrastructures, including network security, firewalls, and threat mitigation.", true, new DateOnly(2024, 04, 01), new DateOnly(2025, 04, 01), ProjectStatus.Ongoing, "Cloud Security", Priority.High) 
    { 
        ProjectId = 1 
    },
    
    // Project 2: Cloud Management
    new Project("dojo", "Cloud Management", "Optimizing and scaling cloud resources, monitoring usage, and managing cloud costs.", true, new DateOnly(2024, 05, 01), new DateOnly(2025, 05, 01), ProjectStatus.Ongoing, "Cloud Management", Priority.Medium)
    { 
        ProjectId = 2 
    },
    
    // Project 3: Kubernetes Implementation
    new Project("dojo", "Kubernetes Implementation", "Implementing Kubernetes clusters for container orchestration, focusing on scalability, security, and performance.", true, new DateOnly(2024, 06, 01), new DateOnly(2025, 06, 01), ProjectStatus.Ongoing, "Cloud Orchestration", Priority.High)
    { 
        ProjectId = 3 
    },

    // Project 4: Cloud Migration
    new Project("dojo", "Cloud Migration", "Migrating data, applications, and infrastructure to the cloud while minimizing disruption and optimizing performance.", true, new DateOnly(2024, 07, 01), new DateOnly(2025, 07, 01), ProjectStatus.Ongoing, "Cloud Migration", Priority.Medium)
    { 
        ProjectId = 4 
    },

    // Project 5: Data Security
    new Project("dojo", "Data Security", "Ensuring that all data hosted in the cloud is secure, with encryption, backup systems, and loss prevention strategies.", true, new DateOnly(2024, 08, 01), new DateOnly(2025, 08, 01), ProjectStatus.Ongoing, "Data Security", Priority.High)
    { 
        ProjectId = 5 
    },

    // Project 6: DevOps Automation
    new Project("dojo", "DevOps Automation", "Implementing automated pipelines and infrastructure as code for continuous integration and deployment in cloud environments.", true, new DateOnly(2024, 09, 01), new DateOnly(2025, 09, 01), ProjectStatus.Ongoing, "Infrastructure as Code", Priority.Medium)
    { 
        ProjectId = 6 
    },

    // Project 7: Cloud Networking
    new Project("dojo", "Cloud Networking", "Setting up secure and scalable cloud networks, including VPNs, network monitoring, and optimization for better performance.", true, new DateOnly(2024, 10, 01), new DateOnly(2025, 10, 01), ProjectStatus.Ongoing, "Cloud Networking", Priority.Medium)
    { 
        ProjectId = 7 
    },

    // Project 8: Cloud DevOps Pipeline
    new Project("dojo", "Cloud DevOps Pipeline", "Designing and implementing a full DevOps pipeline to enable automated deployments and optimized performance in cloud environments.", true, new DateOnly(2024, 11, 01), new DateOnly(2025, 11, 01), ProjectStatus.Ongoing, "Infrastructure as Code" , Priority.High)
    { 
        ProjectId = 8 
    },

    // Project 9: Cloud Data Management
    new Project("dojo", "Cloud Data Management", "Managing data in the cloud, from setup and synchronization to optimizing database performance and establishing data pipelines.", true, new DateOnly(2024, 12, 01), new DateOnly(2025, 12, 01), ProjectStatus.Ongoing, "Data Management", Priority.Medium)
    { 
        ProjectId = 9 
    }
});



modelBuilder.Entity<TaskProject>().HasData(new List<TaskProject>
{
  new TaskProject("Network Security Assessment", 1, "rosj", 40, TaskStatusEnum.Completed, new DateOnly(2024, 01, 01), new DateOnly(2024, 01, 20), null, 1, "Assessing network security for cloud services") { Id = 1, SequenceNo = 1 },
    new TaskProject("Firewall Configuration", 1, "tayl", 30, TaskStatusEnum.Completed, new DateOnly(2024, 01, 21), new DateOnly(2024, 02, 05), null, 2, "Setting up firewalls for cloud infrastructure") { Id = 2, SequenceNo = 2 },
    new TaskProject("Security Policy Review", 1, "howm", 50, TaskStatusEnum.Ongoing, new DateOnly(2024, 02, 06), new DateOnly(2024, 03, 31), null, 3, "Reviewing and updating security policies for cloud") { Id = 3, SequenceNo = 3 },
    new TaskProject("Threat Mitigation Plan", 1, "patj", 60, TaskStatusEnum.Ongoing, new DateOnly(2024, 04, 01), new DateOnly(2024, 05, 30), null, 4, "Creating a mitigation plan for cloud threats") { Id = 4, SequenceNo = 4 },

    // Tasks for Cloud Management Project
    new TaskProject("Cloud Resource Optimization", 2, "vanj", 50, TaskStatusEnum.Completed, new DateOnly(2024, 06, 01), new DateOnly(2024, 07, 20), null, 1, "Optimizing cloud resources for cost savings") { Id = 5, SequenceNo = 1 },
    new TaskProject("Scaling Infrastructure", 2, "rosj", 60, TaskStatusEnum.Ongoing, new DateOnly(2024, 07, 21), new DateOnly(2024, 09, 30), null, 2, "Scaling infrastructure for increased traffic") { Id = 6, SequenceNo = 2 },
    new TaskProject("Cloud Cost Analysis", 2, "tayl", 40, TaskStatusEnum.Completed, new DateOnly(2024, 10, 01), new DateOnly(2024, 10, 31), null, 3, "Analyzing cloud costs and usage patterns") { Id = 7, SequenceNo = 3 },
    new TaskProject("Monitoring Cloud Services", 2, "howm", 30, TaskStatusEnum.Completed, new DateOnly(2024, 11, 01), new DateOnly(2024, 11, 30), null, 4, "Monitoring cloud services performance") { Id = 8, SequenceNo = 4 },

    // Tasks for Kubernetes Implementation Project
    new TaskProject("Kubernetes Cluster Setup", 3, "patj", 60, TaskStatusEnum.Completed, new DateOnly(2024, 12, 01), new DateOnly(2025, 02, 28), null, 1, "Setting up Kubernetes clusters for container orchestration") { Id = 9, SequenceNo = 1 },
    new TaskProject("Containerization of Applications", 3, "vanj", 50, TaskStatusEnum.Ongoing, new DateOnly(2025, 03, 01), new DateOnly(2025, 04, 30), null, 2, "Containerizing applications for cloud deployment") { Id = 10, SequenceNo = 2 },
    new TaskProject("Kubernetes Security Configuration", 3, "rosj", 40, TaskStatusEnum.Ongoing, new DateOnly(2025, 05, 01), new DateOnly(2025, 05, 31), null, 3, "Configuring Kubernetes for enhanced security") { Id = 11, SequenceNo = 3 },
    new TaskProject("Kubernetes Load Balancing", 3, "tayl", 45, TaskStatusEnum.Ongoing, new DateOnly(2025, 06, 01), new DateOnly(2025, 07, 15), null, 4, "Setting up load balancing in Kubernetes clusters") { Id = 12, SequenceNo = 4 },

    // Tasks for Cloud Migration Project
    new TaskProject("Data Migration Planning", 4, "howm", 40, TaskStatusEnum.Completed, new DateOnly(2025, 07, 16), new DateOnly(2025, 08, 15), null, 1, "Planning the migration of data to the cloud") { Id = 13, SequenceNo = 1 },
    new TaskProject("Application Migration", 4, "patj", 50, TaskStatusEnum.Completed, new DateOnly(2025, 08, 16), new DateOnly(2025, 09, 30), null, 2, "Migrating cloud applications") { Id = 14, SequenceNo = 2 },
    new TaskProject("Infrastructure Migration", 4, "vanj", 60, TaskStatusEnum.Ongoing, new DateOnly(2025, 10, 01), new DateOnly(2025, 12, 31), null, 3, "Migrating infrastructure to cloud platforms") { Id = 15, SequenceNo = 3 },
    new TaskProject("Cloud Integration Testing", 4, "rosj", 45, TaskStatusEnum.Ongoing, new DateOnly(2026, 01, 01), new DateOnly(2026, 02, 15), null, 4, "Testing cloud integration with existing systems") { Id = 16, SequenceNo = 4 },

    // Tasks for Data Security Project
    new TaskProject("Encryption Setup", 5, "tayl", 50, TaskStatusEnum.Completed, new DateOnly(2026, 02, 16), new DateOnly(2026, 04, 05), null, 1, "Setting up data encryption for cloud services") { Id = 17, SequenceNo = 1 },
    new TaskProject("Backup System Implementation", 5, "howm", 60, TaskStatusEnum.Ongoing, new DateOnly(2026, 04, 06), new DateOnly(2026, 06, 25), null, 2, "Implementing backup systems for cloud data") { Id = 18, SequenceNo = 2 },
    new TaskProject("Data Loss Prevention", 5, "patj", 45, TaskStatusEnum.Completed, new DateOnly(2026, 06, 26), new DateOnly(2026, 08, 10), null, 3, "Creating data loss prevention strategies") { Id = 19, SequenceNo = 3 },
    new TaskProject("Security Audits", 5, "vanj", 55, TaskStatusEnum.Ongoing, new DateOnly(2026, 08, 11), new DateOnly(2026, 10, 04), null, 4, "Performing audits on cloud security and compliance") { Id = 20, SequenceNo = 4 },
    
     // Tasks for DevOps Automation Project
    new TaskProject("CI/CD Pipeline Setup", 6, "dojo", 50, TaskStatusEnum.Completed, new DateOnly(2024, 09, 01), new DateOnly(2024, 09, 15), null, 1, "Setting up CI/CD pipelines for automated deployment") { Id = 21, SequenceNo = 1 },
    new TaskProject("Infrastructure as Code", 6, "howm", 60, TaskStatusEnum.Ongoing, new DateOnly(2024, 09, 16), new DateOnly(2024, 10, 31), null, 2, "Implementing infrastructure as code for cloud services") { Id = 22, SequenceNo = 2 },
    new TaskProject("Automated Testing Implementation", 6, "tayl", 40, TaskStatusEnum.Completed, new DateOnly(2024, 11, 01), new DateOnly(2024, 11, 15), null, 3, "Implementing automated testing in DevOps pipeline") { Id = 23, SequenceNo = 3 },
    new TaskProject("Performance Monitoring", 6, "rosj", 50, TaskStatusEnum.Ongoing, new DateOnly(2024, 11, 16), new DateOnly(2024, 12, 31), null, 4, "Setting up performance monitoring in cloud environment") { Id = 24, SequenceNo = 4 },

    // Tasks for Cloud Networking Project
    new TaskProject("VPN Configuration", 7, "patj", 50, TaskStatusEnum.Completed, new DateOnly(2024, 10, 01), new DateOnly(2024, 10, 10), null, 1, "Setting up secure VPNs for cloud services") { Id = 25, SequenceNo = 1 },
    new TaskProject("Network Optimization", 7, "vanj", 40, TaskStatusEnum.Completed, new DateOnly(2024, 10, 11), new DateOnly(2024, 10, 20), null, 2, "Optimizing network performance in cloud") { Id = 26, SequenceNo = 2 },
    new TaskProject("Cloud Network Security", 7, "tayl", 60, TaskStatusEnum.Ongoing, new DateOnly(2024, 10, 21), new DateOnly(2024, 11, 30), null, 3, "Configuring security for cloud networks") { Id = 27, SequenceNo = 3 },
    new TaskProject("Network Monitoring Setup", 7, "dojo", 55, TaskStatusEnum.Ongoing, new DateOnly(2024, 12, 01), new DateOnly(2024, 12, 31), null, 4, "Setting up monitoring systems for cloud networks") { Id = 28, SequenceNo = 4 },

    // Tasks for Cloud DevOps Pipeline Project
    new TaskProject("Automated Deployments", 8, "howm", 50, TaskStatusEnum.Completed, new DateOnly(2024, 11, 01), new DateOnly(2024, 11, 15), null, 1, "Setting up automated deployments for cloud services") { Id = 29, SequenceNo = 1 },
    new TaskProject("Pipeline Optimization", 8, "rosj", 60, TaskStatusEnum.Ongoing, new DateOnly(2024, 11, 16), new DateOnly(2024, 12, 31), null, 2, "Optimizing DevOps pipelines for cloud") { Id = 30, SequenceNo = 2 },
    new TaskProject("CI/CD Monitoring", 8, "tayl", 45, TaskStatusEnum.Ongoing, new DateOnly(2025, 01, 01), new DateOnly(2025, 01, 31), null, 3, "Setting up monitoring for CI/CD pipelines") { Id = 31, SequenceNo = 3 },
    new TaskProject("Release Management", 8, "patj", 50, TaskStatusEnum.Ongoing, new DateOnly(2025, 02, 01), new DateOnly(2025, 02, 28), null, 4, "Managing releases in cloud environments") { Id = 32, SequenceNo = 4 },

    // Tasks for Cloud Data Management Project
    new TaskProject("Data Synchronization", 9, "vanj", 50, TaskStatusEnum.Completed, new DateOnly(2024, 12, 01), new DateOnly(2024, 12, 10), null, 1, "Synchronizing data across cloud platforms") { Id = 33, SequenceNo = 1 },
    new TaskProject("Database Optimization", 9, "rosj", 60, TaskStatusEnum.Ongoing, new DateOnly(2024, 12, 11), new DateOnly(2025, 01, 31), null, 2, "Optimizing cloud databases for performance") { Id = 34, SequenceNo = 2 },
    new TaskProject("Data Pipeline Setup", 9, "tayl", 45, TaskStatusEnum.Completed, new DateOnly(2025, 02, 01), new DateOnly(2025, 02, 15), null, 3, "Setting up data pipelines for cloud data management") { Id = 35, SequenceNo = 3 },
    new TaskProject("Data Backup Strategy", 9, "howm", 55, TaskStatusEnum.Ongoing, new DateOnly(2025, 02, 16), new DateOnly(2025, 03, 31), null, 4, "Designing data backup strategies for cloud data") { Id = 36, SequenceNo = 4 }
    
});
    
  



//TASKSKILLS
// TaskSkills
modelBuilder.Entity<TaskSkill>().HasData(new List<TaskSkill>
{
    // Task Skills for Cloud Security tasks
    new TaskSkill(1, "Cloud Security", Proficiency.Advanced) { Id = 1 },
    new TaskSkill(1, "Network Security", Proficiency.Intermediate) { Id = 2 },
    new TaskSkill(2, "Firewall Configuration", Proficiency.Advanced) { Id = 3 },
    new TaskSkill(2, "Cloud Infrastructure", Proficiency.Intermediate) { Id = 4 },
    new TaskSkill(3, "Security Policies", Proficiency.Intermediate) { Id = 5 },
    new TaskSkill(3, "Cloud Monitoring", Proficiency.Advanced) { Id = 6 },
    new TaskSkill(4, "Risk Assessment", Proficiency.Advanced) { Id = 7 },
    new TaskSkill(4, "Cloud Security", Proficiency.Advanced) { Id = 8 },

    // Task Skills for Cloud Management tasks
    new TaskSkill(5, "Cloud Optimization", Proficiency.Advanced) { Id = 9 },
    new TaskSkill(5, "Cost Management", Proficiency.Intermediate) { Id = 10 },
    new TaskSkill(6, "Infrastructure Scaling", Proficiency.Advanced) { Id = 11 },
    new TaskSkill(6, "Cloud Platform Knowledge", Proficiency.Advanced) { Id = 12 },
    new TaskSkill(7, "Cloud Cost Analysis", Proficiency.Advanced) { Id = 13 },
    new TaskSkill(7, "Cloud Monitoring", Proficiency.Intermediate) { Id = 14 },
    new TaskSkill(8, "Cloud Performance", Proficiency.Advanced) { Id = 15 },
    new TaskSkill(8, "Service Management", Proficiency.Intermediate) { Id = 16 },

    // Task Skills for Kubernetes Implementation tasks
    new TaskSkill(9, "Kubernetes", Proficiency.Advanced) { Id = 17 },
    new TaskSkill(9, "Containerization", Proficiency.Intermediate) { Id = 18 },
    new TaskSkill(10, "Cloud Deployment", Proficiency.Advanced) { Id = 19 },
    new TaskSkill(10, "Docker", Proficiency.Advanced) { Id = 20 },
    new TaskSkill(11, "Kubernetes Security", Proficiency.Advanced) { Id = 21 },
    new TaskSkill(11, "Container Security", Proficiency.Advanced) { Id = 22 },
    new TaskSkill(12, "Load Balancing", Proficiency.Advanced) { Id = 23 },
    new TaskSkill(12, "Kubernetes Networking", Proficiency.Intermediate) { Id = 24 },
});


    
    }
}