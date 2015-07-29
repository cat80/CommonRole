using Common;
using Common.Util;
using CommonRole.MVCWebSite.Controllers;
using MP.Role.Businuss;
using MP.Role.Businuss.Filters;
using MP.Role.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonRole.Controllers
{
    [RoleFilter]
    public partial class ControlPanelController : Controller
    {

        // GET: ControlPanel
        public ActionResult Index()
        {
            return View();
        }

        [Description("用户列表")]
        public ActionResult AdminList(string keyword, int? status, int? level, int? page, int? size)
        {
            int pageindex = page ?? 1;
            int pagesize = size ?? 10;
            int recordCount = 0;
            bool? isExpireOnly = null;
            int? creatorUser = null;
            if (status.HasValue && status.Value == 3)
            {
                isExpireOnly = true;
                status = null;
            }

            //if (CurrentUserInfo.IsBrandAgentOrBrandOem) //如果为品牌代理商或品牌OEM
            //{
            //    level = (int)AdminLevel.技术制作人员;
            //}
            var list = User_infoBLL.Current.GetAdminList(creatorUser, 0, keyword, isExpireOnly, status, level, pagesize, pageindex, out recordCount);
            ViewBag.RecordCount = recordCount;
            ViewBag.PagerInfo = new RenderPager(pageindex, pagesize, recordCount, Request.Url.AbsoluteUri).Render();
            return View(list);
        }

        [Description("编辑管理员")]
        public ActionResult EditAdmin(int? id)
        {
            User_info user = null;
            if (id.HasValue)
            {
                user = User_infoBLL.Current.GetByID(id.Value);

            }
            ViewBag.GroupList = Role_groupBLL.Current.CacheAllDataList();

            return View(user);
        }

        [Description("保存管理员信息")]
        public ActionResult SaveAdmin(int? UserID)
        {
            string backUrl = Url.Action("adminlist");
            User_info entity = new User_info();
            string[] allowProperty = new string[] { "UserName", "UserLoginName", "Level", "Status", "MobliePhone", "Email", "MaxCreateNumber", "ExpireTime", "Province", "City", "MakeRate", "NotMakeRate" };

            PageCommandType type = PageCommandType.增加;
            if (UserID.HasValue)
            {
                type = PageCommandType.更新;
                //目前只考虑主键只有一个，且为自增长 
                entity = User_infoBLL.Current.GetByID(UserID.Value);

            }
            TryUpdateModel(entity, allowProperty);

            entity.LastModifyTime = DateTime.Now;
            //end
            User_info checkNameStore = User_infoBLL.Current.GetByUserLoginName(entity.UserLoginName);
            //验证
            if (string.IsNullOrEmpty(entity.UserLoginName))
            {
                return CommonResult.ShowMessage("登陆名不能为空！", backUrl);
            }




            var groupsID = DTLRequest.GetRequestIntList("groups").Distinct();
            entity.GroupNames = string.Join(",", Role_groupBLL.Current.CacheAllDataList().Where(item => groupsID.Contains(item.GroupID)));
            entity.Groups = string.Join(",", groupsID);
            entity.GroupNames = string.Join(",", Role_groupBLL.Current.CacheAllDataList().Where(item => entity.UsersGroupsList.Contains(item.GroupID)).Select(item => item.GroupName));

            string passwrod = DTLRequest.GetString("UserPassword");

            int? createUserID = null;// CurrentUserInfo.CurrentUser.UserID;
            //增加操作
            if (type == PageCommandType.增加)
            {
                if (string.IsNullOrEmpty(passwrod))
                {
                    return CommonResult.ShowMessage("登陆密码不能空！", backUrl);
                }

                if (checkNameStore != null)
                {
                    return CommonResult.ShowMessage("已经存在账号登陆名", backUrl);
                }

                entity.CreateTime = DateTime.Now;
                entity.UserPassword = Utils.MD5(passwrod);

                entity.CreateUser = createUserID ?? CurrentUserInfo.CurrentUser.UserID;

                if (User_infoBLL.Current.Append(entity))
                {
                    return CommonResult.ShowMessage("增加成功", backUrl);
                }
            }
            else if (type == PageCommandType.更新)
            {
                if (createUserID.HasValue)
                {
                    entity.CreateUser = createUserID.Value;
                }
                if (!string.IsNullOrEmpty(passwrod))
                {
                    entity.UserPassword = Utils.MD5(passwrod);
                }
                if (checkNameStore != null && entity.UserID != checkNameStore.UserID)
                {
                    return CommonResult.ShowMessage("已经存在账号登陆名", backUrl);
                }

                if (User_infoBLL.Current.Update(entity))
                {
                    return CommonResult.ShowMessage("更新成功", backUrl);
                }
            }
            return CommonResult.ShowMessage("保存失败", backUrl);
        }


        [Description("删除管理员")]
        public ActionResult DeleteAdmin(int id)
        {
            string backUrl = "/controlpanel/adminlist";
            User_info user = User_infoBLL.Current.GetByID(id);
            user.Deleted = 1;
            User_infoBLL.Current.Update(user);
            return CommonResult.ShowMessage("删除成功", backUrl);
        }
    }
}