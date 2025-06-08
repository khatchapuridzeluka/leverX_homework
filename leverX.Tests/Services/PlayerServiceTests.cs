using AutoMapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Services;
using leverX.Domain.Entities;
using leverX.DTOs.Players;
using Moq;
using Xunit;

namespace leverX.Tests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _playerRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PlayerService _service;

        public PlayerServiceTests()
        {
            _playerRepoMock = new Mock<IPlayerRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new PlayerService(_playerRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidDto_AddsPlayerAndReturnsDto()
        {

            var createDto = new CreatePlayerDto { Name = "Test Player", LastName = "Test1", FideRating = 2300, Nationality = 0, Sex = 0, Title = 0 };
            var playerEntity = new Player { Id = Guid.NewGuid(), Name = "Test Player", LastName = "Test1", FideRating = 2300, Nationality = 0, Sex = 0, Title = 0 };
            var playerDto = new PlayerDto { Id = playerEntity.Id, Name = "Test Player", LastName = "Test1", FideRating = 2300, Nationality = 0, Sex = 0, Title = 0 };

            _mapperMock.Setup(m => m.Map<Player>(createDto)).Returns(playerEntity);


            var result = await _service.CreateAsync(createDto);


            _playerRepoMock.Verify(r => r.AddAsync(playerEntity), Times.Once);
            Assert.Equal(playerEntity.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_PlayerExists_ReturnsDto()
        {
            var id = Guid.NewGuid();
            var playerEntity = new Player { Id = id, Name = "Test" };
            var playerDto = new PlayerDto { Id = id, Name = "Test" };

            _playerRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(playerEntity);
            _mapperMock.Setup(m => m.Map<PlayerDto>(playerEntity)).Returns(playerDto);

            var result = await _service.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(playerDto.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_PlayerDoesNotExist_ReturnsNull()
        {
            var id = Guid.NewGuid();
            _playerRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Player?)null);

            var result = await _service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_PlayerExists_UpdatesEntity()
        {
            var id = Guid.NewGuid();
            var player = new Player { Id = id, Name = "Old Name" };
            var dto = new UpdatePlayerDto { Name = "New Name" };

            _playerRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(player);
            _mapperMock.Setup(m => m.Map(dto, player)); 

            await _service.UpdateAsync(id, dto);

            _playerRepoMock.Verify(r => r.UpdateAsync(player), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryDelete()
        {
            var id = Guid.NewGuid();

            await _service.DeleteAsync(id);

            _playerRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }



    }
}