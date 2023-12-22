import React, { useState } from 'react';
import { Button, Card, Col, Row, Space, Table } from 'antd';
import { PageContainer } from '@ant-design/pro-components';
import styles from './style.less';
import { List } from 'antd';
import { useRequest, useParams } from 'umi';
import { PlusOutlined } from '@ant-design/icons';
import { getCulture, getCultureSupportedCultures } from '@/services/LinguaNex/culture';
import { getResourcesList } from '@/services/LinguaNex/resources';
import { history, Link, matchPath } from '@umijs/max';
import { ColumnsType } from 'antd/es/table';

const CultureRecouece = () => {
  const [culturesData, setCulturesData] = useState<API.CultureDto[]>();

  const { data, loading } = useRequest(() => {
    fetcCulturesData()
  });
  const fetcCulturesData = async () =>{
    var cultures = await getCultures();
    selectCulture(cultures.data[0]);
    setCulturesData([{}].concat(cultures.data))
  }

  const getCultures = async () =>{
    const match = matchPath({ path: "/CultureRecouece/:id" }, history.location.pathname)
    const { id } = match.params;
    return await getCulture({ProjectId: id})
  }
  const selectCulture = (culture : API.CultureDto) => {
    console.log(culture)
    fetcResourceData(culture.id)
  }

  const fetcResourceData = async (cultureId: string | undefined) =>{
    var resource = await getResourcesList({
      CultureId: cultureId
    });
    setResourceData(resource.data);
  }

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
    <Row wrap={false} >
      <Col flex={1}> 
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
                <Button type="dashed" className={styles.newButton} >
                  <PlusOutlined /> 新增地区
                </Button>
              </List.Item>
            );
          }}
          >
        </List>
      </Col>
      <Col flex={9}>
        <Table columns={resourceColumns} dataSource={resourceData}/>
      </Col>
    </Row>
  </PageContainer>
  );
  };

export default CultureRecouece;
