using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Charisma.Domain.GenericCore.Abstractions;
using Charisma.Persistence.Common.Constants;

namespace Charisma.Persistence.Entities
{
    [Table(TableNames.Appointments)]
    public class PatientEntity : Entity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
    }
}