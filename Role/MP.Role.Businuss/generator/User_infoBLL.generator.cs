#region 文件描述

// 描述：业务逻辑层
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
using System.Web;
using System.Web.Caching;
using MP.Role.Entity; 
using MP.Role.MySqlDataProvider;

namespace MP.Role.Businuss
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public partial class User_infoBLL
    {
        #region 静态成员与属性
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 数据操作层
        /// </summary>
        private static User_infoDAL _User_infoDal = new MP.Role.MySqlDataProvider.User_infoDAL();

        /// <summary>
        /// 当前实例
        /// </summary>
        private static User_infoBLL _Current = new User_infoBLL();

        /// <summary>
        /// 当前实例
        /// </summary>
        public static User_infoBLL Current
        {
            get { return _Current; }
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  private User_infoBLL() //私有化构造函数
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private User_infoBLL()
        {
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region public bool Update(User_info entity) //新增一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(User_info entity)
        {
            try
            {
                bool result = _User_infoDal.Append(entity);
                ClearCacheData();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion
        #region public bool Update(Mp_user_info entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(User_info entity)
        {
            try
            {
                bool result = _User_infoDal.Update(entity);
                ClearCacheData();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region public void Delete(System.Int32 userID) //删除一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(System.Int32 userID)
        {
            try
            {
                bool result = _User_infoDal.Delete(userID);
                ClearCacheData();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region public User_info GetByID(System.Int32 userID) //根据ID获取记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据ID获取记录
        /// </summary>
        /// <param name="driverID">驱动ID</param>
        /// <returns></returns>
        public User_info GetByID(System.Int32 userID)
        {
            try
            {
                return _User_infoDal.GetByID(userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region public List<User_info> GetListPager(int pageSize, int pageIndex, out int recordCount); /// 获取分页列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public List<User_info> GetListPager(int pageSize, int pageIndex, out int recordCount)
        {
            try
            {
                return _User_infoDal.GetListPager(pageSize, pageIndex, out recordCount);
            }
            catch (Exception ex)
            {
                throw ex;
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
                // return _User_infoDal.DeleteByIdList(idList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region  public List<User_info> CacheAllDataList()      /// 从缓存里面获取所有有的数据(默认缓存十分钟，对于数据量比较多的表慎用)
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 从缓存里面获取所有有的数据(默认缓存十分钟)
        /// </summary>
        /// <returns></returns>
        public List<User_info> CacheAllDataList()
        {
            try
            {
                string cacheKey = "cache_all_User_info";
                List<User_info> list = HttpRuntime.Cache[cacheKey] as List<User_info>;

                if (list == null)
                {

                    //多线程访问的时候，可能会造成阻塞的情况
                    lock (_Current)
                    {
                        if (list == null)
                        {

                            list = Current.GetListAll() as List<User_info>;
                            HttpRuntime.Cache.Add(cacheKey, list, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero,
                                CacheItemPriority.Normal, null);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
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
                string cacheKey = "cache_all_User_info";
                HttpRuntime.Cache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region  public IList<User_info> GetListAll() // 获取所有记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public IList<User_info> GetListAll()
        {
            {
                try
                {
                    return _User_infoDal.GetListAll();
                }
                catch (Exception ex)
                {
                    DealError(ex);
                    return new List<User_info>();
                }
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion
    }
}
