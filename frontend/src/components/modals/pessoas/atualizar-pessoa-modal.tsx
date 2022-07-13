import React, { useEffect, useState } from 'react';
import { Button, DatePicker, Form, Input, Modal, Select } from 'antd';
import { toast } from 'react-toastify';
import moment from 'moment';
import { AtualizarPessoa, AtualizarPessoaRequest, BuscarPessoa } from '../../../services/pessoas/api';
import { CidadeResponse, ListarCidades } from '../../../services/cidades/api';
const { Option } = Select;

type AtualizarPessoaModalProps = {
    isVisible: boolean;
    setVisableFalse: any;
    id: number;
    atualizar: any
}
const AtualizarPessoaModal: React.FC<AtualizarPessoaModalProps> = ({ isVisible, setVisableFalse, atualizar, id }) => {
    const [form] = Form.useForm();
    const [isLoading, setIsLoading] = useState(false);
    const [cidades, setCidades] = useState<CidadeResponse[]>([]);
    const [pessoa, setPessoa] = useState<AtualizarPessoaRequest>({
        id: id,
        nome: '',
        dataNascimento: new Date(),
        cidadeId: 0
    });

    useEffect(() => {
        if (id !== 0) {
            BuscarPessoa(id).then(res => {
                setPessoa({ id: id, dataNascimento: res.dataNascimento, nome: res.nome, cidadeId: res.cidade.id });
                form.setFieldsValue({ id: id, dataNascimento: moment(res.dataNascimento), nome: res.nome, cidadeId: res.cidade.id });
            })

            setIsLoading(true);

            ListarCidades().then(res => {
                setCidades(res.cidades)
            });

            setIsLoading(false);
        }
    }, [id])

    const handleOk = () => {
        AtualizarPessoa(pessoa).then((res) => {
            if (res.status === 200) {
                atualizar();
                setVisableFalse(false);
                toast.success("Pessoa atualizada com sucesso!")
            }
        })
    };

    const handleCancel = () => {
        setVisableFalse();
    };

    return (
        <>
            <Modal title="Atualização de Pessoa" visible={isVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="atualizar-pessoa-form" key="submit" htmlType="submit">
                    Atualizar
                </Button>
            ]}>
                <Form
                    id="atualizar-pessoa-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Nome" name="nome" rules={[{ required: true, min: 2, max: 200, message: 'Informe um nome entre 2 a 200 caracteres' }]}>
                        <Input placeholder="Nome da pessoa" value={pessoa?.nome} onChange={(e) => setPessoa({ ...pessoa, nome: e.target.value })} />
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

export default AtualizarPessoaModal;