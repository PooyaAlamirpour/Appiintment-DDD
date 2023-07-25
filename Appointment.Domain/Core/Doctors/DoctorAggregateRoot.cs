using System;
using System.Threading.Tasks;
using Appointment.Domain.Core.Doctors.ValueObjects;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Domain.GenericCore.Interfaces;
using Appointment.Domain.SubDomains.Doctors;
using Appointment.Domain.SubDomains.Doctors.States;
using Appointment.Domain.SubDomains.Doctors.States.Abstracts;

namespace Appointment.Domain.Core.Doctors
{
    public class DoctorAggregateRoot : AggregateRoot<DoctorIdValueObject>, IAuditableEntity, ISoftDeletableEntity, IDoctorAggregateRoot
    {
        public DoctorIdValueObject DoctorId { get; private set; }
        public DoctorNameValueObject Name { get; private set;  }
        public DoctorFamilyValueObject Family { get; private set;  }
        public DoctorSpeciality? Speciality { get; private set;  }
        public WeeklySchedule Schedule { get; private set; }
        public IDoctorState DoctorState { get; private set; }
        public DateTime CreatedOnUtc { get; }
        public DateTime? ModifiedOnUtc { get; }
        public DateTime? DeletedOnUtc { get; }
        public bool IsDeleted { get; }

        private DoctorAggregateRoot(DoctorNameValueObject name, DoctorFamilyValueObject family, DoctorSpeciality? speciality)
        {
            Family = family;
            Name = name;
            Speciality = speciality;

            DoctorState = DoctorStateFactory.Make(speciality ?? DoctorSpeciality.General);
        }
        
        public Task<DoctorIdValueObject> Do(DoctorAggregateRoot arg)
        {
            throw new NotImplementedException();
        }

        public static DoctorAggregateRoot Define(DoctorNameValueObject name, DoctorFamilyValueObject family,
            DoctorSpeciality? speciality) => new(name, family, speciality);

        public void SetId(Guid doctorId)
        {
            this.DoctorId = DoctorIdValueObject.New(doctorId);
        }

        public DoctorAggregateRoot SetSchedule(WeeklySchedule schedule)
        {
            Schedule = schedule;
            return this;
        }
    }
}