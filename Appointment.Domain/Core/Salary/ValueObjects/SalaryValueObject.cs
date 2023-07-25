using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.GenericCore.Abstractions;

namespace Appointment.Domain.Core.Salary.ValueObjects
{
    public class SalaryValueObject : ValueObject
    {
        public ImmutableArray<Range<DateTime>> TotalAppointmentList { get; private set; }
        public decimal UnitePerMinutes { get; private set; }
        public decimal Salary { get; private set; }
        public int TotalAppointmentPerMinutes { get; private set; }

        private SalaryValueObject(ImmutableArray<Range<DateTime>> totalAppointmentList, decimal unitPerMinutes)
        {
            TotalAppointmentList = totalAppointmentList;
            UnitePerMinutes = unitPerMinutes;
        }
        
        private SalaryValueObject(decimal salary, decimal unitePerMinutes,
            ImmutableArray<Range<DateTime>> totalAppointmentList, int totalAppointmentPerMinutes)
        {
            TotalAppointmentList = totalAppointmentList;
            UnitePerMinutes = unitePerMinutes;
            Salary = salary;
            TotalAppointmentPerMinutes = totalAppointmentPerMinutes;
        }
        
        public static SalaryValueObject New(ImmutableArray<Range<DateTime>> appointmentMinutes, decimal unitPerMinutes)
        {
            return new SalaryValueObject(appointmentMinutes, unitPerMinutes);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
        // salary, arg.UnitePerMinutes, arg.TotalTreatmentMinutes
        public static SalaryValueObject New(decimal salary, decimal unitePerMinutes,
            ImmutableArray<Range<DateTime>> appointmentList, int totalAppointmentPerMinutes)
        {
            return new SalaryValueObject(salary, unitePerMinutes, appointmentList, totalAppointmentPerMinutes);
        }
    }
}