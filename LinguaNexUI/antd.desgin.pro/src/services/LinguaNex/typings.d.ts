declare namespace API {
  type AssociationProjectsDto = {
    /** 可关联项目列表 */
    canAssociationProjects?: ProjectDto[];
    /** 已关联项目列表 */
    hasAssociationProjects?: ProjectDto[];
  };

  type CreateCultureDto = {
    name?: string;
    projectId?: string;
    syncResource?: boolean;
    translate?: boolean;
    translateProvider?: TranslateProviderEnum;
  };

  type CreateProjectAssociationDto = {
    mainProjectId?: string;
    associationProjectIds?: string[];
  };

  type CreateProjectDto = {
    name?: string;
    enalbe?: boolean;
  };

  type CreateResourceDto = {
    key?: string;
    value?: string;
    cultureId?: string;
    projectId?: string;
    syncCulture?: boolean;
    translate?: boolean;
    translateProvider?: TranslateProviderEnum;
  };

  type CultureDto = {
    id?: string;
    name?: string;
    projectId?: string;
  };

  type deleteCultureIdParams = {
    id: string;
  };

  type DeleteProjectAssociationDto = {
    mainProjectId?: string;
    associationProjectId?: string;
  };

  type deleteProjectsIdParams = {
    id: string;
  };

  type deleteResourcesIdParams = {
    id: string;
  };

  type getCultureParams = {
    ProjectId?: string;
    PageIndex?: number;
    PageSize?: number;
    OrderBy?: string;
  };

  type getOpenApiResourcesJsonProjectIdParams = {
    projectId: string;
    cultureName?: string;
  };

  type getOpenApiResourcesProjectIdParams = {
    /** 项目ID */
    projectId: string;
    /** 地区语言码 */
    cultureName?: string;
    /** 是否获取所有 */
    all?: boolean;
  };

  type getOpenApiResourcesPropertiesProjectIdParams = {
    projectId: string;
    cultureName?: string;
  };

  type getOpenApiResourcesTomlProjectIdParams = {
    projectId: string;
    cultureName?: string;
  };

  type getOpenApiResourcesXmlProjectIdParams = {
    projectId: string;
    cultureName?: string;
  };

  type getOpenApiResourcesTsProjectIdParams = {
    projectId: string;
    cultureName?: string;
  };

  type getProjectsCanAssociationProjectsParams = {
    projectId?: string;
  };

  type getProjectsIdParams = {
    id: string;
  };

  type getProjectsParams = {
    PageIndex?: number;
    PageSize?: number;
    OrderBy?: string;
  };

  type getResourcesAllCultureIdParams = {
    cultureId: string;
  };

  type getResourcesListParams = {
    CultureId?: string;
    PageIndex?: number;
    PageSize?: number;
    OrderBy?: string;
  };

  type getResourcesTestParams = {
    /** 测试字符串 */
    testStr?: string;
  };

  type PageCultureDto = {
    code?: string;
    message?: string;
    data?: CultureDto[];
    total?: string;
  };

  type PageProjectDto = {
    code?: string;
    message?: string;
    data?: ProjectDto[];
    total?: string;
  };

  type PageResourceDto = {
    code?: string;
    message?: string;
    data?: ResourceDto[];
    total?: string;
  };

  type postResourcesFileCultureIdParams = {
    cultureId: string;
  };

  type ProjectDto = {
    id?: string;
    name?: string;
    enalbe?: boolean;
  };

  type putProjectsEnableIdParams = {
    id: string;
  };

  type R = {
    code?: string;
    message?: string;
  };

  type RAssociationProjectsDto = {
    code?: string;
    message?: string;
    data?: AssociationProjectsDto;
  };

  type RCultureDto = {
    code?: string;
    message?: string;
    data?: CultureDto;
  };

  type ResourceDto = {
    id?: string;
    key?: string;
    value?: string;
    cultureId?: string;
    projectId?: string;
  };

  type ResourcesDto = {
    cultureName?: string;
    resources?: Record<string, any>;
  };

  type RListResourceDto = {
    code?: string;
    message?: string;
    data?: ResourceDto[];
  };

  type RListSupportedCulture = {
    code?: string;
    message?: string;
    data?: SupportedCulture[];
  };

  type RProjectDto = {
    code?: string;
    message?: string;
    data?: ProjectDto;
  };

  type RResourceDto = {
    code?: string;
    message?: string;
    data?: ResourceDto;
  };

  type RString = {
    code?: string;
    message?: string;
    data?: string;
  };

  type SupportedCulture = {
    name?: string;
    displayName?: string;
    englishName?: string;
  };

  type TranslateProviderEnum = 0 | 1 | 2 | 3;

  type UpdateResourceDto = {
    id?: string;
    value?: string;
  };
}
