import React, { useEffect, useState } from 'react';
import './index.css'
import { Col, Row, Space, Table, Tooltip } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import CriarPessoaModal from '../../components/modals/pessoas/criar-pessoa-modal';
import AtualizarPessoaModal from '../../components/modals/pessoas/atualizar-pessoa-modal';
import { PessoaResponse, ExcluirPessoa, ListarPessoas } from '../../services/pessoas/api';
import { toast } from 'react-toastify';

const Pessoas: React.FC = () => {

    const [AtualizarPessoaModalVisible, setAtualizarPessoaModalVisible] = useState<boolean>(false);
    const [pessoaAtualizarId, setPessoaAtualizarId] = useState<number>(0);
    const [filtro, setFiltro] = useState<string>();
    const [pessoas, setPessoas] = useState<PessoaResponse[]>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        ListarPessoas().then(res => {
            setIsLoading(true);
            setPessoas(res.pessoas)
            setIsLoading(false);
        })
    }, [isLoading])

    const columns: ColumnsType<PessoaResponse> = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
        },
        {
            title: 'Nome',
            dataIndex: 'nome',
        },
        {
            title: 'CPF',
            dataIndex: 'cpf',
        },
        {
            title: 'Ações',
            dataIndex: 'acao',
            render: (_, { id }) => (
                <Space size="middle">
                    <Tooltip title="Editar">
                        <EditOutlined disabled={isLoading} onClick={() => {
                            setPessoaAtualizarId(id);
                            setAtualizarPessoaModalVisible(true)
                        }}
                        />
                    </Tooltip>

                    <Tooltip title="Excluir">
                        <DeleteOutlined
                            disabled={isLoading}
                            className='actions'
                            style={{ color: 'red' }}
                            onClick={() => {
                                ExcluirPessoa(id).then(() => {
                                    toast.success(`Pessoa com Id: ${id} excluida com sucesso`)
                                    setIsLoading(true);
                                });
                            }}
                        />
                    </Tooltip>
                </Space>
            )
        },
    ];

    return (
        <>
            <AtualizarPessoaModal
                isVisible={AtualizarPessoaModalVisible}
                setVisableFalse={() => setAtualizarPessoaModalVisible(false)}
                atualizar={() => setIsLoading(true)}
                id={pessoaAtualizarId}
            />
            <Row justify='start'>
                <Col>
                    <Title>Pessoa</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search
                        placeholder="Buscar Id, Nome ou CPF"
                        enterButton="Pesquisar"
                        onSearch={(value) => setFiltro(value.toLocaleLowerCase())}
                    />
                </Col>
                <Col>
                    <CriarPessoaModal atualizar={() => setIsLoading(true)} />
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table
                        columns={columns}
                        dataSource={pessoas?.filter(pessoa => `${pessoa.id} ${pessoa.nome} ${pessoa.cpf}`.toLocaleLowerCase().includes(filtro || ''))}
                        scroll={{ x: 800, y: 450 }}
                        loading={isLoading}
                        size='small'
                    />
                </Col>
            </Row>
        </>
    )
};

export default Pessoas;