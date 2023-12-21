// @ts-ignore
/* eslint-disable */
import { request } from '@umijs/max';

/** 分页 GET /api/Projects */
export async function getProjects(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getProjectsParams,
  options?: { [key: string]: any },
) {
  return request<API.PageProjectDto>('/api/Projects', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 添加 POST /api/Projects */
export async function postProjects(body: API.CreateProjectDto, options?: { [key: string]: any }) {
  return request<API.RProjectDto>('/api/Projects', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 获取单个 GET /api/Projects/${param0} */
export async function getProjectsId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getProjectsIdParams,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.RProjectDto>(`/api/Projects/${param0}`, {
    method: 'GET',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 删除 DELETE /api/Projects/${param0} */
export async function deleteProjectsId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.deleteProjectsIdParams,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.R>(`/api/Projects/${param0}`, {
    method: 'DELETE',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 获取可关联项目 GET /api/Projects/CanAssociationProjects */
export async function getProjectsCanAssociationProjects(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.getProjectsCanAssociationProjectsParams,
  options?: { [key: string]: any },
) {
  return request<API.RAssociationProjectsDto>('/api/Projects/CanAssociationProjects', {
    method: 'GET',
    params: {
      ...params,
    },
    ...(options || {}),
  });
}

/** 修改是否启用 PUT /api/Projects/enable/${param0} */
export async function putProjectsEnableId(
  // 叠加生成的Param类型 (非body参数swagger默认没有生成对象)
  params: API.putProjectsEnableIdParams,
  options?: { [key: string]: any },
) {
  const { id: param0, ...queryParams } = params;
  return request<API.R>(`/api/Projects/enable/${param0}`, {
    method: 'PUT',
    params: { ...queryParams },
    ...(options || {}),
  });
}

/** 创建项目关联 POST /api/Projects/ProjectAssociation */
export async function postProjectsProjectAssociation(
  body: API.CreateProjectAssociationDto,
  options?: { [key: string]: any },
) {
  return request<API.R>('/api/Projects/ProjectAssociation', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}

/** 删除项目关联 DELETE /api/Projects/ProjectAssociation */
export async function deleteProjectsProjectAssociation(
  body: API.DeleteProjectAssociationDto,
  options?: { [key: string]: any },
) {
  return request<API.R>('/api/Projects/ProjectAssociation', {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
    },
    data: body,
    ...(options || {}),
  });
}
