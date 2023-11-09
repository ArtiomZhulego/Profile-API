using AutoFixture;
using Contracts;
using Contracts.CreatingDto;
using Domain;

namespace ReceptionistDtoMapper.Mappers.Tests
{
    public class ReceptionistDtoMapper_MapperTests
    {
        private readonly Fixture _fixture;

        public ReceptionistDtoMapper_MapperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void MapToReceptionist()
        {
            ///Arrange
            var receptionistDto = _fixture.Create<ReceptionistDTO>();

            ///Act
            var result = ReceptionistMapper.MapToReceptionist(receptionistDto);

            ///Assert
            Assert.NotNull(result);
            Assert.True(result is Receptionist);
        }

        [Fact]
        public void MapToReceptionistDto()
        {
            ///Arrange
            var receptionist = _fixture.Create<Receptionist>();

            ///Act
            var result = ReceptionistMapper.MapToReceptionistDto(receptionist);

            ///Assert
            Assert.NotNull(result);
            Assert.True(result is ReceptionistDTO);
        }
    }
}