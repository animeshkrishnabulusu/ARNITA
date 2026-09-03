// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using System.Text.Json.Serialization;

namespace ARNITA;

public class Response
{
    [property: JsonPropertyName("model")] public required string Model { get; init; }
    [property: JsonPropertyName("id")] public required string Id { get; init; }
    [property: JsonPropertyName("type")] public required string Type { get; init; }
    [property: JsonPropertyName("role")] public required string Role { get; init; }

    [property: JsonPropertyName("content")]
    public required Content[] Content { get; init; }

    [property: JsonPropertyName("stop_reason")]
    public required string StopReason { get; init; }

    [property: JsonPropertyName("stop_sequence")]
    public required object StopSequence { get; init; }

    [property: JsonPropertyName("stop_details")]
    public required object StopDetails { get; init; }

    [property: JsonPropertyName("usage")] public required Usage Usage { get; init; }
}

public class Content
{
    [property: JsonPropertyName("type")] public required string Type { get; set; }
    [property: JsonPropertyName("text")] public required string Text { get; set; }
}

public class Usage
{
    [property: JsonPropertyName("input_tokens")]
    public int InputTokens { get; set; }

    [property: JsonPropertyName("cache_creation_input_tokens")]
    public int CacheCreationInputTokens { get; set; }

    [property: JsonPropertyName("cache_read_input_tokens")]
    public int CacheReadInputTokens { get; set; }

    [property: JsonPropertyName("output_tokens")]
    public int OutputTokens { get; set; }

    [property: JsonPropertyName("service_tier")]
    public required string ServiceTier { get; set; }

    [property: JsonPropertyName("inference_geo")]
    public required string InferenceGeo { get; set; }
}
