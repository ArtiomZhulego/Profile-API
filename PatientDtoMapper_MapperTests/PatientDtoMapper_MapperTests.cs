using AutoFixture;
using Contracts;
using Contracts.CreatingDto;
using Domain;

namespace PatientDtoMapper.Mappers.Tests
{
    public class PatientDtoMapper_MapperTests
    {
        private readonly Fixture _fixture;

        public PatientDtoMapper_MapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void MapToPatient()
        {
            ///Arrange
            var patientDto = _fixture.Create<PatientDTO>();

            ///Act
            var result = PatientMapper.MapToPatient(patientDto);

            ///Assert
            Assert.NotNull(result);
            Assert.True(result is Patient);
        }

        [Fact]
        public void MapToPatientDto()
        {
            ///Arrange
            var patient = _fixture.Create<Patient>();

            ///Act
            var result = PatientMapper.MapToPatientDto(patient);

            ///Assert
            Assert.NotNull(result);
            Assert.True(result is PatientDTO);
        }
    }
}