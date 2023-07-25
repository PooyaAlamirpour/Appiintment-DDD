using System;
using Charisma.Application.Appointments.Commands.CreateAppointment;
using Charisma.Application.Appointments.Commands.UpdateAppointment;
using Charisma.Application.Common.Models;
using Charisma.Application.Doctors.Commands;
using Charisma.Application.Doctors.Queries.GetDoctors;
using Charisma.Application.Salary.Queries;
using Charisma.Contracts.Appointments;
using Charisma.Contracts.Appointments.Requests;
using Charisma.Contracts.Appointments.Responses;
using Charisma.Contracts.Common;
using Charisma.Contracts.Doctors;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Doctors;
using Charisma.Domain.Core.Salary;
using Charisma.Domain.Core.Salary.ValueObjects;
using MediatR;

namespace Charisma.Presentation.Common.Mappings.Abstracts
{
    public interface IDtoConvertor
    {
        DefineDoctorCommand ToCommand(DefineDoctorRequest request);
        CreateDoctorScheduleCommand ToCommand(Guid doctorId, CreateDoctorScheduleRequest request);
        GetDoctorsQuery ToQuery(GetDoctorsQueryParameters queryParameters);
        PagedResponse<DoctorResponse>? ToDto(Paged<DoctorAggregateRoot> doctors);
        PagedResponse<AppointmentListResponse>? ToDto(Paged<AppointmentAggregateRoot> appointments);
        UpdateAppointmentCommand ToCommand(string trackingCode, UpdateAppointmentRequest request);
        SalaryResponse? ToDto(SalaryValueObject aggr);
        GetSalaryQuery ToQuery(Guid doctorId, GetDoctorSalaryQueryParameters parametrs);
    }
}