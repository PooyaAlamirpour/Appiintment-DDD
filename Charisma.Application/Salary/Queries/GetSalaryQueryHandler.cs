using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.Core.Salary;
using Charisma.Domain.Core.Salary.ValueObjects;
using Charisma.Domain.Core.Schedules;
using ErrorOr;

namespace Charisma.Application.Salary.Queries
{
    public class GetSalaryQueryHandler : IQueryHandler<GetSalaryQuery, ErrorOr<SalaryValueObject>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ISalaryAggregateRoot _salaryAggregateRoot;


        public GetSalaryQueryHandler(IDoctorRepository doctorRepository,  
            IAppointmentRepository appointmentRepository, ISalaryAggregateRoot salaryAggregateRoot)
        {
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _salaryAggregateRoot = salaryAggregateRoot;
        }
        
        public async Task<ErrorOr<SalaryValueObject>> Handle(GetSalaryQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            var doctorValueObject = DoctorValueObject.New(doctor.Speciality, doctor.Schedule);
            
            var doctorAppointments = await _appointmentRepository
                .GetDoctorAppointmentsForSpecificMonth(request.DoctorId, request.Year, request.Month);

            var appointmentRanges = doctorAppointments.Select(x =>
                new Range<DateTime>(x.AppointmentTime, x.AppointmentTime.AddMinutes(x.AppointmentDuration.Minutes)))
                .ToImmutableArray();
            
            var salaryValueObject = SalaryValueObject.New(appointmentRanges, doctorValueObject.DoctorState.SalaryPerMinutes); 
            var salaryAggregateRoot = SalaryAggregateRoot
                .Define(salaryValueObject);
            var totalSalary = await _salaryAggregateRoot.Do(salaryAggregateRoot);

            return totalSalary;
        }
    }
}