using Charisma.Persistence.Common.Constants;
using Charisma.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charisma.Persistence.DbContexts.EfCore.Configurations
{
    public sealed class DoctorConfiguration : IEntityTypeConfiguration<DoctorEntity>
    {
        public void Configure(EntityTypeBuilder<DoctorEntity> builder)
        {
            builder.ToTable(TableNames.Doctors);

            builder.HasKey(entity => entity.Id);

            builder.HasMany<ScheduleEntity>(x => x.Schedules);
        }
    }
}