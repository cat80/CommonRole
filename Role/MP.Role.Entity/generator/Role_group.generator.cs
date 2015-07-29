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
    public partial class Role_group
    {
        #region 构造函数
        // -----------------------------------------------------------------------------------------

        #region public Role_group() |默认构造函数
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Role_group()
        {
           this.GroupName = string.Empty;
           this.Auth_Action = string.Empty;
           this.Auth_Resource = string.Empty;
        }
        // ------------------------------------------------------------------------------------------
        #endregion

        // ------------------------------------------------------------------------------------------
        #endregion

        #region 成员
        // -----------------------------------------------------------------------------------------
        private System.Int32       _groupID;                  //分组ID
        private System.String      _groupName;                //分组名称
        private System.DateTime    _createTime;               //创建时间
        private System.String      _auth_Action;              //分组的action权限列表(,分隔)
        private System.String      _auth_Resource;            //用户可访问资源列表(,分隔)
        private System.Int32       _state;                    //角色状态
        // ------------------------------------------------------------------------------------------
        #endregion

        #region 属性
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 分组ID
        /// </summary>
        public System.Int32 GroupID
        {
            set { _groupID = value; }
            get { return _groupID; }
            
        }
        
        /// <summary>
        /// 分组名称
        /// </summary>
        public System.String GroupName
        {
            set { _groupName = value; }
            get
            { 
                if( _groupName == null){
                    return string.Empty;
                }
                return  _groupName;
            }
            
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
        /// 分组的action权限列表(,分隔)
        /// </summary>
        public System.String Auth_Action
        {
            set { _auth_Action = value; }
            get
            { 
                if( _auth_Action == null){
                    return string.Empty;
                }
                return  _auth_Action;
            }
            
        }
        
        /// <summary>
        /// 用户可访问资源列表(,分隔)
        /// </summary>
        public System.String Auth_Resource
        {
            set { _auth_Resource = value; }
            get
            { 
                if( _auth_Resource == null){
                    return string.Empty;
                }
                return  _auth_Resource;
            }
            
        }
        
        /// <summary>
        /// 角色状态
        /// </summary>
        public System.Int32 State
        {
            set { _state = value; }
            get { return _state; }
            
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
           return string.Format("GroupID:{0},GroupName:{1},CreateTime:{2},Auth_Action:{3},Auth_Resource:{4},State:{5}",_groupID,_groupName,_createTime,_auth_Action,_auth_Resource,_state);         }
        //-------------------------------------------------------------------------------------------
        #endregion
    }
}