// // <copyright file="IGeoCosyClient.cs" company="">
// // Copyright (c) Daniel Cook. All rights reserved.
// // </copyright>
namespace GeoCosy.Sample.ConsoleApp
{
    using System.Threading;
    using System.Threading.Tasks;

    using GeoCosy.Enums;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        static async Task Main(string[] args)
        {
            /* For a web app, this goes in your Startup class ConfigureServices method
             
                .AddHttpClient()
                .AddTransient<IGeoCosyClient, GeoCosyClient>()
             
             */

            // Some basic DI for the console app
            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddTransient<IGeoCosyClient, GeoCosyClient>()
                .BuildServiceProvider();

            // Simply inject IGeoCosyClient into your constructor in a real scenario, for the console app, just get the service from the collection
            var cosyClient = serviceProvider.GetService<IGeoCosyClient>();

            // Obtain a JWT token
            var token = await cosyClient.LoginAsync("<your geo cosy web/app email address>", "<your geo cosy web/app password>");

            // Get a list of devices to control
            var devices = await cosyClient.GetSystems(token.Data.Token, true);

            // Set the mode
            await cosyClient.SetMode(token.Data.Token, devices.Data.SystemDetails[0].SystemId, CosyMode.Comfy);
            
            Thread.Sleep(10000);
            
            await cosyClient.SetMode(token.Data.Token, devices.Data.SystemDetails[0].SystemId, CosyMode.Cosy);
            
            Thread.Sleep(10000);
            
            await cosyClient.Hibernate(token.Data.Token);
           
            Thread.Sleep(10000);
        }
    }
}
