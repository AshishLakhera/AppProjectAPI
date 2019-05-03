using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Benchmarking.Model.Model.Dapper
{
    public class QueryParameterForSqlMapper
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ParameterDirection ParameterDirection { get; set; }
        public DbType? DbType { get; set; }
    }
    public class DataTableParameter
    {
        public string ParameterName { get; set; }
        public DataTable DataTable { get; set; }
    }
}
