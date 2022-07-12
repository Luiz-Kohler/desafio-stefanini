using Application.Services.Cidades.Buscar;
using Application.Services.Cidades.Criar;
using Application.Services.Cidades.DTOs;
using Application.Services.Cidades.Listar;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class CidadesMap : Profile
    {
        public CidadesMap()
        {
            CreateMap<CriarCidadeRequest, Cidade>()
                .ConstructUsing(request => new Cidade(request.Nome, request.Uf));

            CreateMap<Cidade, BuscarCidadeResponse>();

            CreateMap<Cidade, CidadeResponse>();

            CreateMap<IList<Cidade>, ListarCidadesResponse>()
                .ForMember(r => r.Cidades, opt => opt.MapFrom(cidades => cidades));
        }
    }
}
