using Appointment.Persistence.Common.Constants;
using Appointment.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Persistence.DbContexts.EfCore.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<PatientEntity>
    {
        public void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.ToTable(TableNames.Patients);

            builder.HasKey(entity => entity.Id);
        }
    }
}