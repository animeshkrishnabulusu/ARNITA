// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ARNITA.Commands;

public class AskCommand : ICommand
{
    private readonly IRequestBuilder _requestBuilder;
    private readonly IMessageClient _messageClient;
    private readonly IResponseWriter _responseWriter;
    private readonly ILogger<App> _logger;
    private readonly ArnitaDbContext _context;
    public string Name => "ask";

    public AskCommand(IRequestBuilder requestBuilder, IMessageClient messageClient, IResponseWriter responseWriter,
        ILogger<App> logger, ArnitaDbContext context)
    {
        _requestBuilder = requestBuilder;
        _messageClient = messageClient;
        _responseWriter = responseWriter;
        _logger = logger;
        _context = context;
    }

    public async Task<CommandResult> ExecuteAsync(string arguments, CancellationToken ct)
    {
        return await AskAsync(arguments, ct);
    }

    private async Task<CommandResult> AskAsync(string input, CancellationToken ct)
    {
        try
        {
            var request = _requestBuilder.BuildRequest(input);
            _requestBuilder.ValidateRequest(request);
            var messageResponse = await _messageClient.SendMessageAsync(request, ct);
            var content = JsonSerializer.Serialize(messageResponse);
            await _responseWriter.WriteResponseAsync(content, ct);

            var cost = CalculateQueryCost(messageResponse.Usage.InputTokens, messageResponse.Usage.OutputTokens);
            var queryOutput =
                $"{messageResponse.Content[0].Text}\n'Input tokens: {messageResponse.Usage.InputTokens}, Output tokens: {messageResponse.Usage.OutputTokens}, Estimated cost: ${cost}'";

            await AddTokenUsage(messageResponse.Usage.InputTokens, messageResponse.Usage.OutputTokens);

            return new CommandResult(Output: $"{queryOutput}", ShouldExit: false);
        }
        catch (Exception ex) when (ex is ArgumentException or HttpRequestException or JsonException)
        {
            _logger.LogError(ex, "Unhandled exception: {ErrorMessage}", ex.Message);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Output configuration error: {ErrorMessage}", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during request processing: {ErrorMessage}", ex.Message);
        }

        return new CommandResult(Output: "An error occurred while processing your request.", ShouldExit: false);
    }

    private async Task AddTokenUsage(int inputTokens, int outputTokens)
    {
        TokenUsage usage = new() { InputTokens = inputTokens, OutputTokens = outputTokens };
        _context.TokenUsage.Add(usage);
        await _context.SaveChangesAsync();
    }

    private static decimal CalculateQueryCost(int inputTokens, int outputTokens)
    {
        const decimal inputRatePerMillion = 1.00m; // Sonnet 4.5
        const decimal outputRatePerMillion = 5.00m;

        return (inputTokens * inputRatePerMillion / 1_000_000m) + (outputTokens * outputRatePerMillion / 1_000_000m);
    }
}
