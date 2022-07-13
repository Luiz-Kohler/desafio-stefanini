import api from '../api';

const CONTROLLER = 'pessoas';

export type CidadeForPessoa = {
    id: number;
    nome: string;
    uf: string;
};

export type PessoaResponse = {
    id: number;
    nome: string;
    cpf: string;
    dataNascimento: Date;
    cidade: CidadeForPessoa;
};

export type BuscarPessoaResponse = PessoaResponse;

export const BuscarPessoa = async (id : number) : Promise<BuscarPessoaResponse> => {
    return api.get<BuscarPessoaResponse>(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}

export type ListarPessoasResponse = {
    pessoas: PessoaResponse[];
}

export const ListarPessoas = async () : Promise<ListarPessoasResponse>=> {
    return api.get<ListarPessoasResponse>(`${CONTROLLER}`)
    .then(res => res.data)
}

export type CriarPessoaRequest = {
    nome: string;
    cpf: string;
    dataNascimento: Date;
    cidadeId: number;
}

export const CriarPessoa = async (request : CriarPessoaRequest) => {
    return api.post(`${CONTROLLER}`, request).then(res => res);
}

export type AtualizarPessoaRequest = {
    id: number,
    nome: string;
    dataNascimento: Date;
    cidadeId: number;
}

export const AtualizarPessoa = async (request : AtualizarPessoaRequest) => {
    return api.put(`${CONTROLLER}`, request).then(res => res);
}

export const ExcluirPessoa = async (id : number) => {
    return api.delete(`${CONTROLLER}/${id}`)
    .then(res => res.data)
}