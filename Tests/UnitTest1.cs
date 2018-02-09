using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using XYApp.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class UnitTest1
    {
        #region Testes CRUD
        [Fact(DisplayName = "Nova instância válida")]
        [Trait("Category", "Testes de POI")]
        public void POI_NovaInstancia_DeveRetornarSucesso()
        {
            var poi = GerarPOIValido();
            Assert.True(poi != null);
        }

        [Fact(DisplayName = "Nova instância com nome inválido")]
        [Trait("Category", "Testes de POI")]
        public void POI_NovaInstanciaNomeInvalido_DeveRetornarComErro()
        {
            var exception = Assert.Throws<Exception>(() => new POI { NomePOI = "", PntX = 40, PntY = 50 });
            Assert.Equal("Nome Inválido! Não é possível criar instância de POI", exception.Message);
        }

        [Fact(DisplayName = "Obter POI do banco")]
        [Trait("Category", "Testes de POI")]
        public void POI_ObterPOIBanco_DeveRetornarComSucesso()
        {
            var db = new XYApp.Data.POIContexto();
            var poi = db.POIs.FirstOrDefault();

            Assert.NotNull(poi);
        }

        [Fact(DisplayName = "Atualizar Nome POI")]
        [Trait("Category", "Testes de POI")]
        public void POI_AtualizarNomePOI_DeletarPOI_DeveRetornarSucesso()
        {
            POI poi;
            using (var db = new XYApp.Data.POIContexto())
            {
                poi = db.POIs.Add(GerarPOIValido()).Entity;
                db.SaveChanges();
            }

            POI poiAlterado;

            // Simulando novo request (e evitando o problema de tracking)
            using (var db = new XYApp.Data.POIContexto())
            {
                poi.NomePOI = "Novo Nome POI";
                poiAlterado = poi;

                db.POIs.Update(poiAlterado);
                db.SaveChanges();
            }

            var dbfinal = new XYApp.Data.POIContexto();
            poiAlterado = dbfinal.POIs.Find(poi.ID);

            Assert.NotNull(poiAlterado);

            dbfinal.Remove(poiAlterado);
            dbfinal.SaveChanges();

            poiAlterado = dbfinal.POIs.Find(poi.ID);

            Assert.Null(poiAlterado);
        }

        private static POI GerarPOIValido()
        {
            var poi = new Faker<POI>("pt_BR")
               .CustomInstantiator(f => new POI
               {
                   NomePOI = f.Name.Random.ToString(),
                   PntX = 10,
                   PntY = 20
               }
              );

            return poi;
        }
        #endregion

        #region Testes Servicos
        [Fact(DisplayName = "Servico Listar")]
        [Trait("Category", "Testes de Servico")]
        public async Task POI_ListaServico_DeveRetornarSucesso()
        {
            // Arrange
            var dbfinal = new XYApp.Data.POIContexto();
            var controller = new XYApp.Controllers.POIsController(dbfinal);

            // Act
            IActionResult actionResult = await controller.Index("", null, null, null);

            Assert.NotNull(actionResult);

            ViewResult result = actionResult as ViewResult;

            Assert.NotNull(result);

            List<POI> pois = result.ViewData.Model as List<POI>;

            Assert.True(pois.Count > 0);
        }

        [Fact(DisplayName = "Servico Detalhes")]
        [Trait("Category", "Testes de Servico")]
        public async Task POI_DetalheServico_DeveRetornarSucesso()
        {
            // Arrange
            var dbfinal = new XYApp.Data.POIContexto();
            var controller = new XYApp.Controllers.POIsController(dbfinal);

            // Act
            IActionResult actionResult = await controller.Details(5);

            Assert.NotNull(actionResult);

            ViewResult result = actionResult as ViewResult;

            Assert.NotNull(result);

            POI poi = result.ViewData.Model as POI;

            Assert.True(poi.ID == 5);
        }
        #endregion
    }
}
