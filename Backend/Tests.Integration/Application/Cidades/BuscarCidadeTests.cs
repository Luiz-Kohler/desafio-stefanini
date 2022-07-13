using Application.Services.Cidades.Buscar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace Tests.Integration.Application.Cidades
{
    public class BuscarCidadeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_buscar_cidade()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var request = Builder<BuscarCidadeRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            var response = await Handle<BuscarCidadeRequest, BuscarCidadeResponse>(request);

            response.Id.Should().Be(cidadeInserir.Id);
            response.Nome.Should().Be(cidadeInserir.Nome);
            response.UF.Should().Be(cidadeInserir.UF);
        }
    }
}
