using Charisma.Persistence.Common.Constants;
using Charisma.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charisma.Persistence.DbContexts.EfCore.Configurations
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