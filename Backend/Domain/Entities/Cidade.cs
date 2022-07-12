using Domain.Common;

namespace Domain.Entities
{
    public class Cidade : BaseEntity
    {
        public string Nome { get; private set; }
        public string UF { get; private set; }
        public virtual ICollection<Pessoa> Pessoas { get; }

        public Cidade(string nome, string uF)
        {
            Nome = nome;
            UF = uF;
        }

        public void Atualizar(string nome, string uf)
        {
            Nome = nome;
            UF = uf;
            AtualizarEntidadeBase();
        }
    }
}
