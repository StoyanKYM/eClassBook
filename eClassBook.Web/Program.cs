using AutoMapper;
using eClassBook.AutoMapper;
using eClassBook.Data;
using eClassBook.Data.DatabaseSeeder;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.Services.AccountServices;
using eClassBook.Services.ClassServices;
using eClassBook.Services.CurriculumService;
using eClassBook.Services.HomeServices;
using eClassBook.Services.ParentService;
using eClassBook.Services.PrincipalService;
using eClassBook.Services.SchoolAdminService;
using eClassBook.Services.SchoolServices;
using eClassBook.Services.SchoolUserService;
using eClassBook.Services.StatisticsService;
using eClassBook.Services.StudentService;
using eClassBook.Services.SubjectService;
using eClassBook.Services.TeacherService;
using eClassBook.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'eClassBookDbContextConnection' not found.");

builder.Services.AddDbContext<eClassBookDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<eClassBookDbContext>();;
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'eClassBookDbContextConnection' not found.");

//builder.Services.AddDbContext<eClassBookDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<eClassBookDbContext>();;

// Add services to the container.
//var connectionStringSecond = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<eClassBookDbContext>(options =>
//    options.UseSqlServer(connectionStringSecond));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 2;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = false;
})
    .AddDefaultUI()
    .AddEntityFrameworkStores<eClassBookDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(MappingProfile));

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//var mapperConfig = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile(new MappingProfile());
//});
//builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddScoped<IRepositories, Repositories>();
builder.Services.AddTransient<ISeeder, DatabaseInitializer>();

builder.Services.AddTransient<IGetCountService, GetCountService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISchoolService, SchoolService>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IParentService, ParentService>();
builder.Services.AddTransient<IPrincipalService, PrincipalService>();
builder.Services.AddTransient<ISchoolAdminService, SchoolAdminService>();
builder.Services.AddTransient<ISchoolUserService, SchoolUserService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICurriculumService, CurriculumService>();


//builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 3;
//    options.Password.RequiredUniqueChars = 0;

//    options.User.RequireUniqueEmail = true;
//})
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<eClassBookDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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
