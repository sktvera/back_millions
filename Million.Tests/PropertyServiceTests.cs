using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Million.Application.Services;
using Million.Application.DTOs;
using Million.Domain.Entities;
using Million.Infrastructure.Repositories;

namespace Million.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IPropertyRepository> _mockRepo = null!;
        private PropertyService _service = null!;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPropertyRepository>();
            _service = new PropertyService(_mockRepo.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldCallRepositoryOnce_WithMappedProperty()
        {
            // Arrange
            var dto = new CreatePropertyDto
            {
                IdOwner = "1",
                Name = "Casa test",
                Address = "Calle 1",
                Price = 1_000_000m,
                ImageKey = null,
                ImageUrl = null
            };

            _mockRepo
                .Setup(r => r.CreateAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            // Act
            await _service.CreateAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.CreateAsync(It.Is<Property>(p =>
                p.IdOwner == dto.IdOwner &&
                p.Name == dto.Name &&
                p.Address == dto.Address &&
                p.Price == dto.Price &&
                p.ImageKey == dto.ImageKey &&
                p.ImageUrl == dto.ImageUrl
            )), Times.Once);
        }
    }
}
