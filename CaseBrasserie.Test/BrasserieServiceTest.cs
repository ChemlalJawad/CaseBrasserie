using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Application.Repositories.Implementations;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Test
{
    public class BrasserieServiceTest
    {
        private IBrasserieRepository _brasserieRepository;
        private Mock<DbSet<Biere>> _mockBiereDbSet;
        private Mock<IBrasserieContext> _mockDbContext;
        private Mock<DbSet<Brasserie>> _mockBrasserieDbSet;

        public BrasserieServiceTest()
        {
            // Arrange
            var bieres = new List<Biere> {
                new Biere
                {
                    Id = 1,
                    Nom = "Leffe Blonde",
                    DegreAlcool = 6.6F,
                    Prix = 2.20M,
                    BrasserieId = 1
                },
                new Biere
                {
                    Id = 2,
                    Nom = "Leffe Jawad",
                    DegreAlcool = 10.6F,
                    Prix = 5.20M,
                    BrasserieId = 2
                },
                new Biere
                {
                    Id = 3,
                    Nom = "Biere Test",
                    DegreAlcool = 1.6F,
                    Prix = 3.20M,
                    BrasserieId = 1
                },
            };
            var brasseries = new List<Brasserie>
            {
                new Brasserie { Id = 1, Nom = "Test Brasserie"},
                new Brasserie { Id = 2, Nom = "Test Brasserie 2"},
            };

            _mockBiereDbSet = MoqHelpers.MockDbSet(bieres);
            _mockBrasserieDbSet = MoqHelpers.MockDbSet(brasseries);

            _mockDbContext = new Mock<IBrasserieContext>();
            _mockDbContext.Setup(c => c.Bieres).Returns(_mockBiereDbSet.Object);
            _mockDbContext.Setup(c => c.Brasseries).Returns(_mockBrasserieDbSet.Object);

            _brasserieRepository = new BrasserieRepository(_mockDbContext.Object);
        }

        [Fact]
        public void GetAllBieres_ShouldReturnAllBieres()
        {
            // Act
            var result = _brasserieRepository.GetAllBrasseries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FindBrasserieById_ShouldReturnCorrectBrasserie()
        {

            // Act
            var id = 1;
            var result = _brasserieRepository.FindBrasserieById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void FindBrasserieById_InexistanteBrasserieException()
        {
            // Arrange
            int id = 10;

            // Act & Assert
            Assert.Throws<BrasserieInexistantException>(() => _brasserieRepository.FindBrasserieById(id));
        }
    }
}
