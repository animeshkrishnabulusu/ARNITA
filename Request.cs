// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using System.Text.Json.Serialization;

namespace ARNITA;

public record Request(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("max_tokens")]
    int MaxTokens,
    [property: JsonPropertyName("messages")]
    Message[] Messages);
