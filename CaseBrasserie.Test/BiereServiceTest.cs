using CaseBrasserie.Application.Repositories.Commands.Bieres;
using CaseBrasserie.Application.Repositories.Implementations;
using CaseBrasserie.Application.Repositories.Interfaces;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Test
{
    public class BiereServiceTest
    {
        private IBiereRepository _biereRepository;
        private Mock<DbSet<Biere>> _mockBiereDbSet;
        private Mock<IBrasserieContext> _mockDbContext;
        private Mock<DbSet<Brasserie>> _mockBrasserieDbSet;

        public BiereServiceTest()
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
                new Brasserie { Id = 1, Nom = "Test Brasserie"}
            };

            _mockBiereDbSet = MoqHelpers.MockDbSet(bieres);
            _mockBrasserieDbSet = MoqHelpers.MockDbSet(brasseries);

            _mockDbContext = new Mock<IBrasserieContext>();
            _mockDbContext.Setup(c => c.Bieres).Returns(_mockBiereDbSet.Object);
            _mockDbContext.Setup(c => c.Brasseries).Returns(_mockBrasserieDbSet.Object);

            _biereRepository = new BiereRepository(_mockDbContext.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnAllBieres()
        {

            // Act
            var result = _biereRepository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetById_ShouldReturnCorrectBiere()
        {
            // Arrange
            int id = 1;


            // Act
            var result = _biereRepository.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void GetById_InexistanteBiere()
        {
            // Arrange
            int id = 10;

            // Act & Assert
            Assert.ThrowsAsync<BiereInexistantException>(() => _biereRepository.GetById(id));
        }

        [Fact]
        public void AddBiere_WhenBrasserieExists_AddsBiere()
        {
            // Arrange            
            var newBiere = new CreateBiereCommand
            {
                Nom = "TestBiere",
                BrasserieId = 1,
                Prix = 10.0M,
                DegreAlcool = 10F
            };

            // Act
            var result = _biereRepository.Add(newBiere);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Nom, newBiere.Nom);
        }

        [Fact]
        public void AddBiere_CommandNull_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<CommandeVideException>(() => _biereRepository.Add(null));
        }

        [Fact]
        public void AddBiere_WithoutNom_ThrowsException()
        {
            // Arrange
            var newBiere = new CreateBiereCommand
            {
                BrasserieId = 1,
                Prix = 10.0M,
                DegreAlcool = 10F
            };

            // Act & Assert
            Assert.Throws<BiereNomVideException>(() => _biereRepository.Add(newBiere));
        }

        [Fact]
        public void AddBiere_NegativePrix_ThrowsException()
        {
            // Arrange
            var newBiere = new CreateBiereCommand
            {
                Nom = "Biere prix negatif",
                BrasserieId = 1,
                Prix = -10.0M,
                DegreAlcool = 10F
            };

            // Act & Assert
            Assert.Throws<BierePrixException>(() => _biereRepository.Add(newBiere));
        }

        [Fact]
        public void AddBiere_WithNonExistentBrasserie_ThrowsException()
        {
            // Arrange
            var newBiere = new CreateBiereCommand
            {
                Nom = "Biere sans brasserie",
                BrasserieId = 10,
                Prix = 10.0M,
                DegreAlcool = 10F
            };

            // Act & Assert
            Assert.Throws<BrasserieInexistantException>(() => _biereRepository.Add(newBiere));
        }

        [Fact]
        public void Delete_ThrowsException()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _biereRepository.Delete(id);

            // Assert
            _mockBiereDbSet.Verify(m => m.Remove(result), Times.Once);
        }

        [Fact]
        public void Delete_BiereInexistanteThrowsException()
        {
            // Arrange
            int id = 10;

            // Act & Assert
            Assert.Throws<BiereInexistantException>(() => _biereRepository.Delete(id));
        }

    }
}
