import { PlusOutlined } from '@ant-design/icons';
import { Button, Card, List, Typography } from 'antd';
import { PageContainer } from '@ant-design/pro-layout';
import { useRequest } from 'umi';
import { getProjects } from '@/services/LinguaNex/projects';
import styles from './style.less';

const { Paragraph } = Typography;

const CardList = () => {
  const { data, loading } = useRequest(() => {
    return getProjects({
      PageIndex: 1,
      PageSize: 1000
    });
  });

  const list = data || [];

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
  const nullData: Partial<API.ProjectDto> = {};
  return (
    <PageContainer content={content} extraContent={extraContent}>
      <div className={styles.cardList}>
        <List<Partial<API.ProjectDto>>
          rowKey="id"
          loading={loading}
          grid={{
            gutter: 16,
            xs: 1,
            sm: 2,
            md: 3,
            lg: 3,
            xl: 4,
            xxl: 4,
          }}
          dataSource={[nullData, ...list]}
          renderItem={(item) => {
            if (item && item.id) {
              return (
                <List.Item key={item.id}>
                  <Card
                    hoverable
                    className={styles.card}
                    actions={[<a key="option1">关联项目</a>, <a key="option2">查看</a>, <a key="option3">复制Id</a>]}
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
                <Button type="dashed" className={styles.newButton}>
                  <PlusOutlined /> 新增产品
                </Button>
              </List.Item>
            );
          }}
        />
      </div>
    </PageContainer>
  );
};

export default CardList;
