using Application.Services.Pessoas.Atualizar;
using Bogus;
using Domain.Entities;
using FizzWare.NBuilder;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Integration.Application.Pessoas
{
    public class AtualizaPessoaTests : ApplicationTestBase
    {
        [Fact]
        public async Task Deve_atualizar_pessoa()
        {
            var faker = new Faker();

            var cidadeInserir = new Cidade(faker.Random.Words(), "SC");
            InsertOne(cidadeInserir);

            var pessoaInserir = new Pessoa(faker.Random.Words(), CpfUtils.GerarCpf(), DateTime.UtcNow, cidadeInserir.Id);
            InsertOne(pessoaInserir);

            var request = Builder<AtualizarPessoaRequest>
                .CreateNew()
                .With(p => p.Id, 1)
                .With(p => p.Nome, faker.Name.FullName())
                .With(p => p.CidadeId, (int)cidadeInserir.Id)
                .With(p => p.DataNascimento, DateTime.UtcNow)
                .Build();

            await Handle<AtualizarPessoaRequest, AtualizarPessoaResponse>(request);

            var pessoasDb = GetEntities<Pessoa>();
            pessoasDb.Should().HaveCount(1);

            var pessoa = pessoasDb.First();
            pessoa.Id.Should().Be((uint)request.Id);
            pessoa.Nome.Should().Be(request.Nome);
            pessoa.Cpf.Should().Be(pessoaInserir.Cpf);
            pessoa.CidadeId.Should().Be(cidadeInserir.Id);
        }
    }
}
