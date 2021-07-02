using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace tiko_net_assignment.Services
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();
        List<T> List<T>(string query);
        List<T> ListWithParameters<T>(string query, DynamicParameters parameters);
        T IO<T>(string commandText, DynamicParameters parameters);
    }

}
