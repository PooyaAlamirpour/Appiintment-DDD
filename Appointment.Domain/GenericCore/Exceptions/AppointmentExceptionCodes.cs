namespace Appointment.Domain.GenericCore.Exceptions
{
    public class AppointmentExceptionCodes
    {
        public class MustBeWithinWorkingHourOfClinic
        {
            public const string Code = "Ex01";
            public const string Message = "Requested Date Must Be Within Working Hour Of Clinic";
        }
        
        public class MustBeAppropriateToTheDoctorSpeciality
        {
            public const string Code = "Ex03";
            public const string Message = "The Requested Duration Must Be Appropriate To The Doctor Speciality";
        }
        
        public class PatientMustHaveLessThanTwoAppointmentAtTheSameDay
        {
            public const string Code = "Ex04";
            public const string Message = "Patient Must Have Less Than Two Appointment At The Same Day";
        }
        
        public class AppointmentsOfPatientShouldNotOverlap
        {
            public const string Code = "Ex05";
            public const string Message = "Appointments Of Patient Should Not Overlap";
        }

        public class TheNumOfDoctorsOverlappingMustNotMoreThanSpecificNum
        {
            public const string Code = "Ex06";
            public const string Message = "The Number Of Doctors Overlapping Must Not More Than Specific Number";
        }
        
        public class TheRequestedDoctorDoesNotHaveTime
        {
            public const string Code = "Ex07";
            public const string Message = "The Requested Doctor Does Not Have Time";
        }
        
        public class MustBeDuringTheDoctorsPresents
        {
            public const string Code = "Ex08";
            public const string Message = "It Must Be During The Doctors Presents";
        }
        
        public class ThereIsNotFreeTimeForRequestedDoctor
        {
            public const string Code = "Ex08";
            public const string Message = "There Is Not Free Time For Requested Doctor";
        }
        
    }
}