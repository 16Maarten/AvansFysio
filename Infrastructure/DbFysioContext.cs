using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbFysioContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Physiotherapist> Physiotherapists { get; set; }
        public DbSet<Presence> Presences { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<PatientFile> PatientFiles { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlan { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbFysioContext(DbContextOptions<DbFysioContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presence>().HasData(new Presence(1, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0)));
            modelBuilder.Entity<Physiotherapist>().HasData(new Physiotherapist(1, "Peter", "Peter@fysio-avans.nl", "061234567", 5165842, 123456789));
        }
    }
}
