using Application.Services.Pessoas.Excluir;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Pessoas
{
    public class ExcluirPessoaTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_excluir_pessoa()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var pessoaInserir = new Pessoa(faker.Random.Words(), CpfUtils.GerarCpf(), DateTime.UtcNow, cidadeInserir.Id);
            InsertOne(pessoaInserir);

            var request = Builder<ExcluirPessoaRequest>
                .CreateNew()
                .With(c => c.Id, 1)
                .Build();

            await Handle<ExcluirPessoaRequest, ExcluirPessoaResponse>(request);

            var pessoasAtivasDb = GetEntities<Pessoa>().Where(pessoa => pessoa.EhAtivo);
            pessoasAtivasDb.Should().HaveCount(0);
        }
    }
}
