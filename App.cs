// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using ARNITA.Commands;
using Microsoft.Extensions.Logging;

namespace ARNITA;

public class App
{
    private readonly CommandDispatcher _dispatcher;
    private readonly ILogger<App> _logger;

    public App(CommandDispatcher dispatcher, ILogger<App> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }
    public async Task RunAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                Console.Write("input> ");
                var input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                var result = await _dispatcher.ExecuteAsync(input, token);

                if (result.ShouldExit)
                {
                    break;
                }

                if (result.Output is not null)
                {
                    Console.WriteLine(result.Output);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Command execution failed");
                Console.WriteLine("An error occurred. Please try again.");
            }
        }
    }

    // private async Task ShowLoadingMessage()
    // {
    //     Console.WriteLine(_loadingMessages[_random.Next(_loadingMessages.Length)]);
    //     await Task.Delay(_random.Next(190, 450));
    // }

    // private readonly string[] _loadingMessages =
    // [
    //     "Processing ...", "Untangling ...", "Defining scope ...", "Following instructions ...",
    //     "Checking constraints ...", "Solving for correctness ...", "Parsing your request ...", "Gathering context ...",
    //     "Identifying key details ...", "Mapping the problem ...", "Exploring possible approaches ...",
    //     "Weighing relevant information ...", "Connecting the dots ...", "Testing assumptions ...",
    //     "Refining the reasoning ...", "Checking for edge cases ...", "Structuring the response ...",
    //     "Choosing clear wording ...", "Verifying the result ...", "Polishing the explanation ...",
    //     "Preparing the final answer ..."
    // ];
}
