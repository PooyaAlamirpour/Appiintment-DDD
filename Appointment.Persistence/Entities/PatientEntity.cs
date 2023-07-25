using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Persistence.Common.Constants;

namespace Appointment.Persistence.Entities
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