using FluentAssertions;
using NetArchTest.Rules;

namespace Appointment.Test
{
    public class ArchitectureTests
    {
        private const string ApplicationNamespace = "Appointment.Application";
        private const string DomainNamespace = "Appointment.Domain";
        private const string InfrastructureNamespace = "Appointment.Infrastructure";
        private const string PersistenceNamespace = "Appointment.Persistence";
        private const string PresentationNamespace = "Appointment.Presentation";
        private const string WebNamespace = "Appointment.Web";

        [Fact]
        public void Domain_Should_Not_Have_DependencyOnOtherLayers()
        {
            // Arrange
            var assembly = typeof(Domain.DomainAssembly).Assembly;
            var otherLayers = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherLayers)
                .GetResult();
            
            // Asset
            result.IsSuccessful.Should().BeTrue();
        }
        
        [Fact]
        public void Application_Should_Not_Have_DependencyOnOtherLayers()
        {
            // Arrange
            var assembly = typeof(Application.ApplicationAssembly).Assembly;
            var otherLayers = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherLayers)
                .GetResult();
            
            // Asset
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Handlers_Should_have_DependencyOnDomain()
        {
            // Arrange
            var assembly = typeof(Application.ApplicationAssembly).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();

        }
        
        [Fact]
        public void Infrastructure_Should_Not_Have_DependencyOnOtherLayers()
        {
            // Arrange
            var assembly = typeof(Infrastructure.InfrastructureAssembly).Assembly;
            var otherLayers = new[]
            {
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherLayers)
                .GetResult();
            
            // Asset
            result.IsSuccessful.Should().BeTrue();
        }
        
        [Fact]
        public void Presentation_Should_Not_Have_DependencyOnOtherLayers()
        {
            // Arrange
            var assembly = typeof(Presentation.PresentationAssembly).Assembly;
            var otherLayers = new[]
            {
                InfrastructureNamespace,
                WebNamespace
            };

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherLayers)
                .GetResult();
            
            // Asset
            result.IsSuccessful.Should().BeTrue();
        }
        
        [Fact]
        public void Controllers_Should_Not_Have_DependencyOnMediatR()
        {
            // Arrange
            var assembly = typeof(Presentation.PresentationAssembly).Assembly;

            // Act
            var result = Types.InAssembly(assembly)
                .That()
                .HaveNameEndingWith("AppointmentsController")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();
            
            // Asset
            result.IsSuccessful.Should().BeTrue();
        }
    }
}