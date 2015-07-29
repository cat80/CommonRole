using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MP.Role.Businuss;
using Common;
using MP.Role.Entity;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Common.Util;
using CommonRole.MVCWebSite.Controllers;
using MP.Role.Businuss.Filters;

namespace MP.MVCWebSite.Controllers
{ 
    [Description("角色管理")]
    [RoleFilter]
    public class RoleController : Controller
    {   
        public ActionResult Index()
        {
            return View();
        }

        #region 资源管理
        //------------------------------------------------------------------------------------------------------------
        [Description("资源列表")]
        public ActionResult Resources()
        {
            return View(Role_resourceBLL.Current.CacheAllDataList().OrderByDescending(item => item.Sort).ToList());


        }

        [Description("保存资源")]
        public ActionResult SaveResource()
        {

            List<Role_resource> newResource = new List<Role_resource>();
            List<Role_resource> updateResource = new List<Role_resource>();

            foreach (string item in Request.Form.Keys)
            {
                Match match = Regex.Match(item, @"ResourceName\[(?<id>\d+)\]");
                if (match.Success)
                {
                    int id = Convert.ToInt32(match.Groups["id"].Value);
                    Role_resource res = new Role_resource();
                    res.ResourceID = id;
                    res.ResourceName = DTLRequest.GetString(string.Format("ResourceName[{0}]", id));
                    res.ParentID = DTLRequest.GetInt(string.Format("ParentID[{0}]", id), 0);
                    res.Url = DTLRequest.GetString(string.Format("Url[{0}]", id));
                    res.Sort = DTLRequest.GetInt(string.Format("Sort[{0}]", id), 0);
                    res.State = DTLRequest.GetInt(string.Format("State[{0}]", id), 0);
                    res.ShowInMenu = DTLRequest.GetInt(string.Format("ShowInMenu[{0}]", id), 0);
                    updateResource.Add(res);
                }
            }

            if (!string.IsNullOrEmpty(DTLRequest.GetString("new[ResourceName]")))
            {
                string[] names = DTLRequest.GetString("new[ResourceName]").Split(',');
                string[] urls = DTLRequest.GetString("new[Url]").Split(',');
                string[] sorts = DTLRequest.GetString("new[Sort]").Split(',');
                string[] states = DTLRequest.GetString("new[State]").Split(',');
                string[] parents = DTLRequest.GetString("new[ParentID]").Split(',');
                string[] showmenus = DTLRequest.GetString("new[ShowInMenu]").Split(',');
                for (int i = 0; i < names.Length; i++)
                {
                    try
                    {
                        Role_resource res = new Role_resource();

                        res.ResourceName = names[i];
                        res.Url = urls[i];
                        int temp = 0;
                        if (int.TryParse(sorts[i], out temp))
                        {
                            res.Sort = temp;
                        }

                        if (int.TryParse(states[i], out temp))
                        {
                            res.State = temp;
                        }

                        if (int.TryParse(parents[i], out temp))
                        {
                            res.ParentID = temp;
                        }
                        if (int.TryParse(showmenus[i], out temp))
                        {
                            res.ShowInMenu = temp;

                        }

                        if (!string.IsNullOrEmpty(res.ResourceName))
                        {
                            newResource.Add(res);
                        }
                    }
                    catch (Exception ex)
                    {

                        Log.Default.Info(ex);
                    }

                }
            }
            foreach (var item in newResource)
            {
                Role_resourceBLL.Current.Append(item);
            }

            foreach (var item in updateResource)
            {
                Role_resourceBLL.Current.Update(item);
            }
            return CommonResult.ShowMessage("保存成功！", Url.Action("resources"));

        }
        [Description("删除资源")]
        public ActionResult DeleteResources(int id)
        {
            Role_resourceBLL.Current.DeleteAndSub(id);
            return CommonResult.ShowMessage("删除成功", Url.Action("resources"));
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region 角色管理
        //------------------------------------------------------------------------------------------------------------
        [Description("角色名称")]
        public ActionResult Groups()
        {
            return View(Role_groupBLL.Current.CacheAllDataList().OrderBy(item => item.GroupName).ToList());
        }

        [Description("保存角色信息")]
        public ActionResult SaveGroups()
        {

            //            ResourceName[2]	B角色
            //State[2]	0
            List<Role_group> newRole = new List<Role_group>();
            List<Role_group> updateRole = new List<Role_group>();

            foreach (string item in Request.Form.Keys)
            {
                Match match = Regex.Match(item, @"GroupName\[(?<id>\d+)\]");
                if (match.Success)
                {
                    int id = Convert.ToInt32(match.Groups["id"].Value);
                    Role_group group = new Role_group();
                    group.GroupID = id;
                    group.GroupName = DTLRequest.GetString(string.Format("GroupName[{0}]", id));
                    group.State = DTLRequest.GetInt(string.Format("State[{0}]", id), 0);
                    updateRole.Add(group);
                }
            }

            if (!string.IsNullOrEmpty(DTLRequest.GetString("new[GroupName]")))
            {
                string[] names = DTLRequest.GetString("new[GroupName]").Split(',');

                string[] states = DTLRequest.GetString("new[State]").Split(',');

                for (int i = 0; i < names.Length; i++)
                {
                    try
                    {
                        Role_group role = new Role_group();

                        role.GroupName = names[i];

                        int temp = 0;
                        if (int.TryParse(states[i], out temp))
                        {
                            role.State = temp;
                        }

                        if (!string.IsNullOrEmpty(role.GroupName))
                        {
                            newRole.Add(role);
                        }
                    }
                    catch (Exception ex)
                    {

                        Log.Default.Info(ex);
                    }

                }
            }
            foreach (var item in newRole)
            {
                Role_groupBLL.Current.Append(item);
            }

            foreach (var item in updateRole)
            {
                Role_groupBLL.Current.UpdateBaseInfo(item);
            }
            return CommonResult.ShowMessage("保存成功！", Url.Action("groups"));

        }

        public ActionResult DeleteGroups(int id)
        {
            Role_groupBLL.Current.Delete(id);
            return CommonResult.ShowMessage("删除成功", Url.Action("groups"));
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region 功能管理
        //------------------------------------------------------------------------------------------------------------

        [Description("功能列表")]
        public ActionResult Actions(string keyword, int? resid, int? page, int? size)
        {
            ViewBag.ResourceList = GetResourceList();
            int pageindex = page ?? 1;
            int pagesize = size ?? 10;
            int recordCount = 0;
            var list = Role_actionBLL.Current.GetPageList(keyword, resid, pagesize, pageindex, out recordCount);
            ViewBag.RecordCount = recordCount;
            ViewBag.PagerInfo = new RenderPager(pageindex, pagesize, recordCount, Request.Url.AbsoluteUri).Render();
            return View(list);
        }

        private List<Role_resource> GetResourceList()
        {

            var roleList = Role_resourceBLL.Current.CacheAllDataList().OrderByDescending(item => item.Sort).ToList();
            var allParents = roleList.Where(item => item.ParentID == 0).ToList();
            var newList = new List<Role_resource>();
            foreach (var item in allParents)
            {
                newList.Add(item);
                newList.AddRange(roleList.Where(cate => cate.ParentID == item.ResourceID).Select(res => new Role_resource()
                {
                    ResourceID = res.ResourceID,
                    ResourceName = "　┠" + res.ResourceName
                }));
            }
            return newList;
        }

        /// <summary>
        /// 编辑action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Description("编辑Action")]
        public ActionResult EditAction(int? id)
        {
            ViewBag.ResourceList = new SelectList(GetResourceList(), "ResourceID", "ResourceName");
            Role_action action = null;
            if (id.HasValue)
            {
                action = Role_actionBLL.Current.GetByID(id.Value);
            }
            return View(action);
        }

        [Description("保存Action")]
        public ActionResult SaveAction(int? ActionID)
        {
            string backUrl = Url.Action("actions");
            Role_action entity = new Role_action();
            string[] allowProperty = new string[] { "ControllerName", "ActionName", "ResourceID", "DisplayName", "Sort", "Status", "IsShowInMenu" };

            PageCommandType type = PageCommandType.增加;
            if (ActionID.HasValue)
            {
                //目前只考虑主键只有一个，且为自增长 
                entity = Role_actionBLL.Current.GetByID(ActionID.Value);
                if (entity != null)
                {
                    type = PageCommandType.更新;
                }
            }
            TryUpdateModel(entity, allowProperty);

            entity.LastModifyTime = DateTime.Now;
            entity.ControllerName = (entity.ControllerName ?? string.Empty).Trim();
            entity.ActionName = (entity.ActionName ?? string.Empty).Trim();
            //增加操作
            if (type == PageCommandType.增加)
            {

                if (Role_actionBLL.Current.Append(entity))
                {
                    return CommonResult.ShowMessage("增加成功", string.Format("/role/editaction/{0}", entity.ActionID));
                }
            }
            else if (type == PageCommandType.更新)
            {


                if (Role_actionBLL.Current.Update(entity))
                {
                    return CommonResult.ShowMessage("更新成功", string.Format("/role/editaction/{0}", entity.ActionID));
                }
            }
            return CommonResult.ShowMessage("保存失败", backUrl);
        }

        [Description("删除Action")]
        public ActionResult DeleteAction(int id)
        {
            Role_actionBLL.Current.Delete(id);
            return CommonResult.ShowMessage("更新成功!", "/role/actions");
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


        #region 角色权限管理
        //------------------------------------------------------------------------------------------------------------
        [Description("角色权限列表")]
        public ActionResult GroupActions(int? id)
        {
            Role_group group = null;
            if (id.HasValue)
            {
                group = Role_groupBLL.Current.GetByID(id.Value);

            }
            ViewBag.AllGroups = Role_groupBLL.Current.CacheAllDataList().OrderBy(item => item.GroupName).ToList();
            ViewBag.AllActions = Role_actionBLL.Current.CacheAllDataList().OrderByDescending(item => item.Sort).ToList();
            ViewBag.AllResources = Role_resourceBLL.Current.CacheAllDataList().OrderByDescending(item => item.Sort).ToList();
            ViewBag.CurrentGroupID = id ?? -1;
            return View(group);


        }
        [HttpPost]
        [Description("保存角色权限")]
        public ActionResult SaveGroupActions(int GroupID)
        {
            Role_group group = Role_groupBLL.Current.GetByID(GroupID);
            if (group == null)
            {
                return CommonResult.JsonMessage(0, "角色权限不存在！");
            }
            group.Auth_Resource = string.Join(",", DTLRequest.GetRequestIntList("ckResMenu").Distinct());
            group.Auth_Action = string.Join(",", DTLRequest.GetRequestIntList("ckAction").Distinct());
            if (Role_groupBLL.Current.Update(group))
            {
                return CommonResult.JsonMessage(1, "更新成功！");
            }
            else
            {
                return CommonResult.JsonMessage(0, "更新失败，请稍后再试！");
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


    }
}
