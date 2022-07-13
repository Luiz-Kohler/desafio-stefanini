using Bogus;
using Domain.Entities;
using FluentAssertions;
using Tests.Common.Helpers;
using Xunit;

namespace Tests.Unit.Entities
{
    public class PessoaTests
    {
        private readonly Faker _faker;

        public PessoaTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Deve_criar_pessoa()
        {
            var nome = _faker.Name.FirstName();
            var cpf = CpfUtils.GerarCpf();
            var dataNascimento = DateTime.MinValue.ToUniversalTime();
            var cidadeId = uint.MinValue;

            var pessoa = new Pessoa(nome, cpf, dataNascimento, cidadeId);

            pessoa.Nome.Should().Be(nome);
            pessoa.Cpf.Should().Be(cpf);
            pessoa.DataNascimento.Should().Be(dataNascimento);
            pessoa.CidadeId.Should().Be(cidadeId);
        }
    }
}
