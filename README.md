# LinguaNex
多语言连接中心
本项目主打一个一处配置多语言，多处使用的想法。

可配关联项目，主项目关联其他项目即可获取其他项目的多语言资源，相同Key则优先取主项目中的资源（即覆盖）。

## 运行环境
- .NET 8
- Redis
- RabbitMQ(可选)

## OpenApi接入
请求地址：/api/OpenApi/Resources/{ProjectId}?cultureName=&all=

- ProjectId表示项目ID
- cultureName 可选参数，不传则默认当前请求环境语言资源。
- all 可选参数，默认false，cultureName为空时，true则返回所有语言资源
  
响应结构如下：
```
[
  {
    "cultureName": "zh-Hans",
    "resources": {
      "Hello": "你好"
    }
  },
  {
    "cultureName": "en",
    "resources": {
      "Hello": "Hello"
    }
  }
]
```

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
- [x] 导出Toml多语言文件
- [x] 导出messages.properties多语言文件
- [x] 导出xml多语言文件
- [x] .NET SDK
- [ ] JS SDK
- [ ] JAVA SDK
- [ ] GO SDK
- [ ] PY SDK
- [ ] UI管理界面
