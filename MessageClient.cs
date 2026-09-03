// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ARNITA;

public class MessageClient : IMessageClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<AnthropicOptions> _options;
    private readonly ILogger<MessageClient> _logger;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public MessageClient(IHttpClientFactory httpClientFactory,
        IOptions<AnthropicOptions> options,
        ILogger<MessageClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
        _logger = logger;
    }

    public async Task<Response> SendMessageAsync(Request request,
        CancellationToken token = default)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("anthropic");
            var body = JsonSerializer.Serialize(request);
            var endpoint = _options.Value.Endpoint;

            var response = await client.PostAsync(endpoint,
                new StringContent(body,
                    Encoding.UTF8,
                    "application/json"),
                token);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(token);
            var messageResponse = JsonSerializer.Deserialize<Response>(content,
                                      _jsonOptions) ??
                                  throw new JsonException("Failed to deserialize response");

            if (messageResponse.Content.Length == 0)
            {
                throw new InvalidOperationException("Response contains no content blocks");
            }

            return messageResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex,
                "HTTP request failed");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex,
                "Failed to deserialize response");
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(ex,
                "Request was cancelled");
            throw;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unexpected error during message sending");
            throw;
        }
    }
}
