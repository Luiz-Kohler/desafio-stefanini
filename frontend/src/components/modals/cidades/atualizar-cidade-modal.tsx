import React, { useEffect, useState } from 'react';
import { Button, Form, Input, Modal } from 'antd';
import { toast } from 'react-toastify';
import { AtualizarCidade, AtualizarCidadeRequest, BuscarCidade } from '../../../services/cidades/api';

type AtualizarCidadeModalProps = {
    isVisible: boolean;
    setVisableFalse: any;
    id: number;
    atualizar: any
}
const AtualizarCidadeModal: React.FC<AtualizarCidadeModalProps> = ({ isVisible, setVisableFalse, atualizar, id }) => {
    const [form] = Form.useForm();
    const [cidade, setCidade] = useState<AtualizarCidadeRequest>({
        id: id,
        nome: '',
        uf: ''
    });

    useEffect(() => {
        if (id !== 0) {
            BuscarCidade(id).then(res => {
                setCidade({ id: id, nome: res.nome, uf: res.uf });
                form.setFieldsValue({ id: id, nome: res.nome, uf: res.uf });
            })
        }
    }, [id])

    const handleOk = () => {
        AtualizarCidade(cidade).then((res) => {
            if (res.status === 200) {
                atualizar();
                setVisableFalse(false);
                toast.success("Cidade atualizada com sucesso!")
            }
        })
    };

    const handleCancel = () => {
        setVisableFalse();
    };

    return (
        <>
            <Modal title="Atualização de Cidade" visible={isVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="atualizar-cidade-form" key="submit" htmlType="submit">
                    Atualizar
                </Button>
            ]}>
                <Form
                    id="atualizar-cidade-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Nome" name="nome" rules={[{ required: true, min: 2, max: 200, message: 'Informe um nome entre 2 a 200 caracteres' }]}>
                        <Input placeholder="Nome da cidade" value={cidade?.nome} onChange={(e) => setCidade({ ...cidade, nome: e.target.value })} />
                    </Form.Item>
                    <Form.Item label="UF" name="uf" rules={[{ required: true, len: 2, message: 'Informe a UF valida' }]}>
                        <Input placeholder="UF da cidade" value={cidade?.uf} onChange={(e) => setCidade({ ...cidade, uf: e.target.value })} maxLength={2} />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default AtualizarCidadeModal;