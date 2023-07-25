using System;
using System.Collections.Generic;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Schedules;
using Charisma.Domain.GenericCore.Abstractions;
using Charisma.Domain.SubDomains.Doctors;
using Charisma.Domain.SubDomains.Doctors.States;
using Charisma.Domain.SubDomains.Doctors.States.Abstracts;

namespace Charisma.Domain.Core.Appointments.ValueObjects
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