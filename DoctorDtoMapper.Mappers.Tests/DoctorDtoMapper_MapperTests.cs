using AutoFixture;
using Contracts;
using Contracts.CreatingDto;
using Domain;

namespace DoctorDtoMapper.Mappers.Tests
{
    public class DoctorDtoMapper_MapperTests
    {
        private readonly Fixture _fixture;

        public DoctorDtoMapper_MapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void MapToDoctor_ReturnDoctor()
        {
            ///Arrange
            var doctorDto = _fixture.Create<DoctorDTO>();

            ///Act
            var result = DoctorMapper.MapToDoctor(doctorDto);

            ///Assert
            Assert.NotNull(result);
            Assert.True(result is Doctor);
        }

        [Fact]
        public void MapToDoctorDto_ReturnDoctorDto()
        {
            ///Arrange
            var doctor = _fixture.Create<Doctor>();

            ///Act
            var result = DoctorMapper.MapToDoctorDto(doctor);

            ///Assert
            Assert.NotNull(result);
            Assert.True(result is DoctorDTO);
        }
    }
}