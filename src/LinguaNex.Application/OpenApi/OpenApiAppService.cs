﻿using LinguaNex.Const;
using LinguaNex.Domain;
using LinguaNex.Dtos;
using LinguaNex.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Globalization;
using Wheel.Core.Dto;
using Wheel.Core.Exceptions;
using Wheel.Services;

namespace LinguaNex.OpenApi
{
    public class OpenApiAppService(IBasicRepository<Culture, long> cultureRepository, IBasicRepository<Projects, string> projectsRepository, IBasicRepository<ProjectAssociation> projectAssociationRepository, IDistributedCache cache) : LinguaNexServiceBase, IOpenApiAppService
    {
        public async Task<R<List<ResourcesDto>>> GetResources(string projectId, string? cultureName, bool all)
        {
            var project = await projectsRepository.FindAsync(projectId);
            if (project == null)
                throw new BusinessException(ErrorCode.NotExist, ErrorCode.NotExist).WithMessageData(project.Id.ToString());
            if (!project.Enalbe)
                throw new BusinessException(ErrorCode.NotEnable, ErrorCode.NotEnable).WithMessageData(project.Name);
            
            var cacheData = await cache.GetAsync<List<ResourcesDto>>(BuildCacheKey(projectId, cultureName, all));
            if(cacheData != null && cacheData.Count > 0)
            {
                return Success(cacheData);
            }

            var datas = await cultureRepository.GetListAsync(
                cultureRepository.BuildPredicate(
                    (true, a => a.ProjectId == projectId),
                    (!string.IsNullOrWhiteSpace(cultureName), a => a.Name == cultureName),
                    (!all && string.IsNullOrWhiteSpace(cultureName), a => a.Name == CultureInfo.CurrentUICulture.Name)
                    ),
                propertySelectors: a => a.Resources
                ); 
            var mainResouces = datas.Select(a => new ResourcesDto
            {
                CultureName = a.Name,
                Resources = a.Resources.ToDictionary(b => b.Key, b => b.Value)
            }).ToList();


            var pa = await projectAssociationRepository.SelectListAsync(a => a.MainProjectId == project.Id, a => a.AssociationProjectId);
            if (pa.Count > 0)
            {
                var associationDatas = (await cultureRepository.GetListAsync(
                cultureRepository.BuildPredicate(
                    (true, a => pa.Contains(a.ProjectId)),
                    (!string.IsNullOrWhiteSpace(cultureName), a => a.Name == cultureName),
                    (!all && string.IsNullOrWhiteSpace(cultureName), a => a.Name == CultureInfo.CurrentUICulture.Name)
                    ),
                propertySelectors: a => a.Resources
                )).Select(a => new ResourcesDto
                {
                    CultureName = a.Name,
                    Resources = a.Resources.ToDictionary(b => b.Key, b => b.Value)
                }).ToList();

                foreach (var associationData in associationDatas)
                {
                    var mr = mainResouces.FirstOrDefault(a => a.CultureName == associationData.CultureName);
                    if (mr != null)
                    {
                        var exceptData = associationData.Resources.Keys.Except(mr.Resources.Keys);
                        if (exceptData.Count() > 0)
                            mr.Resources = mr.Resources.Union(associationData.Resources.Where(a => exceptData.Contains(a.Key))).ToDictionary(a => a.Key, a => a.Value);
                    }
                    else
                    {
                        mainResouces.Add(new ResourcesDto { CultureName = associationData.CultureName, Resources = associationData.Resources });
                    }
                }
            }

            await cache.SetAbsoluteExpirationRelativeToNowAsync(BuildCacheKey(projectId, cultureName, all), mainResouces, TimeSpan.FromMinutes(10));

            return Success(mainResouces);
        }

        private string BuildCacheKey(string projectId, string? cultureName, bool all)
        {
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                return $"{projectId}:{cultureName}";
            }
            if (!all && string.IsNullOrWhiteSpace(cultureName))
            {
                return $"{projectId}:{CultureInfo.CurrentUICulture.Name}";
            }
            return $"{projectId}:all";
        }
    }
}
