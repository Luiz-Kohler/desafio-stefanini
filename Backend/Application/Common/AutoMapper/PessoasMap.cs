using Application.Services.Pessoas.Buscar;
using Application.Services.Pessoas.Criar;
using Application.Services.Pessoas.DTOs;
using Application.Services.Pessoas.Listar;
using AutoMapper;
using Domain.Common.Extensios;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class PessoasMap : Profile
    {
        public PessoasMap()
        {
            CreateMap<CriarPessoaRequest, Pessoa>()
                .ConstructUsing(request => new Pessoa(request.Nome, request.Cpf.FormatCpf(), request.DataNascimento, (uint)request.CidadeId));

            CreateMap<Cidade, CidadeForPessoaDTO>();

            CreateMap<Pessoa, BuscarPessoaResponse>()
                .ForMember(p => p.Cidade, opt => opt.MapFrom(r => r.Cidade));

            CreateMap<Pessoa, PessoaResponse>();

            CreateMap<IList<Pessoa>, ListarPessoasResponse>()
                .ForMember(r => r.Pessoas, opt => opt.MapFrom(pessoas => pessoas));
        }
    }
}
