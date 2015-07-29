#region 文件描述

// 描述：实体类
// 作者：苏志平
// 时间：2013-11-04 19:47

#endregion

#region 类修改记录 : 每次修改一组描述

// 修改描述：
// 修 改 人：
// 修改时间：

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MP.Role.Entity
{
    /// <summary>
    /// 实体类
    /// </summary>
    public partial class Role_action
    {
        #region 构造函数
        // -----------------------------------------------------------------------------------------

        #region public Role_action() |默认构造函数
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Role_action()
        {
           this.ControllerName = string.Empty;
           this.ActionName = string.Empty;
           this.DisplayName = string.Empty;
           this.Remarks = string.Empty;
        }
        // ------------------------------------------------------------------------------------------
        #endregion

        // ------------------------------------------------------------------------------------------
        #endregion

        #region 成员
        // -----------------------------------------------------------------------------------------
        private System.Int32       _actionID;                 //主键ID
        private System.Int32       _resourceID;               //action对应控制器ID
        private System.String      _controllerName;           //控制器名称
        private System.String      _actionName;               //action请求权限字段
        private System.String      _displayName;              //显示名称
        private System.Int32       _sort;                     //在菜单的位置
        private System.SByte       _status;                   //状态0，启用,1禁用
        private System.DateTime    _createTime;               //创建时间
        private System.SByte       _isShowInMenu;             //是否在菜单中显示
        private System.DateTime    _lastModifyTime;           //最后修改时间
        private System.String      _remarks;                  //备注
        // ------------------------------------------------------------------------------------------
        #endregion

        #region 属性
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Int32 ActionID
        {
            set { _actionID = value; }
            get { return _actionID; }
            
        }
        
        /// <summary>
        /// action对应控制器ID
        /// </summary>
        public System.Int32 ResourceID
        {
            set { _resourceID = value; }
            get { return _resourceID; }
            
        }
        
        /// <summary>
        /// 控制器名称
        /// </summary>
        public System.String ControllerName
        {
            set { _controllerName = value; }
            get
            { 
                if( _controllerName == null){
                    return string.Empty;
                }
                return  _controllerName;
            }
            
        }
        
        /// <summary>
        /// action请求权限字段
        /// </summary>
        public System.String ActionName
        {
            set { _actionName = value; }
            get
            { 
                if( _actionName == null){
                    return string.Empty;
                }
                return  _actionName;
            }
            
        }
        
        /// <summary>
        /// 显示名称
        /// </summary>
        public System.String DisplayName
        {
            set { _displayName = value; }
            get
            { 
                if( _displayName == null){
                    return string.Empty;
                }
                return  _displayName;
            }
            
        }
        
        /// <summary>
        /// 在菜单的位置
        /// </summary>
        public System.Int32 Sort
        {
            set { _sort = value; }
            get { return _sort; }
            
        }
        
        /// <summary>
        /// 状态0，启用,1禁用
        /// </summary>
        public System.SByte Status
        {
            set { _status = value; }
            get { return _status; }
            
        }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
            
        }
        
        /// <summary>
        /// 是否在菜单中显示
        /// </summary>
        public System.SByte IsShowInMenu
        {
            set { _isShowInMenu = value; }
            get { return _isShowInMenu; }
            
        }
        
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public System.DateTime LastModifyTime
        {
            set { _lastModifyTime = value; }
            get { return _lastModifyTime; }
            
        }
        
        /// <summary>
        /// 备注
        /// </summary>
        public System.String Remarks
        {
            set { _remarks = value; }
            get
            { 
                if( _remarks == null){
                    return string.Empty;
                }
                return  _remarks;
            }
            
        }
        
        
        // ------------------------------------------------------------------------------------------
        #endregion
        
        #region  public override string ToString()  重写的ToString方法
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// 重写的ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {   
           return string.Format("ActionID:{0},ResourceID:{1},ControllerName:{2},ActionName:{3},DisplayName:{4},Sort:{5},Status:{6},CreateTime:{7},IsShowInMenu:{8},LastModifyTime:{9},Remarks:{10}",_actionID,_resourceID,_controllerName,_actionName,_displayName,_sort,_status,_createTime,_isShowInMenu,_lastModifyTime,_remarks);         }
        //-------------------------------------------------------------------------------------------
        #endregion
    }
}