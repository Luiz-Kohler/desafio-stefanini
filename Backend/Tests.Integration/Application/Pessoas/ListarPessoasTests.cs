using Application.Services.Pessoas.Listar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Pessoas
{
    public class ListarPessoasTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_listar_pessoas()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var pessoaInserir = new Pessoa(faker.Random.Words(), CpfUtils.GerarCpf(), DateTime.UtcNow, cidadeInserir.Id);
            InsertOne(pessoaInserir);

            var request = Builder<ListarPessoasRequest>
                .CreateNew()
                .Build();

            var response = await Handle<ListarPessoasRequest, ListarPessoasResponse>(request);

            response.Pessoas.Should().HaveCount(1);

            response.Pessoas.First().Id.Should().Be(pessoaInserir.Id);
            response.Pessoas.First().Nome.Should().Be(pessoaInserir.Nome);
            response.Pessoas.First().Cpf.Should().Be(pessoaInserir.Cpf);
            response.Pessoas.First().Cidade.Id.Should().Be(cidadeInserir.Id);
            response.Pessoas.First().Cidade.Nome.Should().Be(cidadeInserir.Nome);
        }
    }
}
