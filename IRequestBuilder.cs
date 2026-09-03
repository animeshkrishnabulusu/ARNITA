// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA;

public interface IRequestBuilder
{
    Request BuildRequest(string userContent);
    void ValidateRequest(Request request);
}
