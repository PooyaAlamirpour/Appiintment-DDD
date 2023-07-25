using Charisma.Persistence.Common.Constants;
using Charisma.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charisma.Persistence.DbContexts.EfCore.Configurations
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