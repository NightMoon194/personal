﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.OleDb;

namespace BookManage.Utils
{
    public class OleDbUtil
    {
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pVal"></param>
        /// <returns></returns>
        public static OleDbParameter CreateParameter(string pName, object pVal)
        {
            return new OleDbParameter(pName, pVal);
        }
        /// <summary>
        /// 数据库链接
        /// </summary>
        private readonly string conStr;
        public OleDbUtil()
        {
            conStr = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr"></param>
        public OleDbUtil(string conStr)
        {
            this.conStr = conStr;
        }
        /// <summary>
        /// 创建Connection对象
        /// </summary>
        /// <returns></returns>
        public OleDbConnection CreateCon()
        {
            return new OleDbConnection(conStr);
        }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, IDataParameter[] parameters = null)
        {
            using (var con = CreateCon())
            {
                try
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        foreach (var p in parameters)
                        {
                            if (p.Value == null)
                            {
                                p.Value = DBNull.Value;
                            }
                            cmd.Parameters.Add(p);
                        }
                    }
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 查询sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataTable QuerySqlDataTable(string sql, IDataParameter[] parms = null)
        {
            using (var con = CreateCon())
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = sql;
                if (parms != null)
                {
                    foreach (var p in parms)
                    {
                        if (p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(p);
                    }
                }
                DataTable table = new DataTable();
                OleDbDataAdapter dapt = new OleDbDataAdapter(cmd);
                dapt.Fill(table);
                return table;
            }
        }
        /// <summary>
        /// 查询数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql, IDataParameter[] parms = null) where T : class, new()
        {
            DataTable table = QuerySqlDataTable(sql, parms);
            if (table == null || table.Rows.Count <= 0)
            {
                return new List<T>();
            }
            var dataList = new List<T>();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach (DataRow row in table.Rows)
            {
                T t = new T();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    //过滤Ignore属性
                    int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
                    if (count > 0)
                    {
                        continue;
                    }
                    //数据库为空时
                    if (row[propertyInfo.Name] == DBNull.Value)
                    {
                        propertyInfo.SetValue(t, null, null);
                    }
                    else
                    {
                        //否则直接赋值
                        propertyInfo.SetValue(t, row[propertyInfo.Name], null);
                    }
                }

                dataList.Add(t);
            }

            return dataList;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Insert<T>(T t) where T : class, new()
        {
            using (var con = CreateCon())
            {
                con.Open();
                var trans = con.BeginTransaction();
                try
                {
                    int ret = con.Insert(t, trans);
                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Edit<T>(T t) where T : class, new()
        {
            using (var con = CreateCon())
            {
                con.Open();
                var trans = con.BeginTransaction();
                try
                {
                    int ret = con.Edit(t, trans);
                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Delete<T>(T t) where T : class, new()
        {
            using (var con = CreateCon())
            {
                con.Open();
                var trans = con.BeginTransaction();
                try
                {
                    int ret = con.Delete(t, trans);
                    trans.Commit();
                    return ret;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 查询DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet QuerySqlDataSet(string sql)
        {
            using (var con = CreateCon())
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter dapt = new OleDbDataAdapter(sql, con);
                dapt.Fill(ds);
                return ds;
            }
        }
    }

    public static class SqlUtilExt
    {
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="con"></param>
        /// <param name="sql"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int ExecuteSql(this OleDbConnection con, string sql, OleDbTransaction trans)
        {
            return ExecuteSql(con, sql, null, trans);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="con"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="trans"></param>
        /// <returns></returns>

        public static int ExecuteSql(this OleDbConnection con, string sql, IDataParameter[] parameters, OleDbTransaction trans)
        {
            var cmd = con.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    if (p.Value == null)
                    {
                        p.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(p);
                }
            }
            cmd.Transaction = trans;
            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con"></param>
        /// <param name="t"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int Insert<T>(this OleDbConnection con, T t, OleDbTransaction trans) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            string tblName = typeof(T).Name;
            int keyCount = 0;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                keyCount++;
            }

            if (keyCount <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }

            DataSet ds = new DataSet();
            OleDbDataAdapter dapt = new OleDbDataAdapter("select * from [" + tblName + "] where 1<>1", con);
            dapt.SelectCommand.Transaction = trans;
            dapt.Fill(ds);

            DataRow row = ds.Tables[0].NewRow();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //过滤Ignore属性
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute).Count();
                if (count > 0)
                {
                    continue;
                }
                //主键如果是Identity则不允许主动插入数据
                object pkAttr = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).FirstOrDefault();
                if (pkAttr != null)
                {
                    if ((pkAttr as KeyAttribute).Identity)
                    {
                        continue;
                    }
                }

                object obj = propertyInfo.GetValue(t, null);
                if (obj == null)
                {
                    row[propertyInfo.Name] = DBNull.Value;
                }
                else
                {
                    row[propertyInfo.Name] = obj;
                }
            }

            ds.Tables[0].Rows.Add(row);

            OleDbCommandBuilder scb = new OleDbCommandBuilder(dapt);
            scb.QuotePrefix = "[";
            scb.QuoteSuffix = "]";
            return dapt.Update(ds);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con"></param>
        /// <param name="t"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int Edit<T>(this OleDbConnection con, T t, OleDbTransaction trans) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            List<IDataParameter> parameters = new List<IDataParameter>();
            StringBuilder s_where = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                object obj = propertyInfo.GetValue(t, null);
                IDataParameter parameter = null;
                if (obj == null)
                {
                    parameter = OleDbUtil.CreateParameter("@" + propertyInfo.Name, DBNull.Value);
                }
                else
                {
                    parameter = OleDbUtil.CreateParameter("@" + propertyInfo.Name, obj);
                }

                if (obj.GetType() == typeof(string))
                {
                    parameter.DbType = DbType.String;
                    var tmp_sla = propertyInfo.GetCustomAttributes(false).Where(a => a is StringLengthAttribute).FirstOrDefault();
                    if (tmp_sla != null)
                    {
                        int tmp_length = (tmp_sla as StringLengthAttribute).Length;
                        (parameter as OleDbParameter).Size = tmp_length;
                    }
                }
                else if (obj.GetType() == typeof(double))
                    parameter.DbType = DbType.Double;
                else if (obj.GetType() == typeof(float))
                    parameter.DbType = DbType.Double;
                else if (obj.GetType() == typeof(decimal))
                    parameter.DbType = DbType.Decimal;
                else if (obj.GetType() == typeof(DateTime))
                    parameter.DbType = DbType.DateTime;
                else if (obj.GetType() == typeof(int))
                    parameter.DbType = DbType.Int32;
                else if (obj.GetType() == typeof(long))
                    parameter.DbType = DbType.Int64;


                parameters.Add(parameter);
                s_where.AppendAnd("[" + propertyInfo.Name + "]" + "=@" + propertyInfo.Name);
            }

            if (parameters.Count <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }

            string tblName = typeof(T).Name;
            OleDbCommand cmd = new OleDbCommand("select * from [" + tblName + "] where " + s_where.ToString(), con);
            cmd.Parameters.AddRange(parameters.ToArray());
            DataSet ds = new DataSet();
            OleDbDataAdapter dapt = new OleDbDataAdapter(cmd);
            dapt.SelectCommand.Transaction = trans;
            dapt.Fill(ds);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                throw new Exception("未找到符合条件的记录");
            }
            DataRow row = ds.Tables[0].Rows[0];
            row.BeginEdit();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //过滤Ignore和Key属性
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is IgnoreAttribute || a is KeyAttribute).Count();
                if (count > 0)
                {
                    continue;
                }

                object obj = propertyInfo.GetValue(t, null);
                if (obj == null)
                {
                    row[propertyInfo.Name] = DBNull.Value;
                }
                else
                {
                    row[propertyInfo.Name] = obj;
                }
            }
            row.EndEdit();
            OleDbCommandBuilder scb = new OleDbCommandBuilder(dapt);
            scb.QuotePrefix = "[";
            scb.QuoteSuffix = "]";
            //dapt.UpdateCommand.Transaction = trans;
            return dapt.Update(ds);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con"></param>
        /// <param name="t"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int Delete<T>(this OleDbConnection con, T t, OleDbTransaction trans) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            List<IDataParameter> parameters = new List<IDataParameter>();
            StringBuilder s_where = new StringBuilder();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                int count = propertyInfo.GetCustomAttributes(false).Where(a => a is KeyAttribute).Count();
                if (count <= 0)
                {
                    continue;
                }
                object obj = propertyInfo.GetValue(t, null);
                IDataParameter parameter = null;
                if (obj == null)
                {
                    parameter = OleDbUtil.CreateParameter("@" + propertyInfo.Name, DBNull.Value);
                }
                else
                {
                    parameter = OleDbUtil.CreateParameter("@" + propertyInfo.Name, obj);
                }

                if (obj.GetType() == typeof(string))
                {
                    parameter.DbType = DbType.String;
                    var tmp_sla = propertyInfo.GetCustomAttributes(false).Where(a => a is StringLengthAttribute).FirstOrDefault();
                    if (tmp_sla != null)
                    {
                        int tmp_length = (tmp_sla as StringLengthAttribute).Length;
                        (parameter as OleDbParameter).Size = tmp_length;
                    }
                }
                else if (obj.GetType() == typeof(double))
                    parameter.DbType = DbType.Double;
                else if (obj.GetType() == typeof(float))
                    parameter.DbType = DbType.Double;
                else if (obj.GetType() == typeof(decimal))
                    parameter.DbType = DbType.Decimal;
                else if (obj.GetType() == typeof(DateTime))
                    parameter.DbType = DbType.DateTime;
                else if (obj.GetType() == typeof(int))
                    parameter.DbType = DbType.Int32;
                else if (obj.GetType() == typeof(long))
                    parameter.DbType = DbType.Int64;


                parameters.Add(parameter);
                s_where.AppendAnd("["+propertyInfo.Name+"]" + "=@" + propertyInfo.Name);
            }

            if (parameters.Count <= 0)
            {
                throw new Exception("实体未设置KeyAttribute属性");
            }

            string tblName = typeof(T).Name;
            OleDbCommand cmd = new OleDbCommand("select * from [" + tblName + "] where " + s_where.ToString(), con);
            cmd.Parameters.AddRange(parameters.ToArray());
            DataSet ds = new DataSet();
            OleDbDataAdapter dapt = new OleDbDataAdapter(cmd);
            dapt.SelectCommand.Transaction = trans;
            dapt.Fill(ds);

            if (ds.Tables[0].Rows.Count <= 0)
            {
                throw new Exception("未找到符合条件的记录");
            }
            DataRow row = ds.Tables[0].Rows[0];
            row.Delete();
            OleDbCommandBuilder scb = new OleDbCommandBuilder(dapt);
            scb.QuotePrefix = "[";
            scb.QuoteSuffix = "]";
            //dapt.DeleteCommand.Transaction = trans;
            return dapt.Update(ds);
        }

    }
}
