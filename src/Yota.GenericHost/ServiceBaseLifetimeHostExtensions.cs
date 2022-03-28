﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Yota.GenericHost
{
    public static class ServiceBaseLifetimeHostExtensions
    {
        public static IHostBuilder UseServiceBaseLifetime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) => services.AddSingleton<IHostLifetime, ServiceBaseLifetime>());
        }

        public static Task RunAsServiceAsync(this IHostBuilder hostBuilder,
            CancellationToken cancellationToken = default)
        {
            var runServiceTask = hostBuilder.UseWindowsService()
                .UseServiceBaseLifetime()
                .Build()
                .RunAsync(cancellationToken);

            return runServiceTask;
        }
    }
}