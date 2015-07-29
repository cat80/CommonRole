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
    public partial class Role_resource
    {
        #region 构造函数
        // -----------------------------------------------------------------------------------------

        #region public Role_resource() |默认构造函数
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Role_resource()
        {
           this.ResourceName = string.Empty;
           this.Url = string.Empty;
        }
        // ------------------------------------------------------------------------------------------
        #endregion

        // ------------------------------------------------------------------------------------------
        #endregion

        #region 成员
        // -----------------------------------------------------------------------------------------
        private System.Int32       _resourceID;               //主键ID
        private System.String      _resourceName;             //资源名称
        private System.Int32       _parentID;                 //父级ID
        private System.Int32       _sort;                     //排序号
        private System.Int32       _showInMenu;               //是否在菜单中显示
        private System.Int32       _state;                    //是否启用
        private System.DateTime    _createTime;               //创建时间
        private System.String      _url;                      //链接地址
        // ------------------------------------------------------------------------------------------
        #endregion

        #region 属性
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 主键ID
        /// </summary>
        public System.Int32 ResourceID
        {
            set { _resourceID = value; }
            get { return _resourceID; }
            
        }
        
        /// <summary>
        /// 资源名称
        /// </summary>
        public System.String ResourceName
        {
            set { _resourceName = value; }
            get
            { 
                if( _resourceName == null){
                    return string.Empty;
                }
                return  _resourceName;
            }
            
        }
        
        /// <summary>
        /// 父级ID
        /// </summary>
        public System.Int32 ParentID
        {
            set { _parentID = value; }
            get { return _parentID; }
            
        }
        
        /// <summary>
        /// 排序号
        /// </summary>
        public System.Int32 Sort
        {
            set { _sort = value; }
            get { return _sort; }
            
        }
        
        /// <summary>
        /// 是否在菜单中显示
        /// </summary>
        public System.Int32 ShowInMenu
        {
            set { _showInMenu = value; }
            get { return _showInMenu; }
            
        }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public System.Int32 State
        {
            set { _state = value; }
            get { return _state; }
            
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
        /// 链接地址
        /// </summary>
        public System.String Url
        {
            set { _url = value; }
            get
            { 
                if( _url == null){
                    return string.Empty;
                }
                return  _url;
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
           return string.Format("ResourceID:{0},ResourceName:{1},ParentID:{2},Sort:{3},ShowInMenu:{4},State:{5},CreateTime:{6},Url:{7}",_resourceID,_resourceName,_parentID,_sort,_showInMenu,_state,_createTime,_url);         }
        //-------------------------------------------------------------------------------------------
        #endregion
    }
}