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
    public partial class Role_groupDAL  
    {
       
       #region public void Append(Role_group entity) //增加一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(Role_group entity)
        {
            string sqlCmd = "insert into mp_role_group (GroupName ,CreateTime ,Auth_Action ,Auth_Resource ,State) values (@GroupName ,@CreateTime ,@Auth_Action ,@Auth_Resource ,@State);SELECT  LAST_INSERT_ID();";
            MySqlParameter[] pars = new MySqlParameter[5];
			pars[0] = new MySqlParameter("@GroupName",entity.GroupName);
			pars[1] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[2] = new MySqlParameter("@Auth_Action",entity.Auth_Action);
			pars[3] = new MySqlParameter("@Auth_Resource",entity.Auth_Resource);
			pars[4] = new MySqlParameter("@State",entity.State);
	
            entity.GroupID = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, sqlCmd,pars));
            return true;
                    }
        //------------------------------------------------------------------------------------------
        #endregion
         #region public void Delete(System.Int32 groupID) //删除一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(System.Int32 groupID)
        {
            string sqlCmd = "DELETE FROM  mp_role_group  where  GroupID =  @GroupID";
            MySqlParameter [] pars = {
                                   new MySqlParameter("@GroupID",groupID) 
                                  };

            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd, pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region public void Update(Role_group entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(Role_group entity)
        {
            string sqlCmd = "update mp_role_group set  GroupName = @GroupName, CreateTime = @CreateTime, Auth_Action = @Auth_Action, Auth_Resource = @Auth_Resource, State = @State  where   GroupID =  @GroupID ";
            
            MySqlParameter[] pars = new MySqlParameter[6];
			pars[0] = new MySqlParameter("@GroupID",entity.GroupID);
			pars[1] = new MySqlParameter("@GroupName",entity.GroupName);
			pars[2] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[3] = new MySqlParameter("@Auth_Action",entity.Auth_Action);
			pars[4] = new MySqlParameter("@Auth_Resource",entity.Auth_Resource);
			pars[5] = new MySqlParameter("@State",entity.State);
	
            return MySqlHelper.ExecuteNonQuery(CommandType.Text,sqlCmd,  pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion
         
        #region　public  Role_group GetRole_groupByPK(System.Int32 groupID) 根据主键查找数据
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据主键查找数据
        /// </summary>
        /// <returns></returns>        
        public  Role_group GetByID(System.Int32 groupID)
        {
             
            string sqlCmd = "select * from mp_role_group where  GroupID =  @GroupID";
            MySqlParameter[] pars = {
                                   new MySqlParameter("@GroupID",groupID) 
                                  };
             Role_group entity = null;
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
            string sqlCmd =string.Format( "DELETE FROM  mp_role_group  where GroupID in ({0})",string.Join(",",strlist)); //为了兼容string.Join，先转换成string[]
            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd);
        }
        //------------------------------------------------------------------------------------------
        #endregion
    
      #region　 public IList<Role_group> GetListAll() 查找所有的数据表字段
        //------------------------------------------------------------------------------------------        
        /// <summary>
        /// 查找所有的数据表字段
        /// </summary>
        /// <returns></returns>
        public IList<Role_group> GetListAll()
        {
          
            string sqlCmd = "select * from mp_role_group";
            List<Role_group> list = new  List<Role_group>();
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
        
      #region　 Role_group DataReaderToEntity(IDataReader reader) 把reader转换成Entity
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 把reader转换成Entity
        /// </summary>
        /// <param name="table">表数据</param>
        /// <returns></returns>
        private Role_group DataReaderToEntity(IDataReader reader)
        {
           
            Role_group entity= new Role_group();
            entity.GroupID = Convert.ToInt32(reader["GroupID"]) ;
            entity.GroupName = Convert.ToString(reader["GroupName"]) ;
            entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]) ;
            entity.Auth_Action = Convert.ToString(reader["Auth_Action"]) ;
            entity.Auth_Resource = Convert.ToString(reader["Auth_Resource"]) ;
            entity.State = Convert.ToInt32(reader["State"]) ;
            return entity;
        }
         //------------------------------------------------------------------------------------------
        #endregion
        
        #region public List<Role_group> GetListPager(int pageSize, int pageIndex, out int recordCount); /// 获取分页列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public List<Role_group> GetListPager(int pageSize, int pageIndex, out int recordCount)
        {
            string sql = "select * from mp_role_group "; 
            string sortWay = string.Empty;
            List<Role_group> list = new List<Role_group>();
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
        
        
        #region public List<Role_group> GetByCondition(string whereCodition) /// 获取条件获取记录
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录
        /// </summary>
        /// <param name="whereCodition">条件</param>
        /// <returns></returns>
        public List<Role_group> GetByCondition(string whereCodition,params MySqlParameter[] pars)
        {
            return GetByCondition(whereCodition, string.Empty,pars);
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
			  
        #region public List<Role_group> GetByCondition(string whereCondition, string sortOrder) /// 获取条件获取记录，并排序
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录，并排序
        /// </summary>
        /// <param name="whereCondition">条件</param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Role_group> GetByCondition(string whereCondition, string sortOrder,params MySqlParameter[] pars)
        {
            string sqlCmd = string.Format(" SELECT * FROM mp_role_group {0} {1}", whereCondition, sortOrder);
            List<Role_group> list = new List<Role_group>();
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

