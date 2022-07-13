import React, { useState } from 'react';
import { Button, Form, Input, Modal } from 'antd';
import { CriarCidade, CriarCidadeRequest } from '../../../services/cidades/api';
import { toast } from 'react-toastify';

type CriarCidadeModalProps = {
    atualizar: any
}

const CriarCidadeModal: React.FC<CriarCidadeModalProps> = ({ atualizar }) => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();

    const [cidade, setCidade] = useState<CriarCidadeRequest>({
        nome: "",
        uf: "",
    });

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        CriarCidade(cidade).then((res) => {
            if (res.status === 200) {
                setCidade({ nome: '', uf: '' })
                form.resetFields();
                toast.success("Cidade cadastrada com sucesso!")
                setIsModalVisible(false);
                atualizar();
            }
        })
    };

    const handleCancel = () => {
        setCidade({ nome: '', uf: '' })
        form.resetFields();
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                Cadastrar
            </Button>
            <Modal destroyOnClose title="Cadastro de Cidade" visible={isModalVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="criar-cidade-form" key="submit" htmlType="submit">
                    Cadastrar
                </Button>
            ]}>
                <Form
                    id="criar-cidade-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Nome" name="nome" rules={[{ required: true, min:2, max: 200, message: 'Informe um nome entre 2 a 200 caracteres' }]}>
                        <Input placeholder="Nome da cidade" value={cidade?.nome} onChange={(e) => setCidade({ ...cidade, nome: e.target.value })} maxLength={200}/>
                    </Form.Item>
                    <Form.Item label="UF" name="uf" rules={[{ required: true, len: 2, message: 'Informe a UF valida' }]}>
                        <Input placeholder="UF da cidade" value={cidade?.uf} onChange={(e) => setCidade({ ...cidade, uf: e.target.value })} maxLength={2} />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default CriarCidadeModal;