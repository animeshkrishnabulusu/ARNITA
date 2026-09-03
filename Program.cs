// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using ARNITA;
using ARNITA.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddDbContext<ArnitaDbContext>(options =>
    options.UseSqlite("Data Source=arnita.db"));
builder.Services.AddTransient<IRequestBuilder, RequestBuilder>();
builder.Services.AddTransient<IMessageClient, MessageClient>();
builder.Services.AddTransient<IResponseWriter, ResponseWriter>();
builder.Services.AddTransient<ICommand, AskCommand>();
builder.Services.AddTransient<ICommand, HelpCommand>();
builder.Services.AddTransient<ICommand, QuitCommand>();
builder.Services.AddTransient<ICommand, RandomCommand>();
builder.Services.AddSingleton<CommandDispatcher>();
builder.Services.AddTransient<App>();

var config = builder.Configuration;
builder.Services.Configure<AnthropicOptions>(config.GetSection("Anthropic"));
builder.Services.Configure<HttpOptions>(config.GetSection("Http"));
builder.Services.Configure<GeneralOptions>(config.GetSection("General"));

builder.Services.AddHttpClient("anthropic")
    .ConfigureHttpClient((sp,
        client) =>
    {
        var options = sp.GetRequiredService<IOptions<AnthropicOptions>>()
            .Value;
        var httpOptions = sp.GetRequiredService<IOptions<HttpOptions>>()
            .Value;

        client.BaseAddress = new Uri(options.BaseAddress);
        client.Timeout = httpOptions.Timeout;
        client.DefaultRequestHeaders.Add("x-api-key",
            sp.GetRequiredService<IConfiguration>()["ApiKeys:Anthropic"]);
        client.DefaultRequestHeaders.Add("anthropic-version",
            options.Version);
        client.DefaultRequestHeaders.UserAgent.ParseAdd(httpOptions.UserAgent);
    });

using var host = builder.Build();

var dbContext = host.Services.GetRequiredService<ArnitaDbContext>();
await dbContext.Database.EnsureCreatedAsync();

var app = host.Services.GetRequiredService<App>();

using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

await app.RunAsync(cts.Token);
