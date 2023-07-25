using System;
using Appointment.Domain.SubDomains.Doctors;
using Appointment.Persistence.Entities;

namespace Appointment.Persistence.DbContexts.EfCore.InMemory
{
    public class SeedInMemoryDatabase
    {
        public static void Seed(EfCoreDbContext dbContext)
        {
            try
            {
                InsertDoctors(dbContext);

                InsertSchedules(dbContext);

                InsertPatients(dbContext);

                InsertAppointments(dbContext);

                dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void InsertAppointments(EfCoreDbContext dbContext)
        {
            var appointment1 = new AppointmentEntity()
            {
                Id = Guid.Parse("E91E80C8-56A2-49CE-9039-02691E9BFB84"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 13, 30, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 13, 20, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "12230708-f7358b38-1822-439b-d66b-08db7c13ae69-ccdd2631-0c6a-4404-9af6-68e8fc84460c"
            };

            var appointment2 = new AppointmentEntity()
            {
                Id = Guid.Parse("3AA063D7-90F6-4279-6599-08DB7E0A1363"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 10, 58, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 10, 45, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 13,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230708-f7358b38-1822-439b-d66b-08db7c13ae69-ccdd2631-0c6a-4404-9af6-68e8fc84460c"
            };

            var appointment3 = new AppointmentEntity()
            {
                Id = Guid.Parse("26B30A4B-7C50-43EF-2592-08DB7E3B0E7D"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 10, 10, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 10, 0, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230708-f7358b38-1822-439b-d66b-08db7c13ae69-6ef4a691-bc87-492a-9e6a-b79c0d4a7d78"
            };

            var appointment4 = new AppointmentEntity()
            {
                Id = Guid.Parse("51517371-6B6C-4968-2593-08DB7E3B0E7D"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 10, 40, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 10, 30, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230708-f7358b38-1822-439b-d66b-08db7c13ae69-57f99218-8bee-47ac-bb40-2ac2cb41f447"
            };

            var appointment5 = new AppointmentEntity()
            {
                Id = Guid.Parse("35825388-ED05-4BBE-2594-08DB7E3B0E7D"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 11, 0, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 10, 50, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230708-f7358b38-1822-439b-d66b-08db7c13ae69-d7e67223-ae97-4689-9961-53236a6a1348"
            };

            var appointment6 = new AppointmentEntity()
            {
                Id = Guid.Parse("5C91E592-814E-4C06-2595-08DB7E3B0E7D"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 11, 10, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 11, 20, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230708-f7358b38-1822-439b-d66b-08db7c13ae69-ffac030a-92c5-48b3-8bee-c017191aeadb"
            };

            var appointment7 = new AppointmentEntity()
            {
                Id = Guid.Parse("5417C038-83A7-40D4-2596-08DB7E3B0E7D"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 11, 20, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 11, 10, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230708-f7358b38-1822-439b-d66b-08db7c13ae69-55fe6235-daa8-4145-b4df-4ce892865c29"
            };

            var appointment8 = new AppointmentEntity()
            {
                Id = Guid.Parse("CD072C5C-8F7B-4B40-EAE6-08DB7E3D40C1"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 09, 10, 10, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 09, 10, 0, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230709-f7358b38-1822-439b-d66b-08db7c13ae69-13bbd493-c943-4c00-ba0c-31310e372fcd"
            };

            var appointment9 = new AppointmentEntity()
            {
                Id = Guid.Parse("89384DB0-DCE3-49EA-EAE7-08DB7E3D40C1"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 09, 10, 30, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 09, 10, 20, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230709-f7358b38-1822-439b-d66b-08db7c13ae69-e473d20d-f8d7-4f00-a5ae-5970d1fb8c81"
            };

            var appointment10 = new AppointmentEntity()
            {
                Id = Guid.Parse("41EA76FA-FE6A-4DFB-BC67-08DB7ED2DF9F"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 16, 13, 3, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 16, 12, 53, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "20230709-f7358b38-1822-439b-d66b-08db7c13ae69-c2f7f816-7925-446e-b649-a715e035b55a"
            };

            var appointment11 = new AppointmentEntity()
            {
                Id = Guid.Parse("0A51071F-0715-4A95-A18F-8ED830A7922E"), CreatedDateTime = DateTime.Now,
                AppointmentDateTimeEnd = new DateTime(2023, 07, 08, 10, 30, 0),
                AppointmentDateTimeStart = new DateTime(2023, 07, 08, 10, 20, 0),
                DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DurationMinutes = 10,
                PatientId = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                AppointmentId = "13230708-f7358b38-1822-439b-d66b-08db7c13ae69-ccdd2631-0c6a-4404-9af6-68e8fc84460c"
            };

            dbContext.Appointments.Add(appointment1);
            dbContext.Appointments.Add(appointment2);
            dbContext.Appointments.Add(appointment3);
            dbContext.Appointments.Add(appointment4);
            dbContext.Appointments.Add(appointment5);
            dbContext.Appointments.Add(appointment6);
            dbContext.Appointments.Add(appointment7);
            dbContext.Appointments.Add(appointment8);
            dbContext.Appointments.Add(appointment9);
            dbContext.Appointments.Add(appointment10);
            dbContext.Appointments.Add(appointment11);
        }

        private static void InsertPatients(EfCoreDbContext dbContext)
        {
            var patient1 = new PatientEntity()
            {
                Id = Guid.Parse("257DB6E2-2B29-4597-84FA-06EBFDA2877E"),
                Name = "Patient 1",
                Family = "Family 1"
            };

            var patient2 = new PatientEntity()
            {
                Id = Guid.Parse("BD7E4456-6DD1-4388-B90B-305F7FD643A1"),
                Name = "Patient 2",
                Family = "Family 2"
            };

            dbContext.Patients.Add(patient1);
            dbContext.Patients.Add(patient2);
        }

        private static void InsertSchedules(EfCoreDbContext dbContext)
        {
            var schedule1 = new ScheduleEntity()
            {
                Id = 1, DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DayOfWeek = 0,
                Start = new DateTime(2023, 07, 08, 10, 0, 0),
                End = new DateTime(2023, 07, 08, 14, 0, 0)
            };

            var schedule2 = new ScheduleEntity()
            {
                Id = 2, DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DayOfWeek = 1,
                Start = new DateTime(2023, 07, 09, 10, 0, 0),
                End = new DateTime(2023, 07, 09, 14, 0, 0)
            };

            var schedule3 = new ScheduleEntity()
            {
                Id = 3, DoctorId = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), DayOfWeek = 0,
                Start = new DateTime(2023, 07, 08, 15, 30, 0),
                End = new DateTime(2023, 07, 08, 17, 30, 0)
            };
            
            var schedule4 = new ScheduleEntity()
            {
                Id = 4, DoctorId = Guid.Parse("3CC86E3A-969B-4457-D3D3-08DB7C142A2D"), DayOfWeek = 2,
                Start = new DateTime(2023, 07, 10, 15, 30, 0),
                End = new DateTime(2023, 07, 10, 17, 30, 0)
            };

            dbContext.Schedules.Add(schedule1);
            dbContext.Schedules.Add(schedule2);
            dbContext.Schedules.Add(schedule3);
            dbContext.Schedules.Add(schedule4);
        }

        private static void InsertDoctors(EfCoreDbContext dbContext)
        {
            var doctor1 = new DoctorEntity()
            {
                Id = Guid.Parse("F7358B38-1822-439B-D66B-08DB7C13AE69"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor2 = new DoctorEntity()
            {
                Id = Guid.Parse("3CC86E3A-969B-4457-D3D3-08DB7C142A2D"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor3 = new DoctorEntity()
            {
                Id = Guid.Parse("47756C97-76A9-4FDC-D3D4-08DB7C142A2D"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor4 = new DoctorEntity()
            {
                Id = Guid.Parse("F8239CBB-2EBB-413E-79AD-08DB7C155BF2"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor5 = new DoctorEntity()
            {
                Id = Guid.Parse("A9AB9A8D-5CFB-410C-79AE-08DB7C155BF2"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor6 = new DoctorEntity()
            {
                Id = Guid.Parse("B20D79B5-1D94-4715-DE41-08DB7C15BED7"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor7 = new DoctorEntity()
            {
                Id = Guid.Parse("E1EBE163-8447-41EC-A7F6-08DB7C15F7CB"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor8 = new DoctorEntity()
            {
                Id = Guid.Parse("E1460320-937C-42CB-BB70-08DB7C161AC7"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            var doctor9 = new DoctorEntity()
            {
                Id = Guid.Parse("8B8DB15E-1F64-42CF-B639-08DB7C18C984"), Name = "Doctor 1", Family = "General 1",
                Speciality = DoctorSpeciality.General
            };

            dbContext.Doctors.Add(doctor1);
            dbContext.Doctors.Add(doctor2);
            dbContext.Doctors.Add(doctor3);
            dbContext.Doctors.Add(doctor4);
            dbContext.Doctors.Add(doctor5);
            dbContext.Doctors.Add(doctor6);
            dbContext.Doctors.Add(doctor7);
            dbContext.Doctors.Add(doctor8);
            dbContext.Doctors.Add(doctor9);
        }
    }
}