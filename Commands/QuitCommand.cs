// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA.Commands;

public class QuitCommand : ICommand
{
    public string Name =>
        "quit";

    public Task<CommandResult> ExecuteAsync(string arguments, CancellationToken ct)
    {
        return Task.FromResult(new CommandResult(
            Output: "Goodbye!",
            ShouldExit: true));
    }
}
