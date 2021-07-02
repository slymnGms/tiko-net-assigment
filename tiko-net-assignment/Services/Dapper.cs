using Dapper;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace tiko_net_assignment.Services
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";

        public Dapperr(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {

        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }


        public List<T> List<T>(string query)
        {
            using IDbConnection db = new SQLiteConnection(_config.GetConnectionString(Connectionstring));
            return db.Query<T>(query, commandType: CommandType.Text).ToList();
        }

        public List<T> ListWithParameters<T>(string query, DynamicParameters parameters)
        {
            using IDbConnection db = new SQLiteConnection(_config.GetConnectionString(Connectionstring));
            return db.Query<T>(query, parameters, commandType: CommandType.Text).ToList();
        }

        public T IO<T>(string commandText, DynamicParameters parameters)
        {
            T result;
            using IDbConnection db = new SQLiteConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed) db.Open();
                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query(commandText, parameters, commandType: CommandType.Text).FirstOrDefault();
                    tran.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
            return default(T);
        }
    }

}
