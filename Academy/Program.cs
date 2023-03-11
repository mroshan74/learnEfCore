using Academy;
using Microsoft.Extensions.Configuration;

/*
 * #(fix) links - Read more
 * https://stackoverflow.com/questions/39157781/the-configuration-file-appsettings-json-was-not-found-and-is-not-optional
 * https://stackoverflow.com/questions/38114761/asp-net-core-configuration-for-net-core-console-application
 * https://stackoverflow.com/questions/31885912/how-to-read-values-from-config-json-in-console-application
 * https://stackoverflow.com/questions/53605249/json-configuration-in-full-net-framework-console-app
 */

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", false);

IConfigurationRoot configuration = builder.Build();
var dbString = configuration[Constants.ConnectionString];

Console.WriteLine("db string: " + dbString);