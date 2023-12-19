# LinguaNex
多语言连接中心
本项目主打一个一处配置多语言，多处使用的想法。

可配置通用项目，业务项目关联通用项目即可获取通用项目的多语言资源，相同Key则优先取业务项目中的资源（即覆盖）。

## 运行环境
- .NET 8
- Redis
- RabbitMQ(可选)

## RoadMap
- [x] Project项目管理API
- [x] Project项目关联API
- [x] Culture地区管理API
- [x] Resouce多语言资源管理API
- [x] OpenApi获取多语言资源
- [x] SignalR获取多语言资源与实时推送更新多语言资源
- [ ] 集成三方翻译API
- [ ] 集成AI翻译
- [x] 导出JSON多语言文件
- [x] .NET SDK
- [ ] JS SDK
- [ ] JAVA SDK
- [ ] GO SDK
- [ ] PY SDK
- [ ] UI管理界面
