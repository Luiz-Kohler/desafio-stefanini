import React, { useEffect, useState } from 'react';
import './index.css'
import { Col, Row, Space, Table, Tooltip } from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { EditOutlined, DeleteOutlined } from '@ant-design/icons';
import Search from 'antd/lib/input/Search';
import Title from 'antd/lib/typography/Title';
import CriarCidadeModal from '../../components/modals/cidades/criar-cidade-modal';
import AtualizarCidadeModal from '../../components/modals/cidades/atualizar-cidade-modal';
import { CidadeResponse, ExcluirCidade, ListarCidades } from '../../services/cidades/api';
import { toast } from 'react-toastify';

const Cidades: React.FC = () => {

    const [atualizarCidadeModalVisible, setAtualizarCidadeModalVisible] = useState<boolean>(false);
    const [cidadeAtualizarId, setCidadeAtualizarId] = useState<number>(0);
    const [filtro, setFiltro] = useState<string>();
    const [cidades, setCidades] = useState<CidadeResponse[]>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        ListarCidades().then(res => {
            setIsLoading(true);
            setCidades(res.cidades)
            setIsLoading(false);
        })
    }, [isLoading])

    const columns: ColumnsType<CidadeResponse> = [
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
            title: 'UF',
            dataIndex: 'uf',
        },
        {
            title: 'Ações',
            dataIndex: 'acao',
            render: (_, { id }) => (
                <Space size="middle">
                    <Tooltip title="Editar">
                        <EditOutlined disabled={isLoading} onClick={() => {
                            setCidadeAtualizarId(id);
                            setAtualizarCidadeModalVisible(true)
                        }}
                        />
                    </Tooltip>

                    <Tooltip title="Excluir">
                        <DeleteOutlined
                            disabled={isLoading}
                            className='actions'
                            style={{ color: 'red' }}
                            onClick={() => {
                                ExcluirCidade(id).then(() => {
                                    toast.success(`Cidade com Id: ${id} excluido com sucesso`)
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
            <AtualizarCidadeModal
                isVisible={atualizarCidadeModalVisible}
                setVisableFalse={() => setAtualizarCidadeModalVisible(false)}
                atualizar={() => setIsLoading(true)}
                id={cidadeAtualizarId}
            />
            <Row justify='start'>
                <Col>
                    <Title>Cidades</Title>
                </Col>
            </Row>
            <Row justify='space-between' align='middle' className='gutter-box'>
                <Col>
                    <Search
                        placeholder="Buscar Id, Nome ou UF"
                        enterButton="Pesquisar"
                        onSearch={(value) => setFiltro(value.toLocaleLowerCase())}
                    />
                </Col>
                <Col>
                    <CriarCidadeModal atualizar={() => setIsLoading(true)} />
                </Col>
            </Row>
            <Row justify='center' align='middle' className='gutter-box'>
                <Col span={24}>
                    <Table
                        columns={columns}
                        dataSource={cidades?.filter(cidade => `${cidade.id} ${cidade.nome} ${cidade.uf}`.toLocaleLowerCase().includes(filtro || ''))}
                        scroll={{ x: 800, y: 450 }}
                        loading={isLoading}
                        size='small'
                    />
                </Col>
            </Row>
        </>
    )
};

export default Cidades;