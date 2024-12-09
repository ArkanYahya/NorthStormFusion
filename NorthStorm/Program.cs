using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NorthStorm.Data;
using Microsoft.Extensions.DependencyInjection;
using NorthStorm.Interfaces;
using NorthStorm.Repositories;
using System.Security.Policy;
using Microsoft.AspNetCore.Authentication;
using NorthStorm.Interfaces.Classifications;
using NorthStorm.Repositories.Classifications;
using NorthStorm.Models;
using NorthStorm.Models.Assistants;
using NorthStorm.Models.Classifications;
using NorthStorm.Interfaces.Assistants;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NorthStormContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthStormContext") ?? throw new InvalidOperationException("Connection string 'NorthStormContext' not found."), o => o.UseCompatibilityLevel(120)));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'NorthStormContext' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// The AddDatabaseDeveloperPageExceptionFilter provides helpful error information in the development environment for EF migrations errors.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


// Classification Interferces
builder.Services.AddScoped<IBeneficiaryClassification, BeneficiaryClassificationRepo>();
builder.Services.AddScoped<ICareerClassification, CareerClassificationRepo>();
builder.Services.AddScoped<ICollegeName, CollegeNameRepo>();
builder.Services.AddScoped<IComptenceClassification, ComptenceClassificationRepo>();
builder.Services.AddScoped<ICreditedServiceClassification, CreditedServiceClassificationRepo>();
builder.Services.AddScoped<IDeputyClassification, DeputyClassificationRepo>();
builder.Services.AddScoped<IDismissClassification, DismissClassificationRepo>();
builder.Services.AddScoped<IEducationalInstitution, EducationalInstitutionRepo>();
builder.Services.AddScoped<IEducationalInstitutionClassification,  EducationalInstitutionClassificationRepo>();
builder.Services.AddScoped<IEngilshLanguge, EngilshLangugeRepo>();
builder.Services.AddScoped<IEvaluationClassification, EvaluationClassificationRepo>();
builder.Services.AddScoped<IGender, GenderRepo>();
builder.Services.AddScoped<IGeneralAndPrecise,  GeneralAndPreciseRepo>();
builder.Services.AddScoped<IGovernmentalInstituteClassification, GovernmentalInstituteClassificationRepo>();
builder.Services.AddScoped<IJobTitleChangeClassification, JobTitleChangeClassificationRepo>();
builder.Services.AddScoped<IJobTitleClassification, JobTitleClassificationRepo>();
builder.Services.AddScoped<ILeaveClassification, LeaveClassificationRepo>();
builder.Services.AddScoped<ILevelClassification, LevelClassificationRepo>();
builder.Services.AddScoped<ILocationClassification, LocationClassificationRepo>();
builder.Services.AddScoped<IMaritalStatusClassification, MaritalStatusClassificationRepo>();
builder.Services.AddScoped<IMilitaryClassification, MilitaryClassificationRepo>();
builder.Services.AddScoped<INationality, NationalityRepo>();
builder.Services.AddScoped<IPrivilegeClassification, PrivilegeClassificationRepo>();
builder.Services.AddScoped<IPunishmentClassification,  PunishmentTypeRepo>();
builder.Services.AddScoped<IRace, RaceRepo>();
builder.Services.AddScoped<IReligion, ReligionRepo>();
builder.Services.AddScoped<IResponsibleClassification, ResponsibleClassificationRepo>();
builder.Services.AddScoped<IRewardClassification, RewardClassificationRepo>();
builder.Services.AddScoped<IShiftClassification, ShiftClassificationRepo>();
builder.Services.AddScoped<IStaffClassification, StaffClassificationRepo>();
builder.Services.AddScoped<IStatus, StatusRepo>();
builder.Services.AddScoped<IThankClassification, ThankClassificationRepo>();

// Root Interfaces
builder.Services.AddScoped<IAbsence, AbsenceRepo>();
builder.Services.AddScoped<ICertificate, CertificateRepo>();
builder.Services.AddScoped<IComptence, ComptenceRepo>();
builder.Services.AddScoped<ICreditedService, CreditedServiceRepo>();
builder.Services.AddScoped<IEmployee, EmployeeRepo>();
builder.Services.AddScoped<IEvaluation, EvaluationRepo>();
builder.Services.AddScoped<IGovernmentalInstitute, GovernmentalInstituteRepo>();
builder.Services.AddScoped<IGrade, GradeRepo>();
builder.Services.AddScoped<IJobPosition, JobPositionRepo>();
builder.Services.AddScoped<IJobTitle, JobTitleRepo>();
builder.Services.AddScoped<IJobTransfer, JobTransferRepo>();
builder.Services.AddScoped<ILeave, LeaveRepo>();
builder.Services.AddScoped<ILevel, LevelRepo>();
builder.Services.AddScoped<ILocation, LocationRepo>();
builder.Services.AddScoped<INationalCard, NationalCardRepo>();
builder.Services.AddScoped<IPrivilege, PrivilegeRepo>();
builder.Services.AddScoped<IPromotion, PromotionRepo>();
builder.Services.AddScoped<IPunishment, PunishmentRepo>();
builder.Services.AddScoped<IRankOther, RankOtherRepo>();
builder.Services.AddScoped<IRetirement, RetirementRepo>();
builder.Services.AddScoped<IRecruitment, RecruitmentRepo>();
builder.Services.AddScoped<IShift, ShiftRepo>();
builder.Services.AddScoped<IStaffing, StaffingRepo>();
builder.Services.AddScoped<IThankAndAppreciation, ThankAndAppreciationRepo>();



// اضافة
// The AddDatabaseDeveloperPageExceptionFilter
// provides helpful error information in the development
// environment for EF migrations errors.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // اضافة
    // The Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore NuGet package provides
    // ASP.NET Core middleware for Entity Framework Core error pages. This middleware
    // helps to detect and diagnose errors with Entity Framework Core migrations.
    app.UseDeveloperExceptionPage();

    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// اضافة
// create database if doesn't exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<NorthStormContext>();
    // I used Migrate instead of EnsureCreated to ensure future maigrate update
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

//if (env.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
