using Appointment.Persistence.Common.Constants;
using Appointment.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Persistence.DbContexts.EfCore.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<AppointmentEntity>
    {
        public void Configure(EntityTypeBuilder<AppointmentEntity> builder)
        {
            builder.ToTable(TableNames.Appointments);

            builder.HasKey(entity => entity.Id);
        }
    }
}