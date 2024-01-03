using Humanizer;
using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Entities;
using LinguaNex.EventDatas;
using LinguaNex.Translates;
using Wheel.Core.Exceptions;
using Wheel.DependencyInjection;
using Wheel.EventBus.Distributed;
using Wheel.Utilities;

namespace LinguaNex.Handlers
{
    public class ResourceSyncCultureAndTranslateEventHandler(IBasicRepository<Resource, string> resourceRepository, IBasicRepository<Culture, string> cultureRepository, ITranslateAppService translateAppService, IServiceProvider serviceProvider) : IDistributedEventHandler<ResourceSyncCultureAndTranslateEto>, ITransientDependency
    {
        public async Task Handle(ResourceSyncCultureAndTranslateEto eventData, CancellationToken cancellationToken = default)
        {
            try
            {
                var resource = await resourceRepository.FindAsync(s => s.Id == eventData.Id);
                if (resource == null)
                {
                    throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData($"Resource:{eventData.Id}");
                }

                var cultrue = await cultureRepository.FindAsync(s => s.Id == resource.CultureId);
                if (cultrue == null)
                {
                    throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageDataData($"Culture:{resource.CultureId}");
                }
                var cultrueList = await cultureRepository.GetListAsync(s => true);
                if (cultrueList != null && cultrueList.Any())
                {
                    var resourceList = new List<Resource>();
                    var snowflakeIdGenerator = serviceProvider.GetRequiredService<SnowflakeIdGenerator>();
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
                            if (eventData.Translate) //翻译
                            {
                                var value = await translateAppService.Translate(new Translates.Dto.TranslateRequestDto() { SourceLang = cultrue.Name, SourceString = resource.Value, TargetLang = item.Name, TranslateProvider = eventData.TranslateProvider ?? Emuns.TranslateProviderEnum.Baidu });
                                resource.Value = value;
                            }
                            resourceList.Add(resourceItem);
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
