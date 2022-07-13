import React, { useEffect, useState } from 'react';
import { Button, DatePicker, Form, Input, Modal, Select } from 'antd';
import { CriarPessoa, CriarPessoaRequest } from '../../../services/pessoas/api';
import { toast } from 'react-toastify';
import moment from 'moment';
import { CidadeResponse, ListarCidades } from '../../../services/cidades/api';
import { MaskedInput } from 'antd-mask-input';
const { Option } = Select;

type CriarPessoaModalProps = {
    atualizar: any
}

const CriarPessoaModal: React.FC<CriarPessoaModalProps> = ({ atualizar }) => {
    const [isLoading, setIsLoading] = useState(false);
    const [cidades, setCidades] = useState<CidadeResponse[]>([]);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();
    const [pessoa, setPessoa] = useState<CriarPessoaRequest>({
        nome: '',
        cpf: '',
        dataNascimento: new Date(),
        cidadeId: 0
    });

    useEffect(() => {
        setIsLoading(true);

        ListarCidades().then(res => {
            setCidades(res.cidades)
        });

        setIsLoading(false);
    }, [isModalVisible])

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        CriarPessoa(pessoa).then((res) => {
            if (res.status === 200) {
                setPessoa({ nome: '', cpf: '', dataNascimento: new Date(), cidadeId: 0 })
                form.resetFields();
                toast.success("Pessoa cadastrada com sucesso!")
                setIsModalVisible(false);
                atualizar();
            }
        })
    };

    const handleCancel = () => {
        setPessoa({ nome: '', cpf: '', dataNascimento: new Date(), cidadeId: 0 })
        form.resetFields();
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                Cadastrar
            </Button>
            <Modal destroyOnClose title="Cadastro de Pessoa" visible={isModalVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="criar-pessoa-form" key="submit" htmlType="submit">
                    Cadastrar
                </Button>
            ]}>
                <Form
                    id="criar-pessoa-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Nome" name="nome" rules={[{ required: true, min:2, max: 200, message: 'Informe um nome entre 2 a 200 caracteres' }]}>
                        <Input placeholder="Nome da pessoa" value={pessoa?.nome} onChange={(e) => setPessoa({ ...pessoa, nome: e.target.value })} />
                    </Form.Item>
                    <Form.Item label="CPF" name="cpf" rules={[{ required: true, len:11, message: 'Informe o CPF valido' }]}>
                        <Input placeholder="CPF da pessoa (sem mascara)" value={pessoa?.cpf}  onChange={(e) => setPessoa({ ...pessoa, cpf: e.target.value })} maxLength={11} />
                    </Form.Item>
                    <Form.Item label="Data De Nascimento" name="dataNascimento" rules={[{ required: true, message: 'Informe a Data de Nascimento' }]}>
                        <DatePicker
                            value={moment(pessoa.dataNascimento)}
                            format={'DD/MM/YYYY'}
                            onChange={(e) => setPessoa({ ...pessoa, dataNascimento: e?.toDate() || new Date() })}
                        />
                    </Form.Item>
                    <Form.Item label="Cidade" name="cidadeId" rules={[{ required: true, message: 'Informe a cidade de onde é a pessoa' }]}>
                        <Select onChange={(value) => setPessoa({ ...pessoa, cidadeId: parseInt(value) })} loading={isLoading}>
                            {cidades.map(cidade => (<Option value={`${cidade.id}`}>{`${cidade.uf} - ${cidade.nome}`}</Option>))}
                        </Select>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default CriarPessoaModal;