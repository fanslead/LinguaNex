import React, { useState } from 'react';
import { Button, Card, Col, Form, Input, Modal, Row, Select, Space, Table } from 'antd';
import { PageContainer } from '@ant-design/pro-components';
import styles from './style.less';
import { List } from 'antd';
import { useRequest, useParams } from 'umi';
import { PlusOutlined } from '@ant-design/icons';
import { getCulture, getCultureSupportedCultures, postCulture } from '@/services/LinguaNex/culture';
import { getResourcesList } from '@/services/LinguaNex/resources';
import { history, Link, matchPath } from '@umijs/max';
import { ColumnsType } from 'antd/es/table';

const CultureRecouece = () => {
  
  const [openCultureModal, setOpenCreateModal] = useState(false);
  const [createCultureForm] = Form.useForm();
  const [culturesData, setCulturesData] = useState<API.CultureDto[]>();
  const [SupportedCulturesData, setSupportedCulturesData] = useState<API.SupportedCulture[]>();

  const { data, loading } = useRequest(() => {
    fetchSupportedCultures();
    fetcCulturesData()
  });
  
  const showOpenCreateModalModal = () =>{
    setOpenCreateModal(true);
  }
  const hideCreateModalModal = () => {
    setOpenCreateModal(false);
  };
  const createCulture = async (params: any) => {
    
    const match = matchPath({ path: "/CultureRecouece/:id" }, history.location.pathname)
    await postCulture({
      name: params.name,
      projectId: match?.params.id
    })
    createCultureForm.resetFields();
    hideCreateModalModal();
    fetcCulturesData();
  }
  const fetcCulturesData = async () =>{
    var cultures = await getCultures();
    selectCulture(cultures.data[0]);
    setCulturesData([{}].concat(cultures.data))
  }
  const fetchSupportedCultures = async () =>{
    var response = await getCultureSupportedCultures();
    setSupportedCulturesData(response.data)
    return response.data;
  }
  const getCultures = async () =>{
    const match = matchPath({ path: "/CultureRecouece/:id" }, history.location.pathname)
    const { id } = match.params;
    return await getCulture({ProjectId: id})
  }
  const selectCulture = async (culture : API.CultureDto | undefined) => {
    if(culture?.id)
    {
      var currentCulture = SupportedCulturesData?.find(a=>a.name === culture.name)
      if(!currentCulture){
        currentCulture = (await fetchSupportedCultures())?.find(a=>a.name === culture.name)
      }
      setTableTitleData(currentCulture?.displayName + ' ' + currentCulture?.englishName + ' ' + currentCulture?.name)
      fetcResourceData(culture?.id)
    }
  }

  const fetcResourceData = async (cultureId: string | undefined) =>{
    var resource = await getResourcesList({
      CultureId: cultureId
    });
    setResourceData(resource.data);
  }

  const selectOptions = () => {
    var result = SupportedCulturesData?.filter(item => {
      return culturesData?.findIndex(a=>a.name===item.name) === -1
    }).map(item => {
      let value: string = item.name as string
      let label: string = item.displayName + ' ' + item.englishName
      return {
        value,
        label
      }
    })
    return result;
  }

  const onChange = (value: string) => {
    console.log(`selected ${value}`);
  };
  
  const onSearch = (value: string) => {
    console.log('search:', value);
  };
  // Filter `option.label` match the user type `input`
  const filterOption = (input: string, option?: { label: string; value: string }) =>
    (option?.label ?? '').toLowerCase().includes(input.toLowerCase());
    
  const [tableTitleData, setTableTitleData] = useState<string>();
  const [resourceData, setResourceData] = useState<API.ResourceDto[]>();
  const resourceColumns: ColumnsType<API.ResourceDto> = [
    {
      title: 'Key',
      dataIndex: 'key',
      key: 'key',
      render: (text: string) => <div>{text}</div>,
      width: '40%'
    },
    {
      title: 'Value',
      dataIndex: 'value',
      key: 'value',
      render: (text: string) => <div>{text}</div>,
      width: '40%'
    },
    {
      title: 'Action',
      key: 'action',
      width: '20%',
      render: (_, record) => (
        <Space size="middle">
          <a >修改</a>
        </Space>
      ),
    },
  ];

  return (
  <PageContainer>
    <Modal
        title="新增地区"
        open={openCultureModal}
        footer={null}
        closable={false}
      >
      <Form
        form={createCultureForm}
        onFinish={createCulture}
      >
        <Form.Item<string>
          label="Name"
          name="name"
          rules={[{ required: true, message: 'Please input name!' }]}
        >
          <Select
            showSearch
            placeholder="Select a culture"
            optionFilterProp="children"
            onChange={onChange}
            onSearch={onSearch}
            filterOption={filterOption}
            options={selectOptions()}
          />
        </Form.Item>
        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          <Button type="primary" htmlType="submit">
            创建
          </Button>
          <Button type="default" onClick={() => setOpenCreateModal(false)}>
            关闭
          </Button>
        </Form.Item>
      </Form>
    </Modal>
    <Row wrap={false} >
      <Col flex={1}> 
        <Card className={styles.CultureRecoueceRow}>
          <List<API.CultureDto>
            rowKey="id"
            dataSource={culturesData}
            renderItem={(item) =>{
              if (item && item.id) {
                return (
                  <List.Item key={item.id}>
                    <a onClick={() => selectCulture(item)}>
                      {item.name}
                    </a>
                  </List.Item>
                );
              }
              return (
                <List.Item>
                  <Button type="dashed" className={styles.newButton} onClick={() => showOpenCreateModalModal()} >
                    <PlusOutlined /> 新增地区
                  </Button>
                </List.Item>
              );
            }}
            >
          </List>
        </Card>
      </Col>
      <Col flex={9}>
        <Card className={styles.CultureRecoueceRow}>
          <Table title={() => tableTitleData} columns={resourceColumns} dataSource={resourceData}
          />
        </Card>
      </Col>
    </Row>
  </PageContainer>
  );
  };

export default CultureRecouece;
