// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA.Commands;

public sealed class CommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands;

    public CommandDispatcher(IEnumerable<ICommand> commands)
    {
        _commands = commands.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<CommandResult> ExecuteAsync(string rawInput, CancellationToken ct)
    {
        var parts = rawInput.Split(' ', StringSplitOptions.TrimEntries);

        var commandName = parts[0];
        var prompt = string.Join(" ", parts.Skip(1));

        if (!_commands.TryGetValue(commandName, out var command))
        {
            return new CommandResult($"Unknown command: {commandName}", ShouldExit: false);
        }

        return await command.ExecuteAsync(prompt, ct);
    }
}
