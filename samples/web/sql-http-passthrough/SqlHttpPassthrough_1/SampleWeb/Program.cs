﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NServiceBus.Transport.SqlServerNative;

namespace SampleWeb
{
    class Program
    {
        public static async Task Main()
        {
            using (var connection = await ConnectionHelpers.OpenConnection(SqlHelper.ConnectionString)
                .ConfigureAwait(false))
            {
                var deduplicationManager = new DeduplicationManager(connection, "Deduplication");
                await deduplicationManager.Create().ConfigureAwait(false);
            }

            var builder = WebHost.CreateDefaultBuilder();
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.UseStartup<Startup>();
            var webHost = builder.Build();
            webHost.Run();
        }
    }
}