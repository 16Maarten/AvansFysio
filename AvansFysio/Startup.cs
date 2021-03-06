using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using DomainServices;
using Microsoft.AspNetCore.Identity;

namespace AvansFysio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbFysioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddDbContext<DbSecurityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Security")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DbSecurityContext>().AddDefaultTokenProviders();
            services.AddAuthorization(options =>options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("Employee")));
            services.AddAuthorization(options => options.AddPolicy("PatientOnly", policy => policy.RequireClaim("Patient")));

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPhysiotherapistRepository, PhysiotherapistRepository>();
            services.AddScoped<IPatientFileRepository, PatientFileRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<ITreatmentPlanRepository, TreatmentPlanRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IRemarkRepository, RemarkRepository>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IVektisRepository, VektisAPIRepository>();
            services.AddScoped<IPresenceRepository, PresenceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
