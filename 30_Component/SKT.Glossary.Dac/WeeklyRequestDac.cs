using Microsoft.Practices.EnterpriseLibrary.Data;
using SKT.Glossary.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SKT.Glossary.Dac
{
    public class WeeklyRequestDac
    {
        private const string connectionStringName = "ConnGlossary";

        private static WeeklyRequestDac _instance = null;
        public static WeeklyRequestDac Instance
        {
            get
            {
                WeeklyRequestDac obj = _instance;
                if (obj == null)
                {
                    obj = new WeeklyRequestDac();
                    _instance = obj;
                }
                return obj;
            }
        }

        public WeeklyRequestDac() { }

        /// <summary>
        /// Inserts a record into the tb_WeeklyRequest table.
        /// </summary>
        /// <returns></returns>
        public DataSet WeeklyRequestInsert(WeeklyRequestType weeklyRequestType)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_WeeklyRequest_Insert");

            //db.AddInParameter(dbCommand, "Year", DbType.Int32, weeklyRequestType.Year);
            //db.AddInParameter(dbCommand, "YearWeek", DbType.Int32, weeklyRequestType.YearWeek);
            db.AddInParameter(dbCommand, "NowDateTime", DbType.DateTime, weeklyRequestType.NowDateTime);
            db.AddInParameter(dbCommand, "SendUserName", DbType.String, weeklyRequestType.SendUserName);
            db.AddInParameter(dbCommand, "SendUserID", DbType.String, weeklyRequestType.SendUserID);
            db.AddInParameter(dbCommand, "SendUserPositionName", DbType.String, weeklyRequestType.SendUserPositionName);
            db.AddInParameter(dbCommand, "ReceiveUserName", DbType.String, weeklyRequestType.ReceiveUserName);
            db.AddInParameter(dbCommand, "ReceiveUserID", DbType.String, weeklyRequestType.ReceiveUserID);
            db.AddInParameter(dbCommand, "ReceiveUserPositionName", DbType.String, weeklyRequestType.ReceiveUserPositionName);
            db.AddInParameter(dbCommand, "SendYN", DbType.String, weeklyRequestType.SendYN);

            // Execute the query and return the new identity value
            DataSet ds = db.ExecuteDataSet(dbCommand);

            return ds;
        }

        /// <summary>
        /// Selects a single record from the tb_WeeklyRequest table.
        /// </summary>
        /// <returns>DataSet</returns>
        public WeeklyRequestType WeeklyRequestSelect(int weeklyRequestID)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_WeeklyRequest_Select");

            db.AddInParameter(dbCommand, "WeeklyRequestID", DbType.Int32, weeklyRequestID);

            WeeklyRequestType weeklyRequestType = null;
            using (DataSet ds = db.ExecuteDataSet(dbCommand))
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    weeklyRequestType = GetWeeklyRequestTypeMapData(ds.Tables[0].Rows[0]);
                }
            }
            return weeklyRequestType;
        }

        /// <summary>
        /// Selects all records from the tb_WeeklyRequest table.
        /// </summary>
        public List<WeeklyRequestType> WeeklyRequestSelectAll()
        {
            List<WeeklyRequestType> listWeeklyRequestType = new List<WeeklyRequestType>();
            Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_WeeklyRequest_SelectAll");

            using (DataSet ds = db.ExecuteDataSet(dbCommand))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        WeeklyRequestType weeklyRequestType = GetWeeklyRequestTypeMapData(dr);
                        listWeeklyRequestType.Add(weeklyRequestType);
                    }
                }
            }
            return listWeeklyRequestType;
        }

        /// <summary>
        /// Updates a record in the tb_WeeklyRequest table.
        /// </summary>
        public void WeeklyRequestUpdate(WeeklyRequestType weeklyRequestType)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_WeeklyRequest_Update");

            db.AddInParameter(dbCommand, "WeeklyRequestID", DbType.Int32, weeklyRequestType.WeeklyRequestID);
            db.AddInParameter(dbCommand, "Year", DbType.Int32, weeklyRequestType.Year);
            db.AddInParameter(dbCommand, "YearWeek", DbType.Int32, weeklyRequestType.YearWeek);
            db.AddInParameter(dbCommand, "SendUserName", DbType.String, weeklyRequestType.SendUserName);
            db.AddInParameter(dbCommand, "SendUserID", DbType.String, weeklyRequestType.SendUserID);
            db.AddInParameter(dbCommand, "SendUserPositionName", DbType.String, weeklyRequestType.SendUserPositionName);
            db.AddInParameter(dbCommand, "ReceiveUserName", DbType.String, weeklyRequestType.ReceiveUserName);
            db.AddInParameter(dbCommand, "ReceiveUserID", DbType.String, weeklyRequestType.ReceiveUserID);
            db.AddInParameter(dbCommand, "ReceiveUserPositionName", DbType.String, weeklyRequestType.ReceiveUserPositionName);
            db.AddInParameter(dbCommand, "SendYN", DbType.String, weeklyRequestType.SendYN);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Deletes a record from the tb_WeeklyRequest table by a composite primary key.
        /// </summary>
        public void WeeklyRequestDelete(int weeklyRequestID)
        {
            Database db = DatabaseFactory.CreateDatabase(connectionStringName);
            DbCommand dbCommand = db.GetStoredProcCommand("up_WeeklyRequest_Delete");

            db.AddInParameter(dbCommand, "WeeklyRequestID", DbType.Int32, weeklyRequestID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Creates a new instance of the WeeklyRequestType class and populates it with data from the specified DataRow.
        /// </summary>
        private WeeklyRequestType GetWeeklyRequestTypeMapData(DataRow dr)
        {
            WeeklyRequestType weeklyRequestType = new WeeklyRequestType();
            weeklyRequestType.WeeklyRequestID = (dr["WeeklyRequestID"] == DBNull.Value) ? 0 : dr.Field<int>("WeeklyRequestID");
            weeklyRequestType.Year = (dr["Year"] == DBNull.Value) ? 0 : dr.Field<int>("Year");
            weeklyRequestType.YearWeek = (dr["YearWeek"] == DBNull.Value) ? 0 : dr.Field<int>("YearWeek");
            weeklyRequestType.SendUserName = (dr["SendUserName"] == DBNull.Value) ? null : dr.Field<string>("SendUserName");
            weeklyRequestType.SendUserID = (dr["SendUserID"] == DBNull.Value) ? null : dr.Field<string>("SendUserID");
            weeklyRequestType.SendUserPositionName = (dr["SendUserPositionName"] == DBNull.Value) ? null : dr.Field<string>("SendUserPositionName");
            weeklyRequestType.ReceiveUserName = (dr["ReceiveUserName"] == DBNull.Value) ? null : dr.Field<string>("ReceiveUserName");
            weeklyRequestType.ReceiveUserID = (dr["ReceiveUserID"] == DBNull.Value) ? null : dr.Field<string>("ReceiveUserID");
            weeklyRequestType.ReceiveUserPositionName = (dr["ReceiveUserPositionName"] == DBNull.Value) ? null : dr.Field<string>("ReceiveUserPositionName");
            weeklyRequestType.SendYN = (dr["SendYN"] == DBNull.Value) ? String.Empty : dr.Field<string>("SendYN");

            return weeklyRequestType;
        }
    }
}
