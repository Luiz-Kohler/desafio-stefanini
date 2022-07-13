using Application.Services.Pessoas.Criar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Pessoas
{
    public class CriarPessoaTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_criar_pessoa()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var request = Builder<CriarPessoaRequest>
                .CreateNew()
                .With(c => c.Nome, faker.Name.FullName())
                .With(c => c.Cpf, CpfUtils.GerarCpf())
                .With(c => c.CidadeId, (int)cidadeInserir.Id)
                .Build();

            await Handle<CriarPessoaRequest, CriarPessoaResponse>(request);

            var pessoasDb = GetEntities<Pessoa>();
            pessoasDb.Should().HaveCount(1);

            var pessoa = pessoasDb.First();
            pessoa.Id.Should().Be(1);
            pessoa.Nome.Should().Be(request.Nome);
            pessoa.Cpf.Should().Be(request.Cpf);
            pessoa.CidadeId.Should().Be(cidadeInserir.Id);
        }
    }
}
