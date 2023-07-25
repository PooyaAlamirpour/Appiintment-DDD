using System;
using System.Collections.Generic;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Domain.SubDomains.Doctors;
using Appointment.Domain.SubDomains.Doctors.States;
using Appointment.Domain.SubDomains.Doctors.States.Abstracts;

namespace Appointment.Domain.Core.Appointments.ValueObjects
{
    public class DoctorValueObject : ValueObject
    {
        public DoctorSpeciality? Speciality { get; private set; }
        public WeeklySchedule WeeklySchedule { get; private set; }
        public IDoctorState DoctorState { get; private set; }
        private DoctorValueObject(DoctorSpeciality? speciality, WeeklySchedule weeklySchedule)
        {
            Speciality = speciality;
            WeeklySchedule = weeklySchedule;
            DoctorState = DoctorStateFactory.Make(speciality ?? DoctorSpeciality.General);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        public static DoctorValueObject New(DoctorSpeciality? speciality, WeeklySchedule schedule)
        {
            
            return new DoctorValueObject(speciality, schedule);
        }
    }
}