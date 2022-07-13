using Application.Services.Pessoas.Buscar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Pessoas
{
    public class BuscarPessoaTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_buscar_pessoa()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var pessoaInserir = new Pessoa(faker.Random.Words(), CpfUtils.GerarCpf(), DateTime.UtcNow, cidadeInserir.Id);
            InsertOne(pessoaInserir);

            var request = Builder<BuscarPessoaRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            var response = await Handle<BuscarPessoaRequest, BuscarPessoaResponse>(request);

            response.Id.Should().Be(pessoaInserir.Id);
            response.Nome.Should().Be(pessoaInserir.Nome);
            response.Cpf.Should().Be(pessoaInserir.Cpf);
            response.Cidade.Id.Should().Be(cidadeInserir.Id);
            response.Cidade.Nome.Should().Be(cidadeInserir.Nome);
        }
    }
}
