﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Entry;

ServiceProvider serviceProvider = new ServiceCollection()
    .AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole()
        )
    .BuildServiceProvider();

var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

string? gamesConnectionString = Environment.GetEnvironmentVariable("NHL_DATABASE");

if (gamesConnectionString == null)
{
    var config = new ConfigurationBuilder().AddJsonFile("appsettings.Local.json").Build();
    gamesConnectionString = config.GetConnectionString("NHL_DATABASE");
}
if (gamesConnectionString == null)
    throw new Exception("Connection String Null");

var dataGetter = new DataCleaner(loggerFactory);
await dataGetter.Main(gamesConnectionString);
