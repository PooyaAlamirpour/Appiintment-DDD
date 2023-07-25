using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Appointment.Domain.Core.Salary.ValueObjects;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.GenericCore.Abstractions;
using Appointment.Domain.GenericCore.Interfaces;

namespace Appointment.Domain.Core.Salary
{
    public class SalaryAggregateRoot : AggregateRoot<SalaryValueObject>, IAuditableEntity, ISoftDeletableEntity, ISalaryAggregateRoot
    {
        public SalaryAggregateRoot() { }
        private SalaryAggregateRoot(ImmutableArray<Range<DateTime>> appointmentlist, decimal unitePerMinutes)
        {
            AppointmentList = appointmentlist;
            UnitePerMinutes = unitePerMinutes;
        }

        public DateTime CreatedOnUtc { get; }
        public DateTime? ModifiedOnUtc { get; }
        public DateTime? DeletedOnUtc { get; }
        public bool IsDeleted { get; }
        public ImmutableArray<Range<DateTime>> AppointmentList { get; private set; }
        public int TotalAppointmentPerMinutes { get; private set; }
        public decimal UnitePerMinutes { get; private set; }
        public decimal Salary { get; private set; }
        public async Task<SalaryValueObject> Do(SalaryAggregateRoot arg)
        {
            var sumMinutes = arg.AppointmentList.Sum(x => (int)(x.End.Subtract(x.Start).TotalMinutes));
            var salary = arg.UnitePerMinutes * sumMinutes;

            return SalaryValueObject.New(salary, arg.UnitePerMinutes, arg.AppointmentList, sumMinutes);
        }

        public static SalaryAggregateRoot Define(SalaryValueObject salaryValue)
        {
            return new SalaryAggregateRoot(salaryValue.TotalAppointmentList, salaryValue.UnitePerMinutes);
        }
    }
}