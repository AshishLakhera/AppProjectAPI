using AppProject.Model;
using CompsContext;
using CompsModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace AppProject.Repositories
{
    public class MyPortfolioRepositories
    {
        private readonly CompsEntities _compsDb;

        private const int sqlCommandTimeout = 300;
        public MyPortfolioRepositories(CompsEntities compsDb)
        {
          
            _compsDb = compsDb;
        }
        string ConnectionString = "server=EVS01CPB162\\SQLEXPRESS;database=CompsBuilderDBLive;uid=sa;password=12345;";
        public List<CustomPortfolioModel> GetPortFolioList(string UserId) {
           
            IList<QueryParameterForSqlMapper> parameterCollection = new List<QueryParameterForSqlMapper>
                {

                      new QueryParameterForSqlMapper
                    {
                        Name = "CreatedBy",
                        ParameterDirection = ParameterDirection.Input,
                        Value =  UserId,
                        DbType = DbType.String
                    }
                };
            var result = QueryExecution<CustomPortfolioModel>("usp_CustomPortfolioList", parameterCollection).ToList();
            return result;
        }
        private SqlConnection CreateDatabaseConnection()
        {
            SqlConnection sql = null;

            try
            {
                if (ConnectionString != null)
                {
                    sql = new SqlConnection(ConnectionString);
                    sql.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }
        public IEnumerable<T> QueryExecution<T>(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection)
        {
            IEnumerable<T> resultSet = null;

            try
            {
                if (!string.IsNullOrEmpty(storedProcedure))
                {
                    DynamicParameters dynamicParameter = ConvertToDynamicParameters(parameterCollection);
                    using (SqlConnection sql = CreateDatabaseConnection())
                    {
                        resultSet = sql.Query<T>(storedProcedure, dynamicParameter, null, true, sqlCommandTimeout, CommandType.StoredProcedure).AsEnumerable();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultSet;
        }
        private DynamicParameters ConvertToDynamicParameters(IList<QueryParameterForSqlMapper> parameterCollection)
        {
            DynamicParameters dynamicParameter = null;

            try
            {
                if (parameterCollection != null && parameterCollection.Count > 0)
                {
                    dynamicParameter = new DynamicParameters();
                    foreach (QueryParameterForSqlMapper parameter in parameterCollection)
                    {
                        dynamicParameter.Add(parameter.Name, parameter.Value, parameter.DbType, parameter.ParameterDirection);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dynamicParameter;
        }

        public List<string> GetUserEmail()
        {
            try
            {

                var GetAllUserEmail = _compsDb.UserProfile.Select(x => x.UserName).ToList();
                return GetAllUserEmail;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<tm_Region> GetRegionList()
        {
            try
            {

                var GetRegionList = _compsDb.tm_Region.ToList();
                return GetRegionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<tm_OfficeLocation> GetLocationBasedOnRegion(int RegionId)
        {
            try
            {

                var GetLocationList = _compsDb.tm_OfficeLocation.Where(x=>x.RegionId==RegionId).ToList();
                return GetLocationList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
