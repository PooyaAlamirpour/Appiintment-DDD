using Appointment.Persistence.Common.Constants;
using Appointment.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Persistence.DbContexts.EfCore.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
    {
        public void Configure(EntityTypeBuilder<ScheduleEntity> builder)
        {
            builder.ToTable(TableNames.Schedules);

            builder.HasKey(entity => entity.Id);
        }
    }
}