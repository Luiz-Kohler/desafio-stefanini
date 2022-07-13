using Bogus;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Tests.Unit.Entities
{
    public class CidadeTests
    {
        private readonly Faker _faker;

        public CidadeTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Deve_criar_cidade()
        {
            var nome = _faker.Name.FirstName();
            var uf = "SC";

            var cidade = new Cidade(nome, uf);

            cidade.Nome.Should().Be(nome);
            cidade.UF.Should().Be(uf);
        }
    }
}
