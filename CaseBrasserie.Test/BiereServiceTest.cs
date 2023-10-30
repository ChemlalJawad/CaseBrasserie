using CaseBrasserie.Application.Repositories.Implementations;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CaseBrasserie.Test
{
    public class BiereServiceTest
    {

        [Fact]
        public void AddBiereAsync_ShouldAddBiere()
        {
            // Arrange
            var bieres = new List<Biere> { new Biere {
                Id = 1,
                Nom = "Leffe Blonde",
                DegreAlcool = 6.6F,
                Prix = 2.20M,
                BrasserieId = 1  // relation avec la Brasserie "Abbaye de Leffe"
            }};

            var mockSet = MoqHelpers.MockDbSet(bieres);
            var mockDbContext = new Mock<IBrasserieContext>();
            mockDbContext.Setup(c => c.Bieres).Returns(mockSet.Object);

            var biereRepo = new BiereRepository(mockDbContext.Object);
            // Act
            var result = biereRepo.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
