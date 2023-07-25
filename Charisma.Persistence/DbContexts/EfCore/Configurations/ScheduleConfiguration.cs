using Charisma.Persistence.Common.Constants;
using Charisma.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charisma.Persistence.DbContexts.EfCore.Configurations
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