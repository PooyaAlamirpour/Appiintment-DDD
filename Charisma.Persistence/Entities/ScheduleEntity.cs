using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Charisma.Domain.Core.Doctors.ValueObjects;
using Charisma.Domain.GenericCore.Abstractions;
using Charisma.Persistence.Common.Constants;

namespace Charisma.Persistence.Entities
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