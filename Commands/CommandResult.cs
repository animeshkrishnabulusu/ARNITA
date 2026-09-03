// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA.Commands;

public sealed record CommandResult(
    string? Output = null,
    bool ShouldExit = false);
