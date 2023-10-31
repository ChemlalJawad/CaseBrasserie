using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Application.Repositories.Implementations;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Test
{
    public class GrossisteServiceTest
    {
        private IGrossisteRepository _grossisteRepository;
        private Mock<DbSet<Biere>> _mockBiereDbSet;
        private Mock<DbSet<Grossiste>> _mockGrossiteDbSet;
        private Mock<IBrasserieContext> _mockDbContext;
        private Mock<DbSet<GrossisteBiere>> _mockGrossisteDbSet;

        public GrossisteServiceTest()
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

            var grossisteBieres = new List<GrossisteBiere> { new GrossisteBiere { BiereId = 3, GrossisteId = 1, Stock = 500 } };
            var grossistes = new List<Grossiste>
            {
                new Grossiste { Id = 1, Nom = "Test Grossiste 1", GrossistesBieres = grossisteBieres  },
                new Grossiste { Id = 2, Nom = "Test Grossiste 2"  },
            };


            _mockBiereDbSet = MoqHelpers.MockDbSet(bieres);
            _mockGrossisteDbSet = MoqHelpers.MockDbSet(grossisteBieres);
            _mockGrossiteDbSet = MoqHelpers.MockDbSet(grossistes);

            _mockDbContext = new Mock<IBrasserieContext>();
            _mockDbContext.Setup(c => c.Bieres).Returns(_mockBiereDbSet.Object);
            _mockDbContext.Setup(c => c.GrossistesBieres).Returns(_mockGrossisteDbSet.Object);
            _mockDbContext.Setup(c => c.Grossistes).Returns(_mockGrossiteDbSet.Object);

            _grossisteRepository = new GrossisteRepository(_mockDbContext.Object);
        }

        [Fact]
        public void AddNewBiereToGrossiste_ValidCommand_BiereIsAdded()
        {
            // Arrange  
            var command = new AddNewBiereCommand
            {
                BiereId = 1,
                GrossisteId = 1,
                Stock = 100
            };

            // Act
            var result = _grossisteRepository.AddNewBiereToGrosssite(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.GrossisteId, command.GrossisteId);
            Assert.Equal(result.BiereId, command.BiereId);
        }

        [Fact]
        public void AddNewBiereToGrossiste_InvalidCommandException()
        {

            // Act & Assert
            Assert.Throws<CommandeVideException>(() => _grossisteRepository.AddNewBiereToGrosssite(null));
        }

        [Fact]
        public void AddNewBiereToGrossiste_StockInsuffisantException()
        {
            // Arrange  
            var command = new AddNewBiereCommand
            {
                BiereId = 1,
                GrossisteId = 1,
                Stock = -1
            };

            // Act & Assert
            Assert.Throws<StockInsuffisantException>(() => _grossisteRepository.AddNewBiereToGrosssite(command));
        }

        [Fact]
        public void AddNewBiereToGrossiste_BiereInexistanteException()
        {
            // Arrange  
            var command = new AddNewBiereCommand
            {
                BiereId = 10,
                GrossisteId = 1,
                Stock = 10
            };

            // Act & Assert
            Assert.Throws<BiereInexistantException>(() => _grossisteRepository.AddNewBiereToGrosssite(command));
        }

        [Fact]
        public void AddNewBiereToGrossiste_GrossisteInexistanteException()
        {
            // Arrange  
            var command = new AddNewBiereCommand
            {
                BiereId = 1,
                GrossisteId = 100,
                Stock = 10
            };

            // Act & Assert
            Assert.Throws<GrossisteInexistantException>(() => _grossisteRepository.AddNewBiereToGrosssite(command));
        }

        [Fact]
        public void AddNewBiereToGrossiste_DuplicateException()
        {
            // Arrange  
            var command = new AddNewBiereCommand
            {
                BiereId = 3,
                GrossisteId = 1,
                Stock = 10
            };

            // Act & Assert
            Assert.Throws<BiereDejaVendueParGrossisteException>(() => _grossisteRepository.AddNewBiereToGrosssite(command));
        }

        [Fact]
        public void UpdateGrossisteBiere_ValidCommand_StockIsUpdated()
        {
            // Arrange  
            var command = new UpdateStockCommand
            {
                BiereId = 3,
                GrossisteId = 1,
                Stock = 500
            };

            // Act
            var result = _grossisteRepository.UpdateGrossisteBiere(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.Stock);
            Assert.Equal(result.BiereId, command.BiereId);
            Assert.Equal(result.GrossisteId, command.GrossisteId);
        }

        [Fact]
        public void UpdateGrossisteBiere_InvalidCommandException()
        {
            // Act & Assert
            Assert.Throws<CommandeVideException>(() => _grossisteRepository.UpdateGrossisteBiere(null));
        }

        [Fact]
        public void UpdateGrossisteBiere_GrossisteBiereException()
        {
            // Arrange  
            var command = new UpdateStockCommand
            {
                BiereId = 100,
                GrossisteId = 100,
                Stock = 500
            };

            // Act & Assert
            Assert.Throws<GrossisteBiereException>(() => _grossisteRepository.UpdateGrossisteBiere(command));
        }

        [Fact]
        public void UpdateGrossisteBiere_StockeException()
        {
            // Arrange  
            var command = new UpdateStockCommand
            {
                BiereId = 3,
                GrossisteId = 1,
                Stock = -500
            };

            // Act & Assert
            Assert.Throws<StockModificationException>(() => _grossisteRepository.UpdateGrossisteBiere(command));
        }

        [Fact]
        public void GetQuotation_ValidCommand_ReturnsExpectedQuotation()
        {
            // Arrange  
            var command = new QuotationCommand
            {
                GrossisteId = 1,
                PrixTotal = 0,
                Items = new List<ItemCommand> { new ItemCommand { BiereId = 3, Quantite = 10 } }
            };

            // Act
            _mockDbContext(dbMemory.Object);
            var result = _grossisteRepository.GetQuotation(command);

            // Assert
            Assert.Equal(500, result);

        }

    }
}
