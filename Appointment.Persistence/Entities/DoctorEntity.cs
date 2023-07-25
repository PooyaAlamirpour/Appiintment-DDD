using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Domain.SubDomains.Doctors;
using Appointment.Persistence.Common.Constants;

namespace Appointment.Persistence.Entities
{
    [Table(TableNames.Doctors)]
    public class DoctorEntity : Entity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public DoctorSpeciality? Speciality { get; set; }
        
        [ForeignKey("DoctorId")]
        public virtual ICollection<ScheduleEntity> Schedules { get; set; }
        public virtual ICollection<AppointmentEntity> Appointments { get; set; }
    }
}