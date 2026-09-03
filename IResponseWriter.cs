// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA;

public interface IResponseWriter
{
    Task WriteResponseAsync(string content, CancellationToken cancellationToken = default);
}
