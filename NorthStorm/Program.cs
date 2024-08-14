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
using NorthStorm.Interfaces.Tmp;
using NorthStorm.Repositories.Tmp;

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



builder.Services.AddScoped<IGender, GenderRepo>();
builder.Services.AddScoped<IGovernmentalInstituteClassification, GovernmentalInstituteClassificationRepo>();
builder.Services.AddScoped<IJobTitleClassification, JobTitleClassificationRepo>();
builder.Services.AddScoped<ILevelClassification, LevelClassificationRepo>();
builder.Services.AddScoped<ILocationClassification, LocationClassificationRepo>();
builder.Services.AddScoped<INationality, NationalityRepo>();
builder.Services.AddScoped<IRace, RaceRepo>();
builder.Services.AddScoped<IReligion, ReligionRepo>();

builder.Services.AddScoped<IEmployee, EmployeeRepo>();
builder.Services.AddScoped<IGovernmentalInstitute, GovernmentalInstituteRepo>();
builder.Services.AddScoped<IGrade, GradeRepo>();
builder.Services.AddScoped<IJobTitle, JobTitleRepo>();
builder.Services.AddScoped<IJobTransfer, JobTransferRepo>();
builder.Services.AddScoped<ILevel, LevelRepo>();
builder.Services.AddScoped<ILocation, LocationRepo>();
builder.Services.AddScoped<IRecruitment, RecruitmentRepo>();


builder.Services.AddScoped<ITmpAppreciation, TmpAppreciationRepo>();
builder.Services.AddScoped<ITmpBonus, TmpBonusRepo>();
builder.Services.AddScoped<ITmpLeave, TmpLeaveRepo>();
builder.Services.AddScoped<ITmpLeaveRequest, TmpLeaveRequestRepo>();
builder.Services.AddScoped<ITmpPromotion, TmpPromotionRepo>();


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
    pattern: "{controller=PublicHomepage}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
