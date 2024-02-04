import { DownOutlined, PlusOutlined, UserOutlined } from '@ant-design/icons';
import { Button, Card, List, Typography, Modal, Table, Space, Form, Input, Checkbox, Dropdown, MenuProps, Breadcrumb } from 'antd';
import { PageContainer } from '@ant-design/pro-layout';
import { useRequest } from 'umi';
import { history, Link } from '@umijs/max';
import { getProjects, postProjects, putProjectsEnableId, getProjectsCanAssociationProjects, postProjectsProjectAssociation, deleteProjectsProjectAssociation } from '@/services/LinguaNex/projects';
import styles from './style.less';
import { useState, useRef, useImperativeHandle } from 'react';
import { ColumnsType } from 'antd/es/table';
import { getOpenApiResourcesJsonProjectId, getOpenApiResourcesTomlProjectId, getOpenApiResourcesTomlProjectId, getOpenApiResourcesTsProjectId, getOpenApiResourcesXmlProjectId } from '@/services/LinguaNex/openApi';

const { Paragraph } = Typography;

const CardList = () => {
  const [cardData, setCardData] = useState<API.ProjectDto[]>();
  const { data, loading } = useRequest(() => {
    fetchCardData()
  });
  const fetchCardData = async () =>{
    var cardData = await getProjects({
      PageIndex: 1,
      PageSize: 1000
    });
    setCardData([{}].concat(cardData.data as any))
  }

  const content = (
    <div className={styles.pageHeaderContent}>
      <p>
        LinguxNex 项目
      </p>
    </div>
  );

  const extraContent = (
    <div className={styles.extraImg}>
      <img
        src="https://gw.alipayobjects.com/zos/rmsportal/RzwpdLnhmvDJToTdfDPe.png"
      />
    </div>
  );
  const nullData: API.ProjectDto[] = [];

  const [open, setOpen] = useState(false);
  const [openCreateModal, setOpenCreateModal] = useState(false);

  const fetchAssociationProjectsData = async (prijectId: string | undefined) =>{
    const { data } = await getProjectsCanAssociationProjects({
      projectId: prijectId
    })
    setHasAssociationProjectsData(data.hasAssociationProjects);
    setCanAssociationProjectsData(data.canAssociationProjects);
  }


  const [currentProjectId, setCurrentProjectId] = useState<any>();
  const [createProjectForm] = Form.useForm();
  const showModal = async (projectId: string | undefined) => {
    setCurrentProjectId(projectId);
    fetchAssociationProjectsData(projectId)
    setOpen(true);
  };

  const hideModal = () => {
    setCurrentProjectId(undefined);
    setOpen(false);
  };
  const showOpenCreateModalModal = () =>{
    setOpenCreateModal(true);
  }
  const hideCreateModalModal = () => {
    setOpenCreateModal(false);
  };
  const createProject = async (params: any) => {
    await postProjects({
      name: params.name,
      enalbe: true
    })
    createProjectForm.resetFields();
    hideCreateModalModal();
    fetchCardData();
  }
  const bindProjectsProjectAssociation = async (projectId: string | undefined, mainProjectId: string | undefined) => {
    await postProjectsProjectAssociation({
      mainProjectId: mainProjectId,
      associationProjectIds: [
        projectId
      ]
    })
    await fetchAssociationProjectsData(mainProjectId)
  }
  const delProjectsProjectAssociation = async (projectId: string | undefined, mainProjectId: string | undefined) =>{
    await deleteProjectsProjectAssociation({
      mainProjectId: mainProjectId,
      associationProjectId: projectId

    })
    await fetchAssociationProjectsData(mainProjectId)
  }
  const putProjectsEnable = async (id: string) => {
    await putProjectsEnableId({
      id: id
    })
    fetchCardData()
  }
  const [hasAssociationProjectsData, setHasAssociationProjectsData] = useState<API.ProjectDto[]>();
  const [canAssociationProjectsData, setCanAssociationProjectsData] = useState<API.ProjectDto[]>();
  const hasAssociationProjectsColumns: ColumnsType<API.ProjectDto> = [
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      render: (text) => <div>{text}</div>,
      width: '65%'
    },
    {
      title: 'Action',
      key: 'action',
      width: '35%',
      render: (_, record) => (
        <Space size="middle">
          <a onClick={() => delProjectsProjectAssociation(record.id, currentProjectId)}>取消关联 {record.name}</a>
        </Space>
      ),
    },
  ];
  const canAssociationProjectsColumns: ColumnsType<API.ProjectDto> = [
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      width: '65%',
      render: (text) => <div>{text}</div>,
    },
    {
      title: 'Action',
      key: 'action',
      width: '35%',
      render: (_, record) => (
        <Space size="middle">
          <a onClick={() => bindProjectsProjectAssociation(record.id, currentProjectId)}>关联 {record.name}</a>
        </Space>
      ),
    },
  ];

  const handleMenuClick = (e: any, project: string) => {
    switch(e.key){
      case '1': getOpenApiResourcesJsonProjectId({projectId:project})
        break;
      case '2': getOpenApiResourcesXmlProjectId({projectId:project})
        break;
      case '3': getOpenApiResourcesTomlProjectId({projectId:project})
        break;
      case '4': getOpenApiResourcesTomlProjectId({projectId:project})
        break;
      case '5': getOpenApiResourcesTsProjectId({projectId:project})
        break;
    }
  };

  const items: MenuProps['items'] = [
    {
      label: 'json',
      key: '1',
      icon: <UserOutlined />,
    },
    {
      label: 'xml',
      key: '2',
      icon: <UserOutlined />,
    },
    {
      label: 'toml',
      key: '3',
      icon: <UserOutlined />
    },
    {
      label: 'properties',
      key: '4',
      icon: <UserOutlined />
    },
    {
      label: 'ts',
      key: '5',
      icon: <UserOutlined />
    },
  ];

  const menuProps = {

  };
  return (
    <PageContainer content={content} extraContent={extraContent}>
      <div className={styles.cardList}>
        <List<API.ProjectDto>
          rowKey="id"
          // loading={loading}
          grid={{
            gutter: 16,
            xs: 1,
            sm: 2,
            md: 3,
            lg: 3,
            xl: 4,
            xxl: 4,
          }}
          dataSource={cardData}
          renderItem={(item) => {
            if (item && item.id) {
              return (
                <List.Item key={item.id}>
                  <Card
                    hoverable
                    className={styles.card}
                    actions={[
                    <Dropdown menu={{
                      items,
                      onClick: (e) => handleMenuClick(e, item.id)
                      }}>
                      <Button key="option0" type="text">
                        <Space>
                          下载
                          <DownOutlined />
                        </Space>
                      </Button>
                    </Dropdown>,
                    <Button key="option1" type="text" onClick={() => showModal(item.id)} >关联项目</Button>,
                    <Button key="option2" type="text" onClick={() => history.push('/CultureRecouece/'+item.id)}>查看</Button>,
                    <Button key="option3" type="text" onClick={() => putProjectsEnable(item.id as string)} danger={item.enalbe}>{item.enalbe?'禁用':'启用'}</Button>
                  ]}
                  >
                    <Card.Meta
                    //   avatar={<img alt="" className={styles.cardAvatar} src={item.avatar} />}
                      title={<a>{item.name}</a>}
                      description={
                        <Paragraph className={styles.item} ellipsis={{ rows: 3 }}>
                          {item.id}
                        </Paragraph>
                      }
                    />
                  </Card>
                </List.Item>
              );
            }
            return (
              <List.Item>
                <Button type="dashed" className={styles.newButton} onClick={() => showOpenCreateModalModal()} >
                  <PlusOutlined /> 新增项目
                </Button>
              </List.Item>
            );
          }}
        />
      </div>
      <Modal
        title="项目关联"
        open={open}
        onOk={hideModal}
        onCancel={hideModal}
        okText="确认"
        cancelText="取消"
      >
        <Table pagination={false} title={() => "HasAssociationProjects"} columns={hasAssociationProjectsColumns} dataSource={hasAssociationProjectsData} />
        <Table pagination={false}  title={() => "CanAssociationProjects"} columns={canAssociationProjectsColumns} dataSource={canAssociationProjectsData} />
      </Modal>
      <Modal
        title="新增项目"
        open={openCreateModal}
        footer={null}
        closable={false}
      >
        <Form
          form={createProjectForm}
          onFinish={createProject}
        >
          <Form.Item<string>
            label="Name"
            name="name"
            rules={[{ required: true, message: 'Please input name!' }]}
          >
            <Input />
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
    </PageContainer>
  );
};

export default CardList;
