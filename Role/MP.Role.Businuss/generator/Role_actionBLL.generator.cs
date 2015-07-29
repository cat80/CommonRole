#region 文件描述

// 描述：业务逻辑层
// 作者：cat80
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
using System.Web;
using System.Web.Caching; 
using MP.Role.Entity;
using MP.Role.MySqlDataProvider; 

namespace MP.Role.Businuss
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public partial class Role_actionBLL: BllBase
    {
        #region 静态成员与属性
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 数据操作层
        /// </summary>
        private static Role_actionDAL  _Role_actionDal = new MP.Role.MySqlDataProvider.Role_actionDAL();

        /// <summary>
        /// 当前实例
        /// </summary>
        private static Role_actionBLL _Current = new Role_actionBLL();

        /// <summary>
        /// 当前实例
        /// </summary>
        public static Role_actionBLL Current
        {
            get { return _Current; }
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  private Role_actionBLL() //私有化构造函数
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private Role_actionBLL()
        {
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region public bool Update(Role_action entity) //新增一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(Role_action entity)
        {
            try 
            {
               bool result = _Role_actionDal.Append(entity);
               ClearCacheData();
               return result;
            }
            catch (Exception ex)
            {
                DealError(ex);
                return false;
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion
        #region public bool Update(Mp_role_action entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(Role_action entity)
        {
            try 
            {
               bool result = _Role_actionDal.Update(entity);
               ClearCacheData();
               return result; 
            }
            catch (Exception ex)
            {
                DealError(ex);
                return false;
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion
        
         #region public void Delete(System.Int32 actionID) //删除一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(System.Int32 actionID)
        {
          try 
            {
               bool result = _Role_actionDal.Delete(actionID);
               ClearCacheData();
               return result; 
            }
            catch (Exception ex)
            {
                DealError(ex);
                return false;
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion
        
        #region public Role_action GetByID(System.Int32 actionID) //根据ID获取记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据ID获取记录
        /// </summary>
        /// <param name="driverID">驱动ID</param>
        /// <returns></returns>
        public Role_action GetByID( System.Int32 actionID)
        {
            try 
            {
                return _Role_actionDal.GetByID(actionID);
            }
            catch (Exception ex)
            {
                DealError(ex);
                return null;
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion
          
        #region public List<Role_action> GetListPager(int pageSize, int pageIndex, out int recordCount); /// 获取分页列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public List<Role_action> GetListPager(int pageSize, int pageIndex, out int recordCount)
        {
            try
            {
                return _Role_actionDal.GetListPager(pageSize, pageIndex, out recordCount);
            }
            catch (Exception ex)
            {
                DealError(ex);
                recordCount = 0;
                return new List<Role_action>();
            } 
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion
        
        #region public void DeleteByIdList(List<int> idList) //根据主键批量删除仅适用主键只有一个，并且类型为整形的表
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据主键批量删除仅适用主键只有一个，并且类型为整形的表
        /// </summary>
        /// <param name="entity"></param>
        public bool DeleteByIdList(List<int> idList)
        {
            try
            {
            
                foreach (var item in idList)
                {
                    Delete(item);
                }
                return true;
                // return _Role_actionDal.DeleteByIdList(idList);
            }
            catch (Exception ex)
            {
                DealError(ex); 
                return false;
            } 
        }
        //------------------------------------------------------------------------------------------
        #endregion
        
        #region  public List<Role_action> CacheAllDataList()      /// 从缓存里面获取所有有的数据(默认缓存十分钟，对于数据量比较多的表慎用)
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 从缓存里面获取所有有的数据(默认缓存十分钟)
        /// </summary>
        /// <returns></returns>
        public List<Role_action> CacheAllDataList()
        {
            try
            {
                string cacheKey = "cache_all_Role_action";
                List<Role_action> list = HttpRuntime.Cache[cacheKey] as List<Role_action>;

                if (list == null)
                {

                    //多线程访问的时候，可能会造成阻塞的情况
                    lock (_Current)
                    {
                        if (list == null)
                        {

                            list = Current.GetListAll() as List<Role_action>;
                            HttpRuntime.Cache.Add(cacheKey, list, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero,
                                CacheItemPriority.Normal, null);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                DealError(ex);
                return new List<Role_action>();
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
        
        #region public void ClearCacheData()  /// 当数据有变量时清空所有缓存
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 当数据有变量时清空所有缓存
        /// </summary>
        public void ClearCacheData()
        {
            try
            {
                string cacheKey = "cache_all_Role_action";
                HttpRuntime.Cache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                DealError(ex);
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
        
                #region  public IList<Role_action> GetListAll() // 获取所有记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public IList<Role_action> GetListAll()
        {
         {
            try 
            {
                return _Role_actionDal.GetListAll();
            }
            catch (Exception ex)
            {
                DealError(ex);
                return new List<Role_action>();
            }
        }
        }
        //------------------------------------------------------------------------------------------
        #endregion
    }
}
