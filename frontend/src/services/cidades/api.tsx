import api from '../api';

const CONTROLLER = 'cidades';

export type CidadeResponse = {
    id: number;
    nome: string;
    uf: string;
};

export type BuscarCidadeResponse = CidadeResponse;

export const BuscarCidade = async (id : number) : Promise<BuscarCidadeResponse> => {
    return api.get<BuscarCidadeResponse>(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}

export type ListarCidadesResponse = {
    cidades: CidadeResponse[];
}

export const ListarCidades = async () : Promise<ListarCidadesResponse>=> {
    return api.get<ListarCidadesResponse>(`${CONTROLLER}`)
    .then(res => res.data)
}

export type CriarCidadeRequest = {
    nome: string,
    uf: string,
}

export const CriarCidade = async (request : CriarCidadeRequest) => {
    return api.post(`${CONTROLLER}`, request).then(res => res);
}

export type AtualizarCidadeRequest = {
    id: number,
    nome: string,
    uf: string,
}

export const AtualizarCidade = async (request : AtualizarCidadeRequest) => {
    return api.put(`${CONTROLLER}`, request).then(res => res);
}

export const ExcluirCidade = async (id : number) => {
    return api.delete(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}