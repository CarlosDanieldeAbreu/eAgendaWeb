using eAgenda.Infra.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace eAgenda.Webpi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfiguracaoLogseAgenda.ConfigurarEscritaLogs();
            Log.Logger.Information("Iniciando o servidor da aplicação E-Agenda...");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex, "O servidor da aplicação parou inesperadamente");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
