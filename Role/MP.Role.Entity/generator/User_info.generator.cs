#region 文件描述

// 描述：实体类
// 作者：苏志萍
// 时间：2013-11-29 14:35

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
    public partial class User_info
    {
        #region 构造函数
        // -----------------------------------------------------------------------------------------

        #region public User_info() |默认构造函数
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public User_info()
        {
           this.UserLoginName = string.Empty;
           this.UserPassword = string.Empty;
           this.UserName = string.Empty;
           this.IsAdmin = string.Empty;
           this.Description = string.Empty;
           this.ModifiedUserID = string.Empty;
           this.MobliePhone = string.Empty;
           this.WorkPhone = string.Empty;
           this.Email = string.Empty;
           this.Address = string.Empty;
           this.Groups = string.Empty;
           this.Province = string.Empty;
           this.City = string.Empty;
           this.GroupNames = string.Empty;
        }
        // ------------------------------------------------------------------------------------------
        #endregion

        // ------------------------------------------------------------------------------------------
        #endregion

        #region 成员
        // -----------------------------------------------------------------------------------------
        private System.Int32       _userID;                   //用户编号
        private System.Int32       _deptID;                   //部门ID
        private System.String      _userLoginName;            //用户名登陆名
        private System.String      _userPassword;             //密码
        private System.String      _userName;                 //用户名
        private System.String      _isAdmin;                  //是否为员
        private System.String      _description;              //描述
        private System.DateTime    _createTime;               //登陆时间
        private System.DateTime    _lastModifyTime;           //最后登陆时间
        private System.String      _modifiedUserID;           //修改人ID
        private System.String      _mobliePhone;              //联系电话
        private System.String      _workPhone;                //工作电话
        private System.String      _email;                    //电子邮箱
        private System.Int32       _status;                   //状态.0。禁用。1.激活
        private System.String      _address;                  //联系地址
        private System.Int32       _deleted;                  //是否已经删除
        private System.String      _groups;                   //所属于分组
        private System.Int32       _level;                    //管理员等级（0,初级管理员，1,高级管理)
        private System.Int32       _maxCreateNumber;          //最大创建数量
        private System.DateTime    _expireTime;               //到期日期
        private System.String      _province;                 //所属于省份
        private System.String      _city;                     //所属于城市
        private System.String      _groupNames;               //所属分组名
        private System.Decimal     _makeRate;                 //创建商家包制作收费比例
        private System.Decimal     _notMakeRate;              //创建商家不制作收费比较
        private System.Decimal     _agentRate;                //发展代理的转账折扣率
        private System.Int32       _createUser;               //创建者用户
        // ------------------------------------------------------------------------------------------
        #endregion

        #region 属性
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 用户编号
        /// </summary>
        public System.Int32 UserID
        {
            set { _userID = value; }
            get { return _userID; }
            
        }
        
        /// <summary>
        /// 部门ID
        /// </summary>
        public System.Int32 DeptID
        {
            set { _deptID = value; }
            get { return _deptID; }
            
        }
        
        /// <summary>
        /// 用户名登陆名
        /// </summary>
        public System.String UserLoginName
        {
            set { _userLoginName = value; }
            get
            { 
                if( _userLoginName == null){
                    return string.Empty;
                }
                return  _userLoginName;
            }
            
        }
        
        /// <summary>
        /// 密码
        /// </summary>
        public System.String UserPassword
        {
            set { _userPassword = value; }
            get
            { 
                if( _userPassword == null){
                    return string.Empty;
                }
                return  _userPassword;
            }
            
        }
        
        /// <summary>
        /// 用户名
        /// </summary>
        public System.String UserName
        {
            set { _userName = value; }
            get
            { 
                if( _userName == null){
                    return string.Empty;
                }
                return  _userName;
            }
            
        }
        
        /// <summary>
        /// 是否为员
        /// </summary>
        public System.String IsAdmin
        {
            set { _isAdmin = value; }
            get
            { 
                if( _isAdmin == null){
                    return string.Empty;
                }
                return  _isAdmin;
            }
            
        }
        
        /// <summary>
        /// 描述
        /// </summary>
        public System.String Description
        {
            set { _description = value; }
            get
            { 
                if( _description == null){
                    return string.Empty;
                }
                return  _description;
            }
            
        }
        
        /// <summary>
        /// 登陆时间
        /// </summary>
        public System.DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
            
        }
        
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public System.DateTime LastModifyTime
        {
            set { _lastModifyTime = value; }
            get { return _lastModifyTime; }
            
        }
        
        /// <summary>
        /// 修改人ID
        /// </summary>
        public System.String ModifiedUserID
        {
            set { _modifiedUserID = value; }
            get
            { 
                if( _modifiedUserID == null){
                    return string.Empty;
                }
                return  _modifiedUserID;
            }
            
        }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        public System.String MobliePhone
        {
            set { _mobliePhone = value; }
            get
            { 
                if( _mobliePhone == null){
                    return string.Empty;
                }
                return  _mobliePhone;
            }
            
        }
        
        /// <summary>
        /// 工作电话
        /// </summary>
        public System.String WorkPhone
        {
            set { _workPhone = value; }
            get
            { 
                if( _workPhone == null){
                    return string.Empty;
                }
                return  _workPhone;
            }
            
        }
        
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public System.String Email
        {
            set { _email = value; }
            get
            { 
                if( _email == null){
                    return string.Empty;
                }
                return  _email;
            }
            
        }
        
        /// <summary>
        /// 状态.0。禁用。1.激活
        /// </summary>
        public System.Int32 Status
        {
            set { _status = value; }
            get { return _status; }
            
        }
        
        /// <summary>
        /// 联系地址
        /// </summary>
        public System.String Address
        {
            set { _address = value; }
            get
            { 
                if( _address == null){
                    return string.Empty;
                }
                return  _address;
            }
            
        }
        
        /// <summary>
        /// 是否已经删除
        /// </summary>
        public System.Int32 Deleted
        {
            set { _deleted = value; }
            get { return _deleted; }
            
        }
        
        /// <summary>
        /// 所属于分组
        /// </summary>
        public System.String Groups
        {
            set { _groups = value; }
            get
            { 
                if( _groups == null){
                    return string.Empty;
                }
                return  _groups;
            }
            
        }
        
        /// <summary>
        /// 管理员等级（0,初级管理员，1,高级管理)
        /// </summary>
        public System.Int32 Level
        {
            set { _level = value; }
            get { return _level; }
            
        }
        
        /// <summary>
        /// 最大创建数量
        /// </summary>
        public System.Int32 MaxCreateNumber
        {
            set { _maxCreateNumber = value; }
            get { return _maxCreateNumber; }
            
        }
        
        /// <summary>
        /// 到期日期
        /// </summary>
        public System.DateTime ExpireTime
        {
            set { _expireTime = value; }
            get { return _expireTime; }
            
        }
        
        /// <summary>
        /// 所属于省份
        /// </summary>
        public System.String Province
        {
            set { _province = value; }
            get
            { 
                if( _province == null){
                    return string.Empty;
                }
                return  _province;
            }
            
        }
        
        /// <summary>
        /// 所属于城市
        /// </summary>
        public System.String City
        {
            set { _city = value; }
            get
            { 
                if( _city == null){
                    return string.Empty;
                }
                return  _city;
            }
            
        }
        
        /// <summary>
        /// 所属分组名
        /// </summary>
        public System.String GroupNames
        {
            set { _groupNames = value; }
            get
            { 
                if( _groupNames == null){
                    return string.Empty;
                }
                return  _groupNames;
            }
            
        }
        
        /// <summary>
        /// 创建商家包制作收费比例
        /// </summary>
        public System.Decimal MakeRate
        {
            set { _makeRate = value; }
            get { return _makeRate; }
            
        }
        
        /// <summary>
        /// 创建商家不制作收费比较
        /// </summary>
        public System.Decimal NotMakeRate
        {
            set { _notMakeRate = value; }
            get { return _notMakeRate; }
            
        }
        
        /// <summary>
        /// 发展代理的转账折扣率
        /// </summary>
        public System.Decimal AgentRate
        {
            set { _agentRate = value; }
            get { return _agentRate; }
            
        }
        
        /// <summary>
        /// 创建者用户
        /// </summary>
        public System.Int32 CreateUser
        {
            set { _createUser = value; }
            get { return _createUser; }
            
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
           return string.Format("UserID:{0},DeptID:{1},UserLoginName:{2},UserPassword:{3},UserName:{4},IsAdmin:{5},Description:{6},CreateTime:{7},LastModifyTime:{8},ModifiedUserID:{9},MobliePhone:{10},WorkPhone:{11},Email:{12},Status:{13},Address:{14},Deleted:{15},Groups:{16},Level:{17},MaxCreateNumber:{18},ExpireTime:{19},Province:{20},City:{21},GroupNames:{22},MakeRate:{23},NotMakeRate:{24},AgentRate:{25},CreateUser:{26}",_userID,_deptID,_userLoginName,_userPassword,_userName,_isAdmin,_description,_createTime,_lastModifyTime,_modifiedUserID,_mobliePhone,_workPhone,_email,_status,_address,_deleted,_groups,_level,_maxCreateNumber,_expireTime,_province,_city,_groupNames,_makeRate,_notMakeRate,_agentRate,_createUser);         }
        //-------------------------------------------------------------------------------------------
        #endregion
    }
}