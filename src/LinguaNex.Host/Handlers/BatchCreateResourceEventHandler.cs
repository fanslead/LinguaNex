using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Translates;
using Microsoft.Extensions.DependencyInjection;
using Wheel.Core.Exceptions;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;
using Wheel.Utilities;

namespace LinguaNex.Handlers
{
    public class BatchCreateResourceEventHandler(IBasicRepository<Culture, string> cultureRepository, IBasicRepository<Resource, string> resourceRepository, ITranslateAppService translateAppService, IServiceProvider serviceProvider) : IDistributedEventHandler<BatchCreateResourceEto>, ITransientDependency
    {
        public async Task Handle(BatchCreateResourceEto eventData, CancellationToken cancellationToken = default)
        {
            try
            {


                var cultrue = await cultureRepository.FindAsync(s => s.Id == eventData.CultureId);
                if (cultrue == null)
                {
                    throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData($"CultureId:{eventData.CultureId}");
                }
                var syncResourceList = await resourceRepository.GetListAsync(s => s.Id >= eventData.FirstResourceId);
                if (syncResourceList != null && syncResourceList.Any())
                {
                    var cultrueList = await cultureRepository.GetListAsync(s => true);
                    if (cultrueList != null && cultrueList.Any())
                    {
                        var resourceList = new List<Resource>();
                        var snowflakeIdGenerator = serviceProvider.GetRequiredService<SnowflakeIdGenerator>();
                        foreach (var resource in syncResourceList)
                        {
                            foreach (var item in cultrueList)
                            {
                                if (item.Resources?.Find(s => s.Key == resource.Key) == null)
                                {
                                    var resourceItem = new Resource()
                                    {
                                        Id = snowflakeIdGenerator.Create(),
                                        CultureId = item.Id,
                                        Key = resource.Key,
                                        ProjectId = resource.ProjectId,
                                        Value = resource.Value,
                                    };
                                    if (eventData.Translate ?? true) //翻译
                                    {
                                        var value = await translateAppService.Translate(new Translates.Dto.TranslateRequestDto() { SourceLang = cultrue.Name, SourceString = resource.Value, TargetLang = item.Name, TranslateProvider = eventData.TranslateProvider ?? Emuns.TranslateProviderEnum.Baidu });
                                        resource.Value = value;
                                    }
                                    resourceList.Add(resourceItem);
                                }
                            }
                        }
                        if (resourceList.Any())
                        {
                            await resourceRepository.InsertManyAsync(resourceList, true, cancellationToken);
                        }
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
