// @ts-ignore
/* eslint-disable */
import { request } from '@umijs/max';

/** 获取资源 GET /api/OpenApi/Resources/${param0} */
export async function getOpenApiResourcesProjectId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getOpenApiResourcesProjectIdParams,
  options?: { [key: string]: any },
) {
  const { projectId: param0, ...queryParams } = params;
  return request<API.ResourcesDto[]>(`/api/OpenApi/Resources/${param0}`, {
    method: 'GET',
    params: {
      ...queryParams,
    },
    ...(options || {}),
  });
}

/** 导出JSON文件 GET /api/OpenApi/Resources/Json/${param0} */
export async function getOpenApiResourcesJsonProjectId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getOpenApiResourcesJsonProjectIdParams,
  options?: { [key: string]: any },
) {
  const { projectId: param0, ...queryParams } = params;
  window.open(`/api/OpenApi/Resources/Json/${param0}`, '_blank')
  // return request<any>(`/api/OpenApi/Resources/Json/${param0}`, {
  //   method: 'GET',
  //   params: {
  //     ...queryParams,
  //   },
  //   responseType: 'blob',
  //   ...(options || {}),
  // })
}

/** 导出properties文件 GET /api/OpenApi/Resources/Properties/${param0} */
export async function getOpenApiResourcesPropertiesProjectId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getOpenApiResourcesPropertiesProjectIdParams,
  options?: { [key: string]: any },
) {
  const { projectId: param0, ...queryParams } = params;
  window.open(`/api/OpenApi/Resources/Properties/${param0}`, '_blank')
  // return request<any>(`/api/OpenApi/Resources/Properties/${param0}`, {
  //   method: 'GET',
  //   params: {
  //     ...queryParams,
  //   },
  //   ...(options || {}),
  // });
}

/** 导出toml文件 GET /api/OpenApi/Resources/toml/${param0} */
export async function getOpenApiResourcesTomlProjectId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getOpenApiResourcesTomlProjectIdParams,
  options?: { [key: string]: any },
) {
  const { projectId: param0, ...queryParams } = params;
  
  window.open(`/api/OpenApi/Resources/toml/${param0}`, '_blank')
  // return request<any>(`/api/OpenApi/Resources/toml/${param0}`, {
  //   method: 'GET',
  //   params: {
  //     ...queryParams,
  //   },
  //   ...(options || {}),
  // });
}

/** 导出xml文件 GET /api/OpenApi/Resources/xml/${param0} */
export async function getOpenApiResourcesXmlProjectId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getOpenApiResourcesXmlProjectIdParams,
  options?: { [key: string]: any },
) {
  const { projectId: param0, ...queryParams } = params;
  window.open(`/api/OpenApi/Resources/xml/${param0}`, '_blank')
  // return request<any>(`/api/OpenApi/Resources/xml/${param0}`, {
  //   method: 'GET',
  //   params: {
  //     ...queryParams,
  //   },
  //   ...(options || {}),
  // });
}

/** 获取支持的地区码 GET /api/OpenApi/SupportedCultures */
export async function getOpenApiSupportedCultures(options?: { [key: string]: any }) {
  return request<API.RListSupportedCulture>('/api/OpenApi/SupportedCultures', {
    method: 'GET',
    ...(options || {}),
  });
}
