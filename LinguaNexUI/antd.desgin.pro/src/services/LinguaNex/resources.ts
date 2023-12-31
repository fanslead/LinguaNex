// @ts-ignore
/* eslint-disable */
import { request } from '@umijs/max';

/** 更新 PUT /api/Resources */
export async function putResources(body: API.UpdateResourceDto, options?: { [key: string]: any }) {
  return request<API.RResourceDto>('/api/Resources', {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 添加 POST /api/Resources */
export async function postResources(body: API.CreateResourceDto, options?: { [key: string]: any }) {
  return request<API.RResourceDto>('/api/Resources', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 删除 DELETE /api/Resources/${param0} */
export async function deleteResourcesId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.deleteResourcesIdParams,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.R>(`/api/Resources/${param0}`, {
    method: 'DELETE',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 根据地区获取所有多语言资源 GET /api/Resources/all/${param0} */
export async function getResourcesAllCultureId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getResourcesAllCultureIdParams,
  options?: { [key: string]: any },
) {
  const { cultureId: param0, ...queryParams } = params;
  return request<API.RListResourceDto>(`/api/Resources/all/${param0}`, {
    method: 'GET',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** Json文件批量导入 POST /api/Resources/File/${param0} */
export async function postResourcesFileCultureId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.postResourcesFileCultureIdParams,
  body: {},
  File?: File,
  options?: { [key: string]: any },
) {
  const { cultureId: param0, ...queryParams } = params;
  const formData = new FormData();

  if (File) {
    formData.append('File', File);
  }

  Object.keys(body).forEach((ele) => {
    const item = (body as any)[ele];

    if (item !== undefined && item !== null) {
      if (typeof item === 'object' && !(item instanceof File)) {
        if (item instanceof Array) {
          item.forEach((f) => formData.append(ele, f || ''));
        } else {
          formData.append(ele, JSON.stringify(item));
        }
      } else {
        formData.append(ele, item);
      }
    }
  });

  return request<API.R>(`/api/Resources/File/${param0}`, {
    method: 'POST',
    params: { ...queryParams },
    data: formData,
    requestType: 'form',
    ...(options || {}),
  });
}

/** 根据地区分页获取多语言资源 GET /api/Resources/list */
export async function getResourcesList(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getResourcesListParams,
  options?: { [key: string]: any },
) {
  return request<API.PageResourceDto>('/api/Resources/list', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 测试多语言 GET /api/Resources/Test */
export async function getResourcesTest(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getResourcesTestParams,
  options?: { [key: string]: any },
) {
  return request<API.RString>('/api/Resources/Test', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}
