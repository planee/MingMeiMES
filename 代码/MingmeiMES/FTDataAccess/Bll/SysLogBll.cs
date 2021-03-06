﻿using System;
using System.Data;
using System.Collections.Generic;
using FTDataAccess.Model;
namespace FTDataAccess.BLL
{
    /// <summary>
    /// SysLog
    /// </summary>
    public partial class SysLogBll
    {
        private readonly FTDataAccess.DAL.SysLogDal dal = new FTDataAccess.DAL.SysLogDal();
        public SysLogBll()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(FTDataAccess.Model.SysLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long LogID)
        {

            return dal.Delete(LogID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string LogIDlist)
        {
            return dal.DeleteList(LogIDlist);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FTDataAccess.Model.SysLog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FTDataAccess.Model.SysLog> DataTableToList(DataTable dt)
        {
            List<FTDataAccess.Model.SysLog> modelList = new List<FTDataAccess.Model.SysLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FTDataAccess.Model.SysLog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
      

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetModelsByPage(int PageSize, int PageIndex, string strWhere,bool Asc)
        {
            DataSet ds = null;
            if (Asc)
            {
                ds = dal.GetList(PageSize, PageIndex, strWhere, 0);
            }
            else
            {
                ds = dal.GetList(PageSize, PageIndex, strWhere, 1);
            }
           
            return ds;
        }
        ///// <summary>
        ///// 分页获得数据列表
        ///// </summary>
        //public List<FTDataAccess.Model.SysLog> GetModelsByPage(int PageSize, int PageIndex, string strWhere)
        //{
        //    DataSet ds = dal.GetList(PageSize,PageIndex,strWhere);
        //    return DataTableToList(ds.Tables[0]);
        //}
        public void ClearHistorydata()
        {
            if(dal.GetRecordCount("")>10000)
            {
                System.TimeSpan ts = new TimeSpan(15,0,0,0); //15天
                System.DateTime delDate = System.DateTime.Now-ts;
                string strWhere = string.Format("delete from SysLog where LogTime<'{0}'", delDate.ToString("yyyy-MM-dd"));
                
                FTDataAccess.DBUtility.DbHelperSQL.ExecuteSql(strWhere);
            }
        }
        #endregion  ExtensionMethod
    }
}


