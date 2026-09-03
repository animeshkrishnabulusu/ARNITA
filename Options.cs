// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using System.ComponentModel.DataAnnotations;

namespace ARNITA;

public class AnthropicOptions
{
    public required string ApiKey { get; set; }
    public required string Model { get; set; }

    [Range(1, 2048, ErrorMessage = "MaxTokens must be greater than 0")]
    public required int MaxTokens { get; set; }

    public required string Role { get; set; }
    public required string BaseAddress { get; set; }
    public required string Endpoint { get; set; }
    public required string Version { get; set; }
}

public class HttpOptions
{
    public required string UserAgent { get; set; }

    [Range(0, 60, ErrorMessage = "Timeout must be set between 0 and 60")]
    public required TimeSpan Timeout { get; set; }
}

public class GeneralOptions
{
    public required string OutputDirectory { get; set; }
}
