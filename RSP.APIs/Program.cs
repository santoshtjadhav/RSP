using Autofac.Extensions.DependencyInjection;
using RSP.APIs;
using RSP.Game;
using RSP.Game.Actions;
using System.ComponentModel;
using Autofac;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}