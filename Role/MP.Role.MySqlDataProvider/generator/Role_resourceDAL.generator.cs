#region 文件描述

// 描述：数据类
// 作者：cat80
// 时间：2013-11-04 19

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
using MP.Role.Entity;
using MySql.Data.MySqlClient;
using System.Data; 

namespace MP.Role.MySqlDataProvider
{
    /// <summary>
    /// 数据类
    /// </summary>
    public partial class Role_resourceDAL  
    {
       
       #region public void Append(Role_resource entity) //增加一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(Role_resource entity)
        {
            string sqlCmd = "insert into mp_role_resource (ResourceName ,ParentID ,Sort ,ShowInMenu ,State ,CreateTime ,Url) values (@ResourceName ,@ParentID ,@Sort ,@ShowInMenu ,@State ,@CreateTime ,@Url);SELECT  LAST_INSERT_ID();";
            MySqlParameter[] pars = new MySqlParameter[7];
			pars[0] = new MySqlParameter("@ResourceName",entity.ResourceName);
			pars[1] = new MySqlParameter("@ParentID",entity.ParentID);
			pars[2] = new MySqlParameter("@Sort",entity.Sort);
			pars[3] = new MySqlParameter("@ShowInMenu",entity.ShowInMenu);
			pars[4] = new MySqlParameter("@State",entity.State);
			pars[5] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[6] = new MySqlParameter("@Url",entity.Url);
	
            entity.ResourceID = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, sqlCmd,pars));
            return true;
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
            string sqlCmd = "DELETE FROM  mp_role_resource  where  ResourceID =  @ResourceID";
            MySqlParameter [] pars = {
                                   new MySqlParameter("@ResourceID",resourceID) 
                                  };

            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd, pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region public void Update(Role_resource entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(Role_resource entity)
        {
            string sqlCmd = "update mp_role_resource set  ResourceName = @ResourceName, ParentID = @ParentID, Sort = @Sort, ShowInMenu = @ShowInMenu, State = @State, CreateTime = @CreateTime, Url = @Url  where   ResourceID =  @ResourceID ";
            
            MySqlParameter[] pars = new MySqlParameter[8];
			pars[0] = new MySqlParameter("@ResourceID",entity.ResourceID);
			pars[1] = new MySqlParameter("@ResourceName",entity.ResourceName);
			pars[2] = new MySqlParameter("@ParentID",entity.ParentID);
			pars[3] = new MySqlParameter("@Sort",entity.Sort);
			pars[4] = new MySqlParameter("@ShowInMenu",entity.ShowInMenu);
			pars[5] = new MySqlParameter("@State",entity.State);
			pars[6] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[7] = new MySqlParameter("@Url",entity.Url);
	
            return MySqlHelper.ExecuteNonQuery(CommandType.Text,sqlCmd,  pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion
         
        #region　public  Role_resource GetRole_resourceByPK(System.Int32 resourceID) 根据主键查找数据
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据主键查找数据
        /// </summary>
        /// <returns></returns>        
        public  Role_resource GetByID(System.Int32 resourceID)
        {
             
            string sqlCmd = "select * from mp_role_resource where  ResourceID =  @ResourceID";
            MySqlParameter[] pars = {
                                   new MySqlParameter("@ResourceID",resourceID) 
                                  };
             Role_resource entity = null;
            using (IDataReader reader = MySqlHelper.ExecuteReader(CommandType.Text, sqlCmd, pars))
            {
                if (reader.Read())
                {
                    entity = DataReaderToEntity(reader);
                }
            }
            return entity;
        }
        //------------------------------------------------------------------------------------------
        #endregion
         
        #region public void DeleteByIdList(List<int> idList) //根据主键批量删除仅适用主键只有一个，并且类型为整形的表
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据主键批量删除仅适用主键只有一个，并且类型为整形的表
        /// </summary>
        /// <param name="entity"></param>
        public bool DeleteByIdList(List<int> idList)
        {
            string [] strlist= idList.Select(item=>item.ToString()).ToArray();
            string sqlCmd =string.Format( "DELETE FROM  mp_role_resource  where ResourceID in ({0})",string.Join(",",strlist)); //为了兼容string.Join，先转换成string[]
            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd);
        }
        //------------------------------------------------------------------------------------------
        #endregion
    
      #region　 public IList<Role_resource> GetListAll() 查找所有的数据表字段
        //------------------------------------------------------------------------------------------        
        /// <summary>
        /// 查找所有的数据表字段
        /// </summary>
        /// <returns></returns>
        public IList<Role_resource> GetListAll()
        {
          
            string sqlCmd = "select * from mp_role_resource";
            List<Role_resource> list = new  List<Role_resource>();
            using (IDataReader reader = MySqlHelper.ExecuteReader(CommandType.Text, sqlCmd))
            {
                while (reader.Read())
                {
                   list.Add( DataReaderToEntity(reader));
                }
            }
            return list;
        }
         //------------------------------------------------------------------------------------------
        #endregion
        
      #region　 Role_resource DataReaderToEntity(IDataReader reader) 把reader转换成Entity
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 把reader转换成Entity
        /// </summary>
        /// <param name="table">表数据</param>
        /// <returns></returns>
        private Role_resource DataReaderToEntity(IDataReader reader)
        {
           
            Role_resource entity= new Role_resource();
            entity.ResourceID = Convert.ToInt32(reader["ResourceID"]) ;
            entity.ResourceName = Convert.ToString(reader["ResourceName"]) ;
            entity.ParentID = Convert.ToInt32(reader["ParentID"]) ;
            entity.Sort = Convert.ToInt32(reader["Sort"]) ;
            entity.ShowInMenu = Convert.ToInt32(reader["ShowInMenu"]) ;
            entity.State = Convert.ToInt32(reader["State"]) ;
            entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]) ;
            entity.Url = Convert.ToString(reader["Url"]) ;
            return entity;
        }
         //------------------------------------------------------------------------------------------
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
            string sql = "select * from mp_role_resource "; 
            string sortWay = string.Empty;
            List<Role_resource> list = new List<Role_resource>();
            using (IDataReader reader = MySqlHelper.GetDataReaderBySql(  pageSize, pageIndex, out recordCount, sql, sortWay))
            {
                while (reader.Read())
                {
                    list.Add(DataReaderToEntity(reader));
                }
            }
            return list;
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion
        
        
        #region public List<Role_resource> GetByCondition(string whereCodition) /// 获取条件获取记录
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录
        /// </summary>
        /// <param name="whereCodition">条件</param>
        /// <returns></returns>
        public List<Role_resource> GetByCondition(string whereCodition,params MySqlParameter[] pars)
        {
            return GetByCondition(whereCodition, string.Empty,pars);
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
			  
        #region public List<Role_resource> GetByCondition(string whereCondition, string sortOrder) /// 获取条件获取记录，并排序
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录，并排序
        /// </summary>
        /// <param name="whereCondition">条件</param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Role_resource> GetByCondition(string whereCondition, string sortOrder,params MySqlParameter[] pars)
        {
            string sqlCmd = string.Format(" SELECT * FROM mp_role_resource {0} {1}", whereCondition, sortOrder);
            List<Role_resource> list = new List<Role_resource>();
            using (IDataReader reader = MySqlHelper.ExecuteReader(CommandType.Text, sqlCmd,pars))
            {
                while (reader.Read())
                {
                    list.Add(DataReaderToEntity(reader));
                }
            }
            return list; 
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
        
    }
}

