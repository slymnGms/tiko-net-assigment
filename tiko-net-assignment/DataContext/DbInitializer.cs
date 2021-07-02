using Dapper;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Threading.Tasks;

namespace tiko_net_assignment.DataContext
{
    public class DbInitializer
    {
        internal static async Task Initialize(IServiceProvider services)
        {
            var config = services.GetService<IConfiguration>();
            using IDbConnection db = new SQLiteConnection(config.GetConnectionString("DefaultConnection"));
            if (db.State == ConnectionState.Closed) db.Open();
            await db.QueryAsync(@"
                CREATE TABLE IF NOT EXISTS Cities (  
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                    [Name] NVARCHAR(128) NOT NULL,
                    [IsDeleted] BOOLEAN NOT NULL
                    );
                CREATE TABLE IF NOT EXISTS Agents (  
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                    [Name] NVARCHAR(128) NOT NULL,
                    [IsDeleted] BOOLEAN NOT NULL,
                    [CityId] INTEGER NOT NULL
                    );
                CREATE TABLE IF NOT EXISTS Houses (  
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
                    [Price] NVARCHAR(128) NOT NULL,
                    [Address] NVARCHAR(500) NOT NULL,
                    [Description] NVARCHAR(500),
                    [BedroomCount] INTEGER NOT NULL,
                    [IsDeleted] BOOLEAN NOT NULL,
                    [AgentId] INTEGER NOT NULL,
                    [CityId] INTEGER NOT NULL
                    );
            ");
            if (db.State == ConnectionState.Open) db.Close();
        }
    }
}
