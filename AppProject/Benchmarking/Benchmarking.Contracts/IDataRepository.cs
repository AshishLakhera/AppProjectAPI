using Benchmarking.Model.Model.Dapper;
using System.Collections.Generic;
using System.Data;

namespace Benchmarking.Contracts
{
    public interface IDataRepository
    {
        DataSet ExecuteQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null);
        int ExecuteNonQuery(string spName, IList<QueryParameterForSqlMapper> QPCollection = null, IList<DataTableParameter> DTPCollection = null);
        IEnumerable<T> ExecuteQuery<T>(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);
        dynamic FetchRecordSet(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);
        IList<dynamic> FetchMultipleRecordSet(string storedProcedure, IList<QueryParameterForSqlMapper> parameterCollection);
    }
}
