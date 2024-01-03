using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Dtos;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Hubs;
using LinguaNex.Translates;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Minio.DataModel;
using Wheel.Core.Exceptions;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;
using Wheel.Utilities;

namespace LinguaNex.Handlers
{
    public class CultrueSyncResourceAndTranslateEventHandler(IBasicRepository<Culture, long> cultureRepository, IBasicRepository<Resource, long> resourceRepository, ITranslateAppService translateAppService, IServiceProvider serviceProvider) : IDistributedEventHandler<CultrueSyncResourceAndTranslateEto>, ITransientDependency
    {
        public async Task Handle(CultrueSyncResourceAndTranslateEto eventData, CancellationToken cancellationToken = default)
        {
            try
            {
                //当前新增的cultrue
                var cultrue = await cultureRepository.FindAsync(s => s.Id == eventData.Id);
                if (cultrue == null)
                {
                    throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData(eventData.Id.ToString());
                }
                if (eventData.SyncResource)
                {
                    var resourceList = new List<Resource>();
                    var syncCultrue = await cultureRepository.FindAsync(s => s.Name == "en");
                    //var syncCultrue = await cultureRepository.FindAsync(cultureRepository.BuildPredicate(
                    //    (true, s=>s.Resources != null)
                    //    ));
                    if (syncCultrue != null && syncCultrue.Resources != null && syncCultrue.Resources.Any())
                    {
                        var snowflakeIdGenerator = serviceProvider.GetRequiredService<SnowflakeIdGenerator>();
                        foreach (var resource in syncCultrue.Resources)
                        {
                            var resourceItem = new Resource()
                            {
                                Id = snowflakeIdGenerator.Create(),
                                CultureId = cultrue.Id,
                                Key = resource.Key,
                                ProjectId = resource.ProjectId,
                                Value = resource.Value,
                            };
                            if (eventData.Translate) //翻译
                            {
                                var value = await translateAppService.Translate(new Translates.Dto.TranslateRequestDto() { SourceLang = syncCultrue.Name, SourceString = resource.Value, TargetLang = cultrue.Name, TranslateProvider = eventData.TranslateProvider ?? Emuns.TranslateProviderEnum.Baidu });
                                resourceItem.Value = value;
                            }
                            resourceList.Add(resource);
                        }
                    }

                    if (resourceList.Any())
                    {
                        await resourceRepository.InsertManyAsync(resourceList, true, cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(ErrorCode.InternalError, ErrorCode.InternalError).WithMessageDataData(ex.Message);
            }
        }
    }
}
