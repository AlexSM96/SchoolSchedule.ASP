using Microsoft.EntityFrameworkCore;
using SchoolSchedule.DAL.Database.Context;
using SchoolSchedule.DAL.Database.Entities;
using SchoolSchedule.DAL.Interfaces;
using SchoolSchedule.DAL.Repositories.Base;
using SchoolSchedule.Service.Implementations;
using SchoolSchedule.Service.Interfaces;

void ConfigureServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services
        .AddScoped<IBaseRepository<Schedule>, BaseRepository<Schedule>>()
        .AddScoped<IScheduleService, ScheduleService>()
        .AddScoped<IBaseRepository<Class>, BaseRepository<Class>>()
        .AddScoped<IClassService, ClassService>()
        .AddScoped<IBaseRepository<Teacher>, BaseRepository<Teacher>>()
        .AddScoped<ITeacherService, TeacherService>()
        .AddScoped<IBaseRepository<Lesson>, BaseRepository<Lesson>>()
        .AddScoped<ILessonService, LessonService>();

}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

ConfigureServices(builder);

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<SchoolScheduleContext>(option =>
{
    option.UseSqlServer(connectionString);
});


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Schedule/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Schedule}/{action=GetSchedule}/{id?}");

app.Run();