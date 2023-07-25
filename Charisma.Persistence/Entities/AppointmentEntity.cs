using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.GenericCore.Abstractions;
using Charisma.Persistence.Common.Constants;

namespace Charisma.Persistence.Entities
{
    [Table(TableNames.Appointments)]
    public class AppointmentEntity : Entity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string AppointmentId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual DoctorEntity Doctor { get; set; }
        
        [ForeignKey("PatientId")]
        public virtual PatientEntity Patient { get; set; }

        public DateTime AppointmentDateTimeStart { get; set; }
        public DateTime AppointmentDateTimeEnd { get; set; }
        public int DurationMinutes { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}