using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tiko_net_assignment.DataContext
{
    public class DbInitializer
    {
        internal static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetService<AppContext>();
            await context.Database.MigrateAsync();
        }
    }
}
