// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using Microsoft.Extensions.Options;

namespace ARNITA;

public class RequestBuilder : IRequestBuilder
{
    private readonly IOptions<AnthropicOptions> _anthropicOptions;

    public RequestBuilder(IOptions<AnthropicOptions> anthropicOptions)
    {
        _anthropicOptions = anthropicOptions;
    }

    public Request BuildRequest(string userContent)
    {
        if (string.IsNullOrWhiteSpace(userContent))
            throw new ArgumentException("User content cannot be empty.",
                nameof(userContent));

        var options = _anthropicOptions.Value;
        ValidateAnthropicOptions(options);

        var request = new Request(
            options.Model,
            options.MaxTokens,
            [
                new Message(options.Role,
                    userContent)
            ]
        );

        // ValidateRequest(request);
        return request;
    }

    public void ValidateRequest(Request request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Model))
            throw new InvalidOperationException("Request model cannot be empty.");

        if (request.MaxTokens <= 0)
            throw new InvalidOperationException("Request max tokens must be greater than 0.");

        if (request.Messages == null || request.Messages.Length == 0)
            throw new InvalidOperationException("Request must contain at least one message.");
    }

    private static void ValidateAnthropicOptions(AnthropicOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Model))
            throw new InvalidOperationException("Anthropic model is not configured.");

        if (options.MaxTokens <= 0)
            throw new InvalidOperationException("Anthropic max tokens must be greater than 0.");

        if (string.IsNullOrWhiteSpace(options.Role))
            throw new InvalidOperationException("Anthropic role is not configured.");
    }
}
