using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LeoProject.Infrastructure.Controllers;
using LeoProject.Infrastructure.Controllers.Response;
using LeoProject.LionOA.Api.ViewModel.Request.Role;
using LeoProject.LionOA.Core;
using LeoProject.LionOA.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeoProject.Infrastructure.Extensions;
using LeoProject.LionOA.Api.ViewModel.Request.Module;
using LeoProject.Infrastructure.Tree;

namespace LeoProject.LionOA.Api.Controllers
{
    public class ModuleController : OABaseController
    {
        private readonly ISysModuleService _sysModuleService;
        public ModuleController(ISysModuleService sysModuleService)
        {
            _sysModuleService = sysModuleService;
        }
        [HttpPost]
        public async Task<ApiResult> Insert(ModuleReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }

            var treePath = await GetPathByParentId(req.ParentId);
            SysModule module = new SysModule
            {
                Name = req.Name,
                ParentId = req.ParentId,
                ParentName = req.ParentName,
                TreePath = treePath,
                Url = req.Url,
                IsLeaf = req.IsLeaf,
                IsAutoExpand = req.IsAutoExpand,
                Icon = req.Icon,
                Component = req.Component,
                FuncPermission = req.FuncPermission,
                Sort = req.Sort,
                Remark = req.Remark,
                CreateBy = CurrentUser.UserId
            };
            var res = await _sysModuleService.InsertAsync(module);
            if (res > 0)
            {
                return Success("添加成功");
            }
            return Success("添加失败");
        }
        [HttpGet]
        public async Task<ApiResult> Get(SearchModuleReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }
            Expression<Func<SysModule, bool>> exp = m => true;
            if (req.ParentId > 0)
            {
                exp = exp.And(m => m.ParentId == req.ParentId);
            }
            var query = _sysModuleService.GetQueryable(exp);

            var list = await query.ToListAsync();

            return Success( list);
        }
        [HttpGet]
        public async Task<ApiResult> GetOne([FromQuery] int id = 0)
        {
            var model = await _sysModuleService.GetByIdAsync(id);
            return Success(model);
        }
        [HttpPost]
        public async Task<ApiResult> Update(ModuleReq req)
        {
            if (req == null)
            {
                return Error("请求参数不能为空");
            }
            if (req.Id == 0)
            {
                return await Insert(req);
            }
            
            var model = await _sysModuleService.GetByIdAsync(req.Id);
            if (model == null)
            {
                return Error("请求参数传递错误");
            }
            var treePath = await GetPathByParentId(req.ParentId);
            model.Name = req.Name;
            model.ParentId = req.ParentId;
            model.ParentName = req.ParentName;
            model.TreePath = treePath;
            model.Url = req.Url;
            model.IsLeaf = req.IsLeaf;
            model.IsAutoExpand = req.IsAutoExpand;
            model.Icon = req.Icon;
            model.Component = req.Component;
            model.FuncPermission = req.FuncPermission;
            model.Sort = req.Sort;
            model.Remark = req.Remark;
            model.UpdateBy = CurrentUser.UserId;
            model.UpdateTime = DateTime.Now;
            var res = await _sysModuleService.UpdateAsync(model);
            if (res > 0)
            {
                return Success("更新成功");
            }
            return Success("更新失败");
        }
        /// <summary>
        /// 根据父Id获取路径
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private async Task<string> GetPathByParentId (long parentId)
        {
            var treePath = "";
            if (parentId == 0)
            {
                treePath = "0";
            }
            else
            {
                var parent = await _sysModuleService.GetByIdAsync(parentId);
                if (parent != null)
                {
                    treePath = parent.TreePath + "," + parent.Id;
                }
            }
            return treePath;
        }
        /// <summary>
        /// 获取树形结构列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetTreeList(bool isAll = false)
        {
            List<SysModule> moduleList = null;
            if (isAll)
            {
                moduleList= await _sysModuleService.GetQueryable().IgnoreQueryFilters()
                    .Where(m => m.IsDelete == false).ToListAsync();
            }
            else
            {
                moduleList = await _sysModuleService.GetQueryable().ToListAsync();
            }

            TreeNode treeNode = new TreeNode();
            TreeHelper.ListToTree(treeNode, moduleList);
        }
    }
}