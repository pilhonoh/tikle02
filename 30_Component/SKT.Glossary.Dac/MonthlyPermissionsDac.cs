using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SKT.Glossary.Type;

namespace SKT.Glossary.Dac
{
	/// <summary>
	/// 설명: Data access class for tb_WeeklyPermissions table.
	/// 작성일 : 2015-04-12
	/// 작성자 : miksystem.com
	/// </summary>
	public sealed class MonthlyPermissionsDac
	{
        private const string connectionStringName = "ConnGlossary";

        public MonthlyPermissionsDac() { }

		/// <summary>
		/// Inserts a record into the tb_WeeklyPermissions table.
		/// </summary>
		/// <returns></returns>
		public void MonthlyPermissionsInsert(MonthlyPermissionsType weeklyPermissionsType)
		{
			Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_MonthlyPermissions_Insert");

			db.AddInParameter(dbCommand, "WeeklyID", DbType.Int64, weeklyPermissionsType.WeeklyID);
			db.AddInParameter(dbCommand, "ToUserID", DbType.String, weeklyPermissionsType.ToUserID);
			db.AddInParameter(dbCommand, "ToUserName", DbType.String, weeklyPermissionsType.ToUserName);

			db.ExecuteNonQuery(dbCommand);
		}

		/// <summary>
		/// Selects a single record from the tb_WeeklyPermissions table.
		/// </summary>
		/// <returns>DataSet</returns>
        public MonthlyPermissionsType MonthlyPermissionsSelect(Int64 weeklyID, string toUserID)
		{
			Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_MonthlyPermissions_Select");

			db.AddInParameter(dbCommand, "WeeklyID", DbType.Int64, weeklyID);
			db.AddInParameter(dbCommand, "ToUserID", DbType.String, toUserID);

			MonthlyPermissionsType weeklyPermissionsType = null;
			using (DataSet ds = db.ExecuteDataSet(dbCommand))
			{
			    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			    {
                    weeklyPermissionsType = GetMonthlyPermissionsTypeMapData(ds.Tables[0].Rows[0]);
			    }
			}
            return weeklyPermissionsType;
		}

		/// <summary>
		/// Selects all records from the tb_WeeklyPermissions table.
		/// </summary>
        public List<MonthlyPermissionsType> MonthlyPermissionsList(Int64 weeklyID)
		{
            List<MonthlyPermissionsType> listWeeklyPermissionsType = new List<MonthlyPermissionsType>();
			Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_MonthlyPermissions_List");
            db.AddInParameter(dbCommand, "WeeklyID", DbType.Int64, weeklyID);

			using (DataSet ds = db.ExecuteDataSet(dbCommand))
			{
			    if (ds != null && ds.Tables.Count > 0)
			    {
			        foreach (DataRow dr in ds.Tables[0].Rows)
			        {
                        MonthlyPermissionsType weeklyPermissionsType = GetMonthlyPermissionsTypeMapData(dr);
                        listWeeklyPermissionsType.Add(weeklyPermissionsType);
			        }
			    }
			}
			return listWeeklyPermissionsType;
		}

        /// <summary>
        /// Selects all records from the tb_WeeklyPermissions table.
        /// </summary>
        public DataSet MonthlyPermissionsView(Int64 weeklyID)
        {
            List<MonthlyPermissionsType> listWeeklyPermissionsType = new List<MonthlyPermissionsType>();
            Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_MonthlyPermissions_List");
            db.AddInParameter(dbCommand, "WeeklyID", DbType.Int64, weeklyID);

            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds;
        }

		/// <summary>
		/// Deletes a record from the tb_WeeklyPermissions table by a composite primary key.
		/// </summary>
        public void MonthlyPermissionsDeleteAll(Int64 weeklyID)
		{
			Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_MonthlyPermissions_DeleteAll");

			db.AddInParameter(dbCommand, "WeeklyID", DbType.Int64, weeklyID);

			db.ExecuteNonQuery(dbCommand);
		}

		/// <summary>
		/// Creates a new instance of the WeeklyPermissionsType class and populates it with data from the specified DataRow.
		/// </summary>
        private MonthlyPermissionsType GetMonthlyPermissionsTypeMapData(DataRow dr)
		{
            MonthlyPermissionsType weeklyPermissionsType = new MonthlyPermissionsType();
			weeklyPermissionsType.WeeklyID = (dr["WeeklyID"] == DBNull.Value) ? 0 : dr.Field<long>("WeeklyID");
			weeklyPermissionsType.ToUserID = (dr["ToUserID"] == DBNull.Value) ? null : dr.Field<string>("ToUserID");
			weeklyPermissionsType.ToUserName = (dr["ToUserName"] == DBNull.Value) ? null : dr.Field<string>("ToUserName");

            return weeklyPermissionsType;
		}
	}
}
