using Benchmarking.Model;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Benchmarking.DAL
{
    public sealed class DbConnection
    {
        private static DbConnection instance = null;
        private static readonly object padlock = new object();
        DbConnection()
        {

        }
        public static DbConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DbConnection();
                        }
                    }
                }
                return instance;
            }
        }
        public SqlConnection GetSqlConnection(ConnectionStringsCollection conName)
        {
            
            SqlConnection sql = null;
            try
            {
                string constring = Convert.ToString(ConfigurationSettings.connectionStrings.ToList().Where(x => x.key.Equals(conName)).Select(x => x.value).FirstOrDefault());
                if (!string.IsNullOrEmpty(constring))
                {
                    sql = new SqlConnection(constring);
                    sql.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }
    }
}
