using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Benchmarking.Contracts;
using Benchmarking.Model.Model.Dapper;
using Dapper;
using Benchmarking.Model;
using Benchmarking.DAL;
using Benchmarking.Logger;
//using ModelAccessLayer.Awards.Model;

namespace CompsBuilderDLL.Implementation
{
    public class DataRepository : IDataRepository
    {         
        private DbConnection _dbconnection;
        public DataRepository()
        {
            _dbconnection = DbConnection.Instance;
        }
        public DataSet ExecuteQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection=null, IList<DataTableParameter> DTPCollection=null)
        {
            DataSet resultSet = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection(ConnectionStringsCollection.DatabaseConnection))
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            cmd.CommandTimeout = ConfigurationSettings.SqlCommandTimeout;
                            if (QPCollection != null && QPCollection.Count() > 0)
                            {
                                foreach (QueryParameterForSqlMapper param in QPCollection)
                                {
                                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                                    cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                }
                            }
                            if (DTPCollection != null && DTPCollection.Count() > 0)
                            {
                                foreach (DataTableParameter dataTableParameter in DTPCollection)
                                {
                                    cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
                                }
                            }
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(resultSet);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                BMLogger.Error(ex.Message);
            }
            return resultSet;
        }
        
        public int ExecuteNonQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection=null, IList<DataTableParameter> DTPCollection=null)
        {
            int result = 0;
            string outputParamName = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection(ConnectionStringsCollection.DatabaseConnection))
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            cmd.CommandTimeout = ConfigurationSettings.SqlCommandTimeout;
                            if (QPCollection != null && QPCollection.Count() > 0)
                            {
                                foreach (QueryParameterForSqlMapper param in QPCollection)
                                {
                                    if (param.ParameterDirection == ParameterDirection.Output)
                                    {
                                        cmd.Parameters.Add(param.Name, SqlDbType.VarChar, 4);
                                        cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                        outputParamName = param.Name;
                                    }
                                    else
                                    {
                                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                                        cmd.Parameters[param.Name].Direction = param.ParameterDirection;
                                    }
                                }
                            }
                            if (DTPCollection != null && DTPCollection.Count() > 0)
                            {
                                foreach (DataTableParameter dataTableParameter in DTPCollection)
                                {
                                    cmd.Parameters.AddWithValue(dataTableParameter.ParameterName, dataTableParameter.DataTable);
                                }
                            }
                            result = cmd.ExecuteNonQuery();
                            if (!string.IsNullOrEmpty(outputParamName))
                            {
                                result = Convert.ToInt32(cmd.Parameters[outputParamName].Value);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                BMLogger.Error(ex.Message);
            }
            return result;
        }

        public IEnumerable<T> ExecuteQuery<T>(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            IEnumerable<T> resultSet = null;
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {                    
                    using (SqlConnection con = _dbconnection.GetSqlConnection(ConnectionStringsCollection.DatabaseConnection))
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);
                            resultSet = con.Query<T>(spName, dynamicParameter, null, true, ConfigurationSettings.SqlCommandTimeout, CommandType.StoredProcedure).AsEnumerable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BMLogger.Error(ex.Message);
            }

            return resultSet;
        }

        public dynamic FetchRecordSet(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            dynamic resultSet = null;

            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection(ConnectionStringsCollection.DatabaseConnection))
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);
                            resultSet = con.Query(spName, dynamicParameter, null, true, ConfigurationSettings.SqlCommandTimeout, CommandType.StoredProcedure);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BMLogger.Error(ex.Message);
            }
            return resultSet;
        }

        public IList<dynamic> FetchMultipleRecordSet(string spName, IList<QueryParameterForSqlMapper> QPCollection)
        {
            IList<dynamic> dataCollection = null;
            try
            {
                if (!string.IsNullOrEmpty(spName))
                {
                    using (SqlConnection con = _dbconnection.GetSqlConnection(ConnectionStringsCollection.DatabaseConnection))
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            DynamicParameters dynamicParameter = ConvertToDynamicParameters(QPCollection);

                            SqlMapper.GridReader resultSet = con.QueryMultiple(spName, dynamicParameter, null, ConfigurationSettings.SqlCommandTimeout, commandType: CommandType.StoredProcedure);
                            dataCollection = new List<dynamic>();
                            while (!resultSet.IsConsumed)
                            {
                                dataCollection.Add(resultSet.Read());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BMLogger.Error(ex.Message);
            }
            return dataCollection;
        }

        private DynamicParameters ConvertToDynamicParameters(IList<QueryParameterForSqlMapper> QPCollection)
        {
            DynamicParameters dynamicParameter = null;
            try
            {
                if (QPCollection != null && QPCollection.Count > 0)
                {
                    dynamicParameter = new DynamicParameters();
                    foreach (QueryParameterForSqlMapper parameter in QPCollection)
                    {
                        dynamicParameter.Add(parameter.Name, parameter.Value, parameter.DbType, parameter.ParameterDirection);
                    }
                }
            }
            catch (Exception ex)
            {
                BMLogger.Error(ex.Message);
            }
            return dynamicParameter;
        }
    }
}
