using Application.Services.Cidades.Listar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace Tests.Integration.Application.Cidades
{
    public class ListarCidadesTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_listar_cidades()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var request = Builder<ListarCidadesRequest>
                .CreateNew()
                .Build();

            var response = await Handle<ListarCidadesRequest, ListarCidadesResponse>(request);

            response.Cidades.Should().HaveCount(1);

            response.Cidades.First().Id.Should().Be(cidadeInserir.Id);
            response.Cidades.First().Nome.Should().Be(cidadeInserir.Nome);
            response.Cidades.First().UF.Should().Be(cidadeInserir.UF);
        }
    }
}
