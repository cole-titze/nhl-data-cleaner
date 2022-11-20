﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Entry;

ServiceProvider serviceProvider = new ServiceCollection()
    .AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole()
        )
    .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

string? gamesConnectionString = Environment.GetEnvironmentVariable("NHL_DATABASE");

if (gamesConnectionString == null)
{
    var config = new ConfigurationBuilder().AddJsonFile("appsettings.Local.json").Build();
    gamesConnectionString = config.GetConnectionString("NHL_DATABASE");
}
if (gamesConnectionString == null)
    throw new Exception("Connection String Null");

var dataGetter = new DataCleaner(logger);
await dataGetter.Main(gamesConnectionString);
