# LinguaNex
多语言连接中心
本项目主打一个一处配置多语言，多处使用的想法。让项目方便快捷实现国际化（多语言）。

可配关联项目，主项目关联其他项目即可获取其他项目的多语言资源，相同Key则优先取主项目中的资源（即覆盖）。

批量导入已存在项目多语言资源。

自动翻译，翻译效果根据不同翻译Provider效果不一致。

测试环境

http://47.119.20.111

http://47.119.20.111/swagger/index.html
### 应用场景
通过API/SDK拉取多语言资源加载，可选WebSocket对接实现即时更新多语言资源。
- API后端项目响应内容，如错误码对应的Message国际化多语言处理。
- Web项目国际化多语言集成，可导出多语言文件编译，或对接API/SDK即时获取加载数据。
- APP项目与Web基本一致。
- 骚操作：实现一个短Key完成长文章多语言显示。

## 运行环境
- .NET 8
- Redis
- RabbitMQ(可选)
- EF Core SQLLite（可自行替换数据库）

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
## .NET SDK 接入
``` c#
builder.Services.AddLinguaNexLocalization(options =>
{
    options.LinguaNexApiUrl = builder.Configuration["LinguaNex:ApiUrl"];
    options.Project = builder.Configuration["LinguaNex:Project"];
    options.UseWebSocket = true;
});
builder.Services.AddLocalization();

app.UseRequestLocalization();
```
## Java SDK 接入
``` java
public static void main(String[] args) {
        ResourceBundleMessageSource source = new RemoteSourceBundle();
        GlobalProp.initFromYaml(null);
        Locale locale = new Locale("zh-Hans");
        BundleTest test = new BundleTest();
        System.out.println(source.getMessage("40004", null, locale));
    }
```


## JS SDK 接入
``` js
const { initLinguaNex, setLocale, getLocale, getAllLocale, L } = linguanex
initLinguaNex({
    baseUrl: 'http://47.119.20.111',
    locales: ["zh-CN", "en"],
    defaultLocale: 'zh-CN',
    project: 'C96755D0-C22C-4DAD-9620-AF64C4C3D9D7'
})
.then(() => {
    console.log(L('Hello'));
    setLocale('aa')
    .then(() => {
        console.log(L('Hello'));
        console.log(getAllLocale());
    })
    console.log(getLocale("zh-CN"));
})
```


## 效果图
![image](https://github.com/fanslead/LinguaNex/assets/22066473/afcc5346-b21b-4762-880f-aa60ea7e36c3)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/f97edbcb-6523-43d8-a97a-fe56be92dfa1)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/184186cf-99c6-4cba-8b34-da928baf9167)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/d8cbe946-2f73-4e37-b1df-f18816055a20)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/eff1d832-5011-4a87-9c11-f651f739785e)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/a7d6f086-5c77-49f8-824d-cafee8038731)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/360eedad-ef6a-445d-bab6-03e2f9fe5c4a)

![image](https://github.com/fanslead/LinguaNex/assets/22066473/3070bb22-d5d0-4d95-9035-379308a91054)

## RoadMap
- [x] Project项目管理API
- [x] Project项目关联API
- [x] Culture地区管理API
- [x] Resouce多语言资源管理API
- [x] OpenApi获取多语言资源
- [x] SignalR获取多语言资源与实时推送更新多语言资源
- [x] 自动同步资源到不同Culture
- [x] 自动翻译资源到不同Culture
- [x] 集成三方翻译API （目前支持百度，有道，腾讯，阿里机器翻译API）
- [x] 集成AI翻译
- [x] 导出JSON多语言文件
- [x] 导出Toml多语言文件
- [x] 导出messages.properties多语言文件
- [x] 导出xml多语言文件
- [x] 导出TS多语言文件
- [x] 批量导入多语言配置(JSON)
- [x] .NET SDK
- [x] JS SDK
- [x] JAVA SDK
- [x] GO SDK
- [ ] PY SDK
- [x] UI管理界面
