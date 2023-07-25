using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Persistence.Common.Constants;

namespace Appointment.Persistence.Entities
{
    [Table(TableNames.Schedules)]
    public class ScheduleEntity : Entity<long>
    {
        [Key]
        public long Id { get; set; }

        public Guid DoctorId { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime Start { get; set; } 
        public DateTime End { get; set; }
    }
}