using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Interfaces;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;

namespace NorthStorm.Data
{
    public class NorthStormContext : DbContext
    {
        public NorthStormContext(DbContextOptions<NorthStormContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<JobTransfer> JobTransfers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GovernmentalInstitute> GovernmentalInstitutes { get; set; }
        public DbSet<GovernmentalInstituteClassification> GovernmentalInstituteClassifications { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<JobTitleClassification> JobTitleClassifications { get; set; }
        public DbSet<LocationClassification> LocationClassifications { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<EmployeeJobTitle> EmployeeJobTitles { get; set; }
        public DbSet<LevelClassification> LevelClassifications { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>()
                .HasMany(l => l.ChildLevels)
                .WithOne(l => l.ParentLevel)
                .HasForeignKey(l => l.ParentLevelId);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.ChildLocations)
                .WithOne(l => l.ParentLocation)
                .HasForeignKey(l => l.ParentLocationId);

            modelBuilder.Entity<JobTitle>()
                .HasMany(l => l.ChildJobTitles)
                .WithOne(l => l.ParentJobTitle)
                .HasForeignKey(l => l.ParentJobTitleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobTransfer>()
                .HasOne(c => c.DestinationLevel)
                .WithMany()
                .HasForeignKey(c => c.DestinationLevelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.University)
                .WithMany()
                .HasForeignKey(c => c.UniversityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobTitle>()
                .HasOne(c => c.Grade)
                .WithMany()
                .HasForeignKey(c => c.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JobTitle>()
                .HasOne(c => c.Classification)
                .WithMany()
                .HasForeignKey(c => c.ClassificationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasMany(s => s.JobTransfers)
                .WithMany(c => c.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeJobTransfer",
                    j => j
                        .HasOne<JobTransfer>()
                        .WithMany()
                        .HasForeignKey("JobTransfersId")
                        .OnDelete(DeleteBehavior.Cascade), // Deleting JobTransfer will delete related records in EmployeeJobTransfers
                    j => j
                        .HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.NoAction)) // Specify delete behavior here
                .ToTable("EmployeeJobTransfer");

            modelBuilder.Entity<Certificate>()
                .HasMany(s => s.Employees)
                .WithMany(c => c.Certificates)
                .UsingEntity<Dictionary<string, object>>(
                    "CertificateEmployee",
                    j => j
                        .HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.NoAction), // Specify delete behavior here
                    j => j
                        .HasOne<Certificate>()
                        .WithMany()
                        .HasForeignKey("CertificatesId")
                        .OnDelete(DeleteBehavior.NoAction)) // Specify delete behavior here
                .ToTable("CertificateEmployee");

            // Configure the many-to-many relationship between Employee and JobTitle
            modelBuilder.Entity<EmployeeJobTitle>()
                .HasOne(ejt => ejt.Employee)
                .WithMany(e => e.EmployeeJobTitles)
                .HasForeignKey(ejt => ejt.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<EmployeeJobTitle>()
                .HasOne(ejt => ejt.JobTitle)
                .WithMany(jt => jt.EmployeeJobTitles)
                .HasForeignKey(ejt => ejt.JobTitleId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Employee>()
                .HasMany(s => s.Levels)
                .WithMany(c => c.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeLevel",
                    j => j
                        .HasOne<Level>()
                        .WithMany()
                        .HasForeignKey("LevelsId")
                        .OnDelete(DeleteBehavior.NoAction), // Specify delete behavior here
                    j => j
                        .HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.NoAction)) // Specify delete behavior here
                .ToTable("EmployeeLevel");

            modelBuilder.Entity<Employee>()
                .HasMany(s => s.Salaries)
                .WithMany(c => c.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeSalary",
                    j => j
                        .HasOne<Salary>()
                        .WithMany()
                        .HasForeignKey("SalariesId")
                        .OnDelete(DeleteBehavior.NoAction), // Specify delete behavior here
                    j => j
                        .HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.NoAction)) // Specify delete behavior here
                .ToTable("EmployeeSalary");


            modelBuilder.Entity<JobTitle>()
                .HasMany(s => s.Levels)
                .WithMany(c => c.JobTitles)
                .UsingEntity<Dictionary<string, object>>(
                    "JobTitleLevel",
                    j => j
                        .HasOne<Level>()
                        .WithMany()
                        .HasForeignKey("LevelsId")
                        .OnDelete(DeleteBehavior.NoAction), // Specify delete behavior here
                    j => j
                        .HasOne<JobTitle>()
                        .WithMany()
                        .HasForeignKey("JobTitlesId")
                        .OnDelete(DeleteBehavior.NoAction)) // Specify delete behavior here
                .ToTable("JobTitleLevel");

            modelBuilder.Entity<Employee>()
                .HasMany(s => s.Promotions)
                .WithMany(c => c.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeePromotion",
                    j => j
                        .HasOne<Promotion>()
                        .WithMany()
                        .HasForeignKey("PromotionsId")
                        .OnDelete(DeleteBehavior.NoAction), // Specify delete behavior here
                    j => j
                        .HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.NoAction)) // Specify delete behavior here
                .ToTable("EmployeePromotion");

            // Set the delete behavior to Restrict for all foreign keys in the model
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    foreach (var foreignKey in entityType.GetForeignKeys())
            //    {
            //        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            //    }
            //}

            //base.OnModelCreating(modelBuilder);


        }

    }
}
