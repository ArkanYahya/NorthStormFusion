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
        public DbSet<ThankClassification> ThankClassifications { get; set; }            // التشكرات


        public DbSet<TmpAppreciation> TmpAppreciations { get; set; }
        public DbSet<TmpBonus> TmpBonuses { get; set; }
        public DbSet<TmpLeave> TmpLeaves { get; set; }
        public DbSet<TmpLeaveRequest> TmpLeaveRequests { get; set; }
        public DbSet<TmpPromotion> TmpPromotions { get; set; }


        public DbSet<EducationalInstitution> EducationalInstitutions { get; set; }      // المؤسسات التعليمية
        public DbSet<EducationalInstitutionClassification> EducationalInstituteClassifications { get; set; } // تصنيف المؤسسات التعليمية
        public DbSet<ComptenceClassification> ComptenceClassifications { get; set; }    // تصنيف التخصصات
        public DbSet<CareerClassification> CareerClassifications { get; set; }          // المهن والدور الوظيفي
        public DbSet<PunishmentClassification> PunishmentTypes { get; set; }            // أنواع العقوبات
        public DbSet<ResponsibleClassification> ResponsibleClassifications { get; set; }       // المسؤولية
        public DbSet<MilitaryClassification> MilitaryClassification { get; set; }       // الخدمة العسكرسة
        public DbSet<PrivilegeClassification> PrivilegeClassifications { get; set; }    // الامتيازات الوظيفية
        public DbSet<LeaveClassification> LeaveClassifications { get; set; }            // الاجازات
        public DbSet<StaffClassification> StaffClassifications { get; set; }            // نوع الملاك الاضافات
        public DbSet<DismissClassification> DismissClassifications { get; set; }        //  اشغال الملاك المطروحات
        public DbSet<Comptence> Comptences { get; set; }                                // التخصصات العلمية
        public DbSet<MaritalStatusClassification> MaritalStatusClassifications { get; set; }    // الحالة الاجتماعية
        public DbSet<EngilshLanguge> EngilshLanguges { get; set; }                      // مستويات اللغة الانكليزية
        public DbSet<ShiftClassification> ShiftClasifications { get; set; }             // دوام المناوبات
        public DbSet<RewardClassification> RewardClassifications { get; set; }          // دوام المكافات والمساعدات
        public DbSet<RankOther> RankOthers { get; set; }                                // يفيد لترتيب الحقول تصاعديا او تنازليا
        public DbSet<CollegeName> CollegeNames { get; set; }                            // الكليات و المعاهد
        public DbSet<Punishment> Punishments { get; set; }                              // اوامر عقوبات الموظفين
        public DbSet<Leave> Leaves { get; set; }                                        // اوامر الاجازات للموظفين
        public DbSet<EvaluationClassification> EvaluationClassifications { get; set; }  //  تصنيف التقييم للموظفين
        public DbSet<Evaluation> Evaluation { get; set; }                               //  تقييم للموظفين
        public DbSet<GeneralAndPrecise> GeneralAndPrecises { get; set; }                //  تصنيف العام و الدقيق
        public DbSet<DeputyClassification> DeputyClassifications { get; set; }          //  تصنيف الوكالة والاصالة
        public DbSet<BeneficiaryClassification> BeneficiaryClassifications { get; set; }    // مستفيد او لا
        public DbSet<ThankAndAppreciation> ThankAndAppreciations { get; set; }          // الشكر والتقدير للموظفين
        public DbSet<CreditedServiceClassification> CreditedServiceClassifications { get; set; }    // تصنيف الخدمة المضافة
        public DbSet<Shift> Shifts { get; set; }                                        // اوامر نوع الدوام/ المناوبات
        public DbSet<CreditedService> CreditedServices { get; set; }                    // اوامر نوع احتساب الخدمات المضمونة المضافة
        public DbSet<Privilege> Privileges { get; set; }                                // اوامر احتساب الامتيازات الوظيفية
        public DbSet<Retirement> Retirements { get; set; }                              // اوامر التقاعد
        public DbSet<JobPosition> JobPositions { get; set; }                            // اوامر المنصب الوظيفي
        public DbSet<Staffing> Staffings { get; set; }                                  // المـــــــــــلاك
        public DbSet<Absence> Absences { get; set; }                                    // الغيـــــــاب
        public DbSet<JobTitleChangeClassification> JobTitleChangeClassifications { get; set; }  // تصنيف نغيير العنوان الوظيفي
        public DbSet<DocumintsAndComminication> DocumintsAndComminications { get; set; }    // ملف الوثائق و التصال للموظفين
        public DbSet<NationalCard> NationalCards { get; set; }                          // ملف معلومات البطاقة الوطنية



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

            //modelBuilder.Entity<Certificate>()
            //    .HasOne(c => c.University)
            //    .WithMany()
            //    .HasForeignKey(c => c.UniversityId)
            //    .OnDelete(DeleteBehavior.NoAction);

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
