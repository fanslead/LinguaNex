// @ts-ignore
/* eslint-disable */
import { request } from '@umijs/max';

/** 分页 GET /api/Culture */
export async function getCulture(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getCultureParams,
  options?: { [key: string]: any },
) {
  return request<API.PageCultureDto>('/api/Culture', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 添加 POST /api/Culture */
export async function postCulture(body: API.CreateCultureDto, options?: { [key: string]: any }) {
  return request<API.RCultureDto>('/api/Culture', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 删除 DELETE /api/Culture/${param0} */
export async function deleteCultureId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.deleteCultureIdParams,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.R>(`/api/Culture/${param0}`, {
    method: 'DELETE',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 获取支持的地区码 GET /api/Culture/SupportedCultures */
export async function getCultureSupportedCultures(options?: { [key: string]: any }) {
  return request<API.RListSupportedCulture>('/api/Culture/SupportedCultures', {
    method: 'GET',
    ...(options || {}),
  });
}
