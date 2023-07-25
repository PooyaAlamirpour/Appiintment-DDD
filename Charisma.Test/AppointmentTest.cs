using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Contracts.Appointments.Requests;
using Charisma.Contracts.Appointments.Responses;
using Charisma.Contracts.Common;
using Charisma.Test.Helper;
using Charisma.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

namespace Charisma.Test
{
    public class AppointmentTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        
        public AppointmentTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ShouldReturnsAppointmentList_WhenCalled()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync("/api/v1/appointments");
            
            // Assert
            response.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task SetAppointment_ShouldReturnsException_WhenCalledWithOutOfClinicWorkingDay()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new CreateAppointmentRequest("F7358B38-1822-439B-D66B-08DB7C13AE69", "257DB6E2-2B29-4597-84FA-06EBFDA2877E", 
                10, new DateTime(2023, 07, 21));
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("/api/v1/appointment", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("Requested Date Must Be Within Working Hour Of Clinic", errorResponse?.title);
        }
        
        [Fact]
        public async Task SetAppointment_ShouldReturnsException_WhenDurationNotAppropriateToTheDoctorSpeciality()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new CreateAppointmentRequest("F7358B38-1822-439B-D66B-08DB7C13AE69", "257DB6E2-2B29-4597-84FA-06EBFDA2877E", 
                30, new DateTime(2023, 07, 22, 11, 30, 00));
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("/api/v1/appointment", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("The Requested Duration Must Be Appropriate To The Doctor Speciality", errorResponse?.title);
        }
        
        [Fact]
        public async Task SetAppointment_ShouldReturnsException_WhenAppointmentTimeDuringDoctorNotPresents()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new CreateAppointmentRequest("F7358B38-1822-439B-D66B-08DB7C13AE69", "257DB6E2-2B29-4597-84FA-06EBFDA2877E", 
                10, new DateTime(2023, 07, 24, 11, 30, 00));
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("/api/v1/appointment", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("It Must Be During The Doctors Presents", errorResponse?.title);
        }
        
        [Fact]
        public async Task SetAppointment_ShouldReturnsException_WhenAppointmentsOfPatientHasOverlap()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new CreateAppointmentRequest("F7358B38-1822-439B-D66B-08DB7C13AE69", "257DB6E2-2B29-4597-84FA-06EBFDA2877E", 
                10, new DateTime(2023, 07, 08, 10, 20, 00));
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("/api/v1/appointment", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("Appointments Of Patient Should Not Overlap", errorResponse?.title);
        }
        
        [Fact]
        public async Task SetAppointment_ShouldReturnsException_WhenPatientMoreThanTwoAppointmentAtTheSameDate()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new CreateAppointmentRequest("F7358B38-1822-439B-D66B-08DB7C13AE69", "257DB6E2-2B29-4597-84FA-06EBFDA2877E", 
                10, new DateTime(2023, 07, 08, 11, 45, 00));
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("/api/v1/appointment", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("Patient Must Have Less Than Two Appointment At The Same Day", errorResponse?.title);
        }
        
        [Fact]
        public async Task SetAppointment_ShouldReturnsException_WhenADoctorHasMoreThanInvalidOverlappingAppointment()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new CreateAppointmentRequest("F7358B38-1822-439B-D66B-08DB7C13AE69", "bd7e4456-6dd1-4388-b90b-305f7fd643a1", 
                14, new DateTime(2023, 07, 08, 11, 10, 00));
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("/api/v1/appointment", content);
            string responseBody = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal("The Number Of Doctors Overlapping Must Not More Than Specific Number", errorResponse?.title);
        }
    }    
}

