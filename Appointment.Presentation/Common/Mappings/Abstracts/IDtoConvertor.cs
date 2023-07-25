using System;
using Appointment.Application.Appointments.Commands.UpdateAppointment;
using Appointment.Application.Common.Models;
using Appointment.Application.Doctors.Commands;
using Appointment.Application.Doctors.Queries.GetDoctors;
using Appointment.Application.Salary.Queries;
using Appointment.Contracts.Appointments.Requests;
using Appointment.Contracts.Appointments.Responses;
using Appointment.Contracts.Common;
using Appointment.Contracts.Doctors;
using Appointment.Domain.Core.Appointments;
using Appointment.Domain.Core.Doctors;
using Appointment.Domain.Core.Salary.ValueObjects;

namespace Appointment.Presentation.Common.Mappings.Abstracts
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