// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA;

public class TokenUsage
{
    public int Id { get; set; }
    public DateTime QueryTimestamp { get; set; }
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
}
