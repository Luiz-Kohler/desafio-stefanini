using Application.Services.Cidades.Excluir;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace Tests.Integration.Application.Cidades
{
    public class ExcluirCidadeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_excluir_cidade()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var request = Builder<ExcluirCidadeRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            await Handle<ExcluirCidadeRequest, ExcluirCidadeResponse>(request);

            var cidadesAtivasDb = GetEntities<Cidade>().Where(cidade => cidade.EhAtivo);
            cidadesAtivasDb.Should().HaveCount(0);
        }
    }
}
