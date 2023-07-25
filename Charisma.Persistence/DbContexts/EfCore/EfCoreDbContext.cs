using System;
using System.Data;
using Charisma.Persistence.DbContexts.EfCore.InMemory;
using Charisma.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Charisma.Persistence.DbContexts.EfCore
{
    public class EfCoreDbContext : DbContext
    {
        public DbSet<AppointmentEntity> Appointments { get; set; }
        public DbSet<DoctorEntity> Doctors { get; set; }
        public DbSet<ScheduleEntity> Schedules { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {
            // todo: Comment the below code for real database usage
            SeedInMemoryDatabase.Seed(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(PersistenceAssembly.Assembly);
        }
    }
}