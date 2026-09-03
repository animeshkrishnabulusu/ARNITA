// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA.Commands;

public sealed class HelpCommand : ICommand
{
    public string Name => "help";

    public Task<CommandResult> ExecuteAsync(string arguments, CancellationToken ct)
    {
        return Task.FromResult(new CommandResult(Output: "ask <text>\nhelp\nquit", ShouldExit: false));
    }
}
