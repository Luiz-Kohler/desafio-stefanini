namespace Application.Services.Pessoas.DTOs
{
    public class PessoaResponse
    {
        public uint Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public CidadeForPessoaDTO Cidade { get; set; }
    }

    public class CidadeForPessoaDTO
    {
        public uint Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
    }
}
