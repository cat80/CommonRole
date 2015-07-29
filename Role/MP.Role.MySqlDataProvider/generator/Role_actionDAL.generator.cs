#region 文件描述

// 描述：数据类
// 作者：苏志平
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
    public partial class Role_actionDAL  
    {
       
       #region public void Append(Role_action entity) //增加一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(Role_action entity)
        {
            string sqlCmd = "insert into mp_role_action (ResourceID ,ControllerName ,ActionName ,DisplayName ,Sort ,Status ,CreateTime ,IsShowInMenu ,LastModifyTime ,Remarks) values (@ResourceID ,@ControllerName ,@ActionName ,@DisplayName ,@Sort ,@Status ,@CreateTime ,@IsShowInMenu ,@LastModifyTime ,@Remarks);SELECT  LAST_INSERT_ID();";
            MySqlParameter[] pars = new MySqlParameter[10];
			pars[0] = new MySqlParameter("@ResourceID",entity.ResourceID);
			pars[1] = new MySqlParameter("@ControllerName",entity.ControllerName);
			pars[2] = new MySqlParameter("@ActionName",entity.ActionName);
			pars[3] = new MySqlParameter("@DisplayName",entity.DisplayName);
			pars[4] = new MySqlParameter("@Sort",entity.Sort);
			pars[5] = new MySqlParameter("@Status",entity.Status);
			pars[6] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[7] = new MySqlParameter("@IsShowInMenu",entity.IsShowInMenu);
			pars[8] = new MySqlParameter("@LastModifyTime",entity.LastModifyTime);
			pars[9] = new MySqlParameter("@Remarks",entity.Remarks);
	
            entity.ActionID = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, sqlCmd,pars));
            return true;
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
            string sqlCmd = "DELETE FROM  mp_role_action  where  ActionID =  @ActionID";
            MySqlParameter [] pars = {
                                   new MySqlParameter("@ActionID",actionID) 
                                  };

            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd, pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region public void Update(Role_action entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(Role_action entity)
        {
            string sqlCmd = "update mp_role_action set  ResourceID = @ResourceID, ControllerName = @ControllerName, ActionName = @ActionName, DisplayName = @DisplayName, Sort = @Sort, Status = @Status, CreateTime = @CreateTime, IsShowInMenu = @IsShowInMenu, LastModifyTime = @LastModifyTime, Remarks = @Remarks  where   ActionID =  @ActionID ";
            
            MySqlParameter[] pars = new MySqlParameter[11];
			pars[0] = new MySqlParameter("@ActionID",entity.ActionID);
			pars[1] = new MySqlParameter("@ResourceID",entity.ResourceID);
			pars[2] = new MySqlParameter("@ControllerName",entity.ControllerName);
			pars[3] = new MySqlParameter("@ActionName",entity.ActionName);
			pars[4] = new MySqlParameter("@DisplayName",entity.DisplayName);
			pars[5] = new MySqlParameter("@Sort",entity.Sort);
			pars[6] = new MySqlParameter("@Status",entity.Status);
			pars[7] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[8] = new MySqlParameter("@IsShowInMenu",entity.IsShowInMenu);
			pars[9] = new MySqlParameter("@LastModifyTime",System.DateTime.Now);
			pars[10] = new MySqlParameter("@Remarks",entity.Remarks);
	
            return MySqlHelper.ExecuteNonQuery(CommandType.Text,sqlCmd,  pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion
         
        #region　public  Role_action GetRole_actionByPK(System.Int32 actionID) 根据主键查找数据
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据主键查找数据
        /// </summary>
        /// <returns></returns>        
        public  Role_action GetByID(System.Int32 actionID)
        {
             
            string sqlCmd = "select * from mp_role_action where  ActionID =  @ActionID";
            MySqlParameter[] pars = {
                                   new MySqlParameter("@ActionID",actionID) 
                                  };
             Role_action entity = null;
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
            string sqlCmd =string.Format( "DELETE FROM  mp_role_action  where ActionID in ({0})",string.Join(",",strlist)); //为了兼容string.Join，先转换成string[]
            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd);
        }
        //------------------------------------------------------------------------------------------
        #endregion
    
      #region　 public IList<Role_action> GetListAll() 查找所有的数据表字段
        //------------------------------------------------------------------------------------------        
        /// <summary>
        /// 查找所有的数据表字段
        /// </summary>
        /// <returns></returns>
        public IList<Role_action> GetListAll()
        {
          
            string sqlCmd = "select * from mp_role_action";
            List<Role_action> list = new  List<Role_action>();
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
        
      #region　 Role_action DataReaderToEntity(IDataReader reader) 把reader转换成Entity
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 把reader转换成Entity
        /// </summary>
        /// <param name="table">表数据</param>
        /// <returns></returns>
        private Role_action DataReaderToEntity(IDataReader reader)
        {
           
            Role_action entity= new Role_action();
            entity.ActionID = Convert.ToInt32(reader["ActionID"]) ;
            entity.ResourceID = Convert.ToInt32(reader["ResourceID"]) ;
            entity.ControllerName = Convert.ToString(reader["ControllerName"]) ;
            entity.ActionName = Convert.ToString(reader["ActionName"]) ;
            entity.DisplayName = Convert.ToString(reader["DisplayName"]) ;
            entity.Sort = Convert.ToInt32(reader["Sort"]) ;
            entity.Status = Convert.ToSByte(reader["Status"]) ;
            entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]) ;
            entity.IsShowInMenu = Convert.ToSByte(reader["IsShowInMenu"]) ;
            entity.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]) ;
            entity.Remarks = Convert.ToString(reader["Remarks"]) ;
            return entity;
        }
         //------------------------------------------------------------------------------------------
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
            string sql = "select * from mp_role_action "; 
            string sortWay = string.Empty;
            List<Role_action> list = new List<Role_action>();
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
        
        
        #region public List<Role_action> GetByCondition(string whereCodition) /// 获取条件获取记录
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录
        /// </summary>
        /// <param name="whereCodition">条件</param>
        /// <returns></returns>
        public List<Role_action> GetByCondition(string whereCodition,params MySqlParameter[] pars)
        {
            return GetByCondition(whereCodition, string.Empty,pars);
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
			  
        #region public List<Role_action> GetByCondition(string whereCondition, string sortOrder) /// 获取条件获取记录，并排序
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录，并排序
        /// </summary>
        /// <param name="whereCondition">条件</param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Role_action> GetByCondition(string whereCondition, string sortOrder,params MySqlParameter[] pars)
        {
            string sqlCmd = string.Format(" SELECT * FROM mp_role_action {0} {1}", whereCondition, sortOrder);
            List<Role_action> list = new List<Role_action>();
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

