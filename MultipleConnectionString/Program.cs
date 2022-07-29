// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using MultipleConnectionString;

var services = Startup.ConfigureServices().BuildServiceProvider();
await services.GetRequiredService<App>().Run().ConfigureAwait(false);


Console.WriteLine("Hello, World!");