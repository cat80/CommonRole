#region 文件描述

// 描述：业务逻辑层
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
using System.Web;
using System.Web.Caching; 
using MP.Role.Entity;
using MP.Role.MySqlDataProvider; 

namespace MP.Role.Businuss
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public partial class Role_resourceBLL: BllBase
    {
        #region 静态成员与属性
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 数据操作层
        /// </summary>
        private static Role_resourceDAL  _Role_resourceDal = new MP.Role.MySqlDataProvider.Role_resourceDAL();

        /// <summary>
        /// 当前实例
        /// </summary>
        private static Role_resourceBLL _Current = new Role_resourceBLL();

        /// <summary>
        /// 当前实例
        /// </summary>
        public static Role_resourceBLL Current
        {
            get { return _Current; }
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  private Role_resourceBLL() //私有化构造函数
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private Role_resourceBLL()
        {
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region public bool Update(Role_resource entity) //新增一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(Role_resource entity)
        {
            try 
            {
               bool result = _Role_resourceDal.Append(entity);
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
        #region public bool Update(Mp_role_resource entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(Role_resource entity)
        {
            try 
            {
               bool result = _Role_resourceDal.Update(entity);
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
        
         #region public void Delete(System.Int32 resourceID) //删除一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(System.Int32 resourceID)
        {
          try 
            {
               bool result = _Role_resourceDal.Delete(resourceID);
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
        
        #region public Role_resource GetByID(System.Int32 resourceID) //根据ID获取记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据ID获取记录
        /// </summary>
        /// <param name="driverID">驱动ID</param>
        /// <returns></returns>
        public Role_resource GetByID( System.Int32 resourceID)
        {
            try 
            {
                return _Role_resourceDal.GetByID(resourceID);
            }
            catch (Exception ex)
            {
                DealError(ex);
                return null;
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion
          
        #region public List<Role_resource> GetListPager(int pageSize, int pageIndex, out int recordCount); /// 获取分页列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public List<Role_resource> GetListPager(int pageSize, int pageIndex, out int recordCount)
        {
            try
            {
                return _Role_resourceDal.GetListPager(pageSize, pageIndex, out recordCount);
            }
            catch (Exception ex)
            {
                DealError(ex);
                recordCount = 0;
                return new List<Role_resource>();
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
                // return _Role_resourceDal.DeleteByIdList(idList);
            }
            catch (Exception ex)
            {
                DealError(ex); 
                return false;
            } 
        }
        //------------------------------------------------------------------------------------------
        #endregion
        
        #region  public List<Role_resource> CacheAllDataList()      /// 从缓存里面获取所有有的数据(默认缓存十分钟，对于数据量比较多的表慎用)
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 从缓存里面获取所有有的数据(默认缓存十分钟)
        /// </summary>
        /// <returns></returns>
        public List<Role_resource> CacheAllDataList()
        {
            try
            {
                string cacheKey = "cache_all_Role_resource";
                List<Role_resource> list = HttpRuntime.Cache[cacheKey] as List<Role_resource>;

                if (list == null)
                {

                    //多线程访问的时候，可能会造成阻塞的情况
                    lock (_Current)
                    {
                        if (list == null)
                        {

                            list = Current.GetListAll() as List<Role_resource>;
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
                return new List<Role_resource>();
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
                string cacheKey = "cache_all_Role_resource";
                HttpRuntime.Cache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                DealError(ex);
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
        
                #region  public IList<Role_resource> GetListAll() // 获取所有记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public IList<Role_resource> GetListAll()
        {
         {
            try 
            {
                return _Role_resourceDal.GetListAll();
            }
            catch (Exception ex)
            {
                DealError(ex);
                return new List<Role_resource>();
            }
        }
        }
        //------------------------------------------------------------------------------------------
        #endregion
    }
}
