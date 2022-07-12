using Domain.Common;

namespace Domain.Entities
{
    public class Pessoa : BaseEntity
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public uint CidadeId { get; private set; }
        public virtual Cidade Cidade { get; private set; }

        public Pessoa(string nome, string cpf, DateTime dataNascimento, uint cidadeId)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento.ToUniversalTime();
            CidadeId = cidadeId;
        }

        public void Atualizar(string nome, DateTime dataNascimento, Cidade cidade)
        {
            Nome = nome;
            DataNascimento = dataNascimento.ToUniversalTime();
            CidadeId = cidade.Id;
            Cidade = cidade;
        }
    }
}
