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
## SignalR接入(c#例子)
``` c#
var connection = new HubConnectionBuilder()
    .WithUrl($"{linguaNexApiUrl}/hubs/LinguaNex?project={project}", Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets)
    .AddJsonProtocol()
    .WithAutomaticReconnect()
    .Build();

connection.On<LinguaNexResources>("CreateOrUpdateResource", obj => 
{
    if (_resourcesCache.TryGetValue(obj.CultureName, out var value))
    {
        foreach (var resource in obj.Resources)
        {
            value[resource.Key] = resource.Value;
        }
        _resourcesCache[obj.CultureName] = value;
    }else
    {
        _resourcesCache[obj.CultureName] = new ConcurrentDictionary<string, string>(obj.Resources);
    }
});

connection.StartAsync();

//拉取资源 参数跟OpenApi接口一致
connection.InvokeAsync<List<LinguaNexResources>>("GetResources", projectId, cultureName,all);
```

## 效果图
![image](https://github.com/fanslead/LinguaNex/assets/22066473/8f38e6dd-63ee-4d78-9434-ef9f302a1630)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/55238455-c0a3-483d-bf0d-fd3307b07683)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/c6271834-d961-4b79-a25a-c9aff58482ab)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/ad488c11-bd0e-4809-b9ff-c98d1ba69207)



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
