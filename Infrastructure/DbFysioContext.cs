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
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<PatientFile> PatientFiles { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbFysioContext(DbContextOptions<DbFysioContext> contextOptions) : base(contextOptions)
        {

        }
    }
}
