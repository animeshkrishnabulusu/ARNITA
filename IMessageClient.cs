// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA;

public interface IMessageClient
{
    Task<Response> SendMessageAsync(Request request, CancellationToken token = default);
}
