using Application.Services.Cidades.Criar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace Tests.Integration.Application.Cidades
{
    public class CriarCidadeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_criar_cidade()
        {
            var faker = new Faker();

            var request = Builder<CriarCidadeRequest>
                .CreateNew()
                .With(c => c.Nome, faker.Name.FullName())
                .With(c => c.Uf, "SC")
                .Build();

            await Handle<CriarCidadeRequest, CriarCidadeResponse>(request);

            var cidadesDb = GetEntities<Cidade>();
            cidadesDb.Should().HaveCount(1);

            var cidade = cidadesDb.First();
            cidade.Id.Should().BeGreaterThan(0);
            cidade.Nome.Should().Be(request.Nome);
            cidade.UF.Should().Be(request.Uf);
        }
    }
}
