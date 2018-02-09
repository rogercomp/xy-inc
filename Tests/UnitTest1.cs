using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using XYApp.Models;
using Bogus;

namespace Tests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Nova inst�ncia v�lida")]
        [Trait("Category", "Testes de POI")]
        public void POI_NovaInstancia_DeveRetornarSucesso()
        {
            var poi = GerarPOIValido();
            Assert.True(poi != null);
        }

        [Fact(DisplayName = "Nova inst�ncia com nome inv�lido")]
        [Trait("Category", "Testes de POI")]
        public void POI_NovaInstanciaNomeInvalido_DeveRetornarComErro()
        {
            var exception = Assert.Throws<Exception>(() => new POI { NomePOI = "", PntX = 40, PntY = 50 });
            Assert.Equal("Nome Inv�lido! N�o � poss�vel criar inst�ncia de POI", exception.Message);
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
    }
}
