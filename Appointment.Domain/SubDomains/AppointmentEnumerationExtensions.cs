using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Appointment.Domain.Core.Appointments;
using Appointment.Domain.Core.Appointments.ValueObjects;
using Appointment.Domain.Core.Schedules;
using Appointment.Domain.GenericCore.Extensions;

namespace Appointment.Domain.SubDomains
{
    public static class AppointmentEnumerationExtensions
    {
        public static bool HasNotOverlapWith(this IEnumerable<AppointmentAggregateRoot> source, DateTime appointmentTime,
            TimeSpan appointmentDuration)
        {
            var (start, end) = (appointmentTime.ToTimeOnly(), appointmentTime.ToTimeOnly().Add(appointmentDuration));
            return source.Select(a =>
                    new Range<TimeOnly>(a.AppointmentTime.ToTimeOnly(), a.AppointmentTime.ToTimeOnly().Add(a.AppointmentDuration)))
                .All(a => !a.Contains(start) && !a.Contains(end));
        }
        
        public static bool HasNotOverlapWith(this ImmutableArray<AppointmentHistoryValueObject> source, DateTime appointmentTime,
            TimeSpan appointmentDuration)
        {
            var (start, end) = (appointmentTime.ToTimeOnly(), appointmentTime.ToTimeOnly().Add(appointmentDuration));
            return source.Select(a =>
                    new Range<TimeOnly>(a.AppointmentTime.ToTimeOnly(), a.AppointmentTime.ToTimeOnly().Add(a.AppointmentDuration)))
                .All(a => !a.Contains(start) && !a.Contains(end));
        }
        
        public static int NumOfOverlapWith(this IEnumerable<AppointmentHistoryValueObject> source, DateTime appointmentTime,
            TimeSpan appointmentDuration)
        {
            var (start, end) =
                (appointmentTime.ToTimeOnly(), appointmentTime.ToTimeOnly().Add(appointmentDuration));

            var numOfOverlapWith = source.Select(a => 
                    new Range<TimeOnly>(a.AppointmentTime.ToTimeOnly(),
                        a.AppointmentTime.ToTimeOnly().Add(a.AppointmentDuration)))
                .Sum(x => (x.Contains(start) || x.Contains(end)) ? 1 : 0);
            return numOfOverlapWith;
        }
        
        public static int NumOfOverlapWith(this IEnumerable<Schedule> source, DateTime appointmentTime,
            TimeSpan appointmentDuration)
        {
            var (start, end) =
                (appointmentTime, appointmentTime.Add(appointmentDuration));

            var numOfOverlapWith = source.Select(s => s.DaySchedules
                .Sum(x => (x.Contains(start) && x.Contains(end)) ? 1 : 0))
                .Sum();
            return numOfOverlapWith;
        }
    }
}