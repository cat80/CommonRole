#region 文件描述

// 描述：数据类
// 作者：苏志萍
// 时间：2013-11-29 14

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
    public partial class User_infoDAL  
    {
       
       #region public void Append(User_info entity) //增加一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Append(User_info entity)
        {
            string sqlCmd = "insert into mp_user_info (DeptID ,UserLoginName ,UserPassword ,UserName ,IsAdmin ,Description ,CreateTime ,LastModifyTime ,ModifiedUserID ,MobliePhone ,WorkPhone ,Email ,Status ,Address ,Deleted ,Groups ,Level ,MaxCreateNumber ,ExpireTime ,Province ,City ,GroupNames ,MakeRate ,NotMakeRate ,AgentRate ,CreateUser) values (@DeptID ,@UserLoginName ,@UserPassword ,@UserName ,@IsAdmin ,@Description ,@CreateTime ,@LastModifyTime ,@ModifiedUserID ,@MobliePhone ,@WorkPhone ,@Email ,@Status ,@Address ,@Deleted ,@Groups ,@Level ,@MaxCreateNumber ,@ExpireTime ,@Province ,@City ,@GroupNames ,@MakeRate ,@NotMakeRate ,@AgentRate ,@CreateUser);SELECT  LAST_INSERT_ID();";
            MySqlParameter[] pars = new MySqlParameter[26];
			pars[0] = new MySqlParameter("@DeptID",entity.DeptID);
			pars[1] = new MySqlParameter("@UserLoginName",entity.UserLoginName);
			pars[2] = new MySqlParameter("@UserPassword",entity.UserPassword);
			pars[3] = new MySqlParameter("@UserName",entity.UserName);
			pars[4] = new MySqlParameter("@IsAdmin",entity.IsAdmin);
			pars[5] = new MySqlParameter("@Description",entity.Description);
			pars[6] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[7] = new MySqlParameter("@LastModifyTime",entity.LastModifyTime);
			pars[8] = new MySqlParameter("@ModifiedUserID",entity.ModifiedUserID);
			pars[9] = new MySqlParameter("@MobliePhone",entity.MobliePhone);
			pars[10] = new MySqlParameter("@WorkPhone",entity.WorkPhone);
			pars[11] = new MySqlParameter("@Email",entity.Email);
			pars[12] = new MySqlParameter("@Status",entity.Status);
			pars[13] = new MySqlParameter("@Address",entity.Address);
			pars[14] = new MySqlParameter("@Deleted",entity.Deleted);
			pars[15] = new MySqlParameter("@Groups",entity.Groups);
			pars[16] = new MySqlParameter("@Level",entity.Level);
			pars[17] = new MySqlParameter("@MaxCreateNumber",entity.MaxCreateNumber);
			pars[18] = new MySqlParameter("@ExpireTime",entity.ExpireTime);
			pars[19] = new MySqlParameter("@Province",entity.Province);
			pars[20] = new MySqlParameter("@City",entity.City);
			pars[21] = new MySqlParameter("@GroupNames",entity.GroupNames);
			pars[22] = new MySqlParameter("@MakeRate",entity.MakeRate);
			pars[23] = new MySqlParameter("@NotMakeRate",entity.NotMakeRate);
			pars[24] = new MySqlParameter("@AgentRate",entity.AgentRate);
			pars[25] = new MySqlParameter("@CreateUser",entity.CreateUser);
	
            entity.UserID = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, sqlCmd,pars));
            return true;
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
            string sqlCmd = "DELETE FROM  mp_user_info  where  UserID =  @UserID";
            MySqlParameter [] pars = {
                                   new MySqlParameter("@UserID",userID) 
                                  };

            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd, pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region public void Update(User_info entity) //更新一条记录
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(User_info entity)
        {
            string sqlCmd = "update mp_user_info set  DeptID = @DeptID, UserLoginName = @UserLoginName, UserPassword = @UserPassword, UserName = @UserName, IsAdmin = @IsAdmin, Description = @Description, CreateTime = @CreateTime, LastModifyTime = @LastModifyTime, ModifiedUserID = @ModifiedUserID, MobliePhone = @MobliePhone, WorkPhone = @WorkPhone, Email = @Email, Status = @Status, Address = @Address, Deleted = @Deleted, Groups = @Groups, Level = @Level, MaxCreateNumber = @MaxCreateNumber, ExpireTime = @ExpireTime, Province = @Province, City = @City, GroupNames = @GroupNames, MakeRate = @MakeRate, NotMakeRate = @NotMakeRate, AgentRate = @AgentRate, CreateUser = @CreateUser  where   UserID =  @UserID ";
            
            MySqlParameter[] pars = new MySqlParameter[27];
			pars[0] = new MySqlParameter("@UserID",entity.UserID);
			pars[1] = new MySqlParameter("@DeptID",entity.DeptID);
			pars[2] = new MySqlParameter("@UserLoginName",entity.UserLoginName);
			pars[3] = new MySqlParameter("@UserPassword",entity.UserPassword);
			pars[4] = new MySqlParameter("@UserName",entity.UserName);
			pars[5] = new MySqlParameter("@IsAdmin",entity.IsAdmin);
			pars[6] = new MySqlParameter("@Description",entity.Description);
			pars[7] = new MySqlParameter("@CreateTime",entity.CreateTime);
			pars[8] = new MySqlParameter("@LastModifyTime",System.DateTime.Now);
			pars[9] = new MySqlParameter("@ModifiedUserID",entity.ModifiedUserID);
			pars[10] = new MySqlParameter("@MobliePhone",entity.MobliePhone);
			pars[11] = new MySqlParameter("@WorkPhone",entity.WorkPhone);
			pars[12] = new MySqlParameter("@Email",entity.Email);
			pars[13] = new MySqlParameter("@Status",entity.Status);
			pars[14] = new MySqlParameter("@Address",entity.Address);
			pars[15] = new MySqlParameter("@Deleted",entity.Deleted);
			pars[16] = new MySqlParameter("@Groups",entity.Groups);
			pars[17] = new MySqlParameter("@Level",entity.Level);
			pars[18] = new MySqlParameter("@MaxCreateNumber",entity.MaxCreateNumber);
			pars[19] = new MySqlParameter("@ExpireTime",entity.ExpireTime);
			pars[20] = new MySqlParameter("@Province",entity.Province);
			pars[21] = new MySqlParameter("@City",entity.City);
			pars[22] = new MySqlParameter("@GroupNames",entity.GroupNames);
			pars[23] = new MySqlParameter("@MakeRate",entity.MakeRate);
			pars[24] = new MySqlParameter("@NotMakeRate",entity.NotMakeRate);
			pars[25] = new MySqlParameter("@AgentRate",entity.AgentRate);
			pars[26] = new MySqlParameter("@CreateUser",entity.CreateUser);
	
            return MySqlHelper.ExecuteNonQuery(CommandType.Text,sqlCmd,  pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion
         
        #region　public  User_info GetUser_infoByPK(System.Int32 userID) 根据主键查找数据
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据主键查找数据
        /// </summary>
        /// <returns></returns>        
        public  User_info GetByID(System.Int32 userID)
        {
             
            string sqlCmd = "select * from mp_user_info where  UserID =  @UserID";
            MySqlParameter[] pars = {
                                   new MySqlParameter("@UserID",userID) 
                                  };
             User_info entity = null;
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
            string sqlCmd =string.Format( "DELETE FROM  mp_user_info  where UserID in ({0})",string.Join(",",strlist)); //为了兼容string.Join，先转换成string[]
            return MySqlHelper.ExecuteNonQuery( CommandType.Text,sqlCmd);
        }
        //------------------------------------------------------------------------------------------
        #endregion
    
      #region　 public IList<User_info> GetListAll() 查找所有的数据表字段
        //------------------------------------------------------------------------------------------        
        /// <summary>
        /// 查找所有的数据表字段
        /// </summary>
        /// <returns></returns>
        public IList<User_info> GetListAll()
        {
          
            string sqlCmd = "select * from mp_user_info";
            List<User_info> list = new  List<User_info>();
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
        
      #region　 User_info DataReaderToEntity(IDataReader reader) 把reader转换成Entity
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 把reader转换成Entity
        /// </summary>
        /// <param name="table">表数据</param>
        /// <returns></returns>
        private User_info DataReaderToEntity(IDataReader reader)
        {
           
            User_info entity= new User_info();
            entity.UserID = Convert.ToInt32(reader["UserID"]) ;
            entity.DeptID = Convert.ToInt32(reader["DeptID"]) ;
            entity.UserLoginName = Convert.ToString(reader["UserLoginName"]) ;
            entity.UserPassword = Convert.ToString(reader["UserPassword"]) ;
            entity.UserName = Convert.ToString(reader["UserName"]) ;
            entity.IsAdmin = Convert.ToString(reader["IsAdmin"]) ;
            entity.Description = Convert.ToString(reader["Description"]) ;
            entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]) ;
            entity.LastModifyTime = Convert.ToDateTime(reader["LastModifyTime"]) ;
            entity.ModifiedUserID = Convert.ToString(reader["ModifiedUserID"]) ;
            entity.MobliePhone = Convert.ToString(reader["MobliePhone"]) ;
            entity.WorkPhone = Convert.ToString(reader["WorkPhone"]) ;
            entity.Email = Convert.ToString(reader["Email"]) ;
            entity.Status = Convert.ToInt32(reader["Status"]) ;
            entity.Address = Convert.ToString(reader["Address"]) ;
            entity.Deleted = Convert.ToInt32(reader["Deleted"]) ;
            entity.Groups = Convert.ToString(reader["Groups"]) ;
            entity.Level = Convert.ToInt32(reader["Level"]) ;
            entity.MaxCreateNumber = Convert.ToInt32(reader["MaxCreateNumber"]) ;
            entity.ExpireTime = Convert.ToDateTime(reader["ExpireTime"]) ;
            entity.Province = Convert.ToString(reader["Province"]) ;
            entity.City = Convert.ToString(reader["City"]) ;
            entity.GroupNames = Convert.ToString(reader["GroupNames"]) ;
            entity.MakeRate = Convert.ToDecimal(reader["MakeRate"]) ;
            entity.NotMakeRate = Convert.ToDecimal(reader["NotMakeRate"]) ;
            entity.AgentRate = Convert.ToDecimal(reader["AgentRate"]) ;
            entity.CreateUser = Convert.ToInt32(reader["CreateUser"]) ;
            return entity;
        }
         //------------------------------------------------------------------------------------------
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
            string sql = "select * from mp_user_info "; 
            string sortWay = string.Empty;
            List<User_info> list = new List<User_info>();
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
        
        
        #region public List<User_info> GetByCondition(string whereCodition) /// 获取条件获取记录
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录
        /// </summary>
        /// <param name="whereCodition">条件</param>
        /// <returns></returns>
        public List<User_info> GetByCondition(string whereCodition,params MySqlParameter[] pars)
        {
            return GetByCondition(whereCodition, string.Empty,pars);
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
			  
        #region public List<User_info> GetByCondition(string whereCondition, string sortOrder) /// 获取条件获取记录，并排序
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取条件获取记录，并排序
        /// </summary>
        /// <param name="whereCondition">条件</param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<User_info> GetByCondition(string whereCondition, string sortOrder,params MySqlParameter[] pars)
        {
            string sqlCmd = string.Format(" SELECT * FROM mp_user_info {0} {1}", whereCondition, sortOrder);
            List<User_info> list = new List<User_info>();
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

