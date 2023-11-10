using Contracts;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Factories;
using Moq;
using Profile_API;
using Services;

namespace DoctorServices.Service.IntegrationTests
{
    public class DoctorService_ServiceTests : IClassFixture<WebAppFactory<Program>>
    {
        private readonly Mock<IDoctorRepository> _repositoryMock;
        private readonly DoctorService _service;

        public DoctorService_ServiceTests()
        {
            _repositoryMock = new Mock<IDoctorRepository>();
            _service = new DoctorService(_repositoryMock.Object);
        }

        [Fact]
        public async void CreateAsync_ValidDoctor_ReturnsCreatedDoctorDto()
        {
            // Arrange
            var doctorDto = new DoctorDTO { };
            var doctor = new Doctor { };
            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Doctor>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(doctor);

            // Act
            var result = await _service.CreateAsync(doctorDto, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DoctorDTO>(result);
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Doctor>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_InvalidSpecialization_ThrowsBadRequestException()
        {
            // Arrange
            var doctorDto = new DoctorDTO { };
            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Doctor>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Doctor)null);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _service.CreateAsync(doctorDto, CancellationToken.None));
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Doctor>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}