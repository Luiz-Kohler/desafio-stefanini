using Application.Services.Cidades.Atualizar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace Tests.Integration.Application.Cidades
{
    public class AtualizaCidadeTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_atualizar_cidade()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var request = Builder<AtualizarCidadeRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .With(c => c.Nome, faker.Name.FullName())
                .With(c => c.Uf, "RS")
                .Build();

            await Handle<AtualizarCidadeRequest, AtualizarCidadeResponse>(request);

            var cidadesDb = GetEntities<Cidade>();
            cidadesDb.Should().HaveCount(1);

            var cidade = cidadesDb.First();
            cidade.Id.Should().Be((uint)request.Id);
            cidade.Nome.Should().Be(request.Nome);
            cidade.UF.Should().Be(request.Uf);
        }
    }
}
