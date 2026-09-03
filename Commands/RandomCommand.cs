// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

namespace ARNITA.Commands;

public class RandomCommand : ICommand
{
    private static readonly Random _random = new();

    public string Name => "random";

    public Task<CommandResult> ExecuteAsync(string arguments, CancellationToken ct)
    {
        return Task.FromResult(new CommandResult(Output: RandomResponse(arguments.Length, ct), ShouldExit: false));
    }

    private static string RandomResponse(int length, CancellationToken ct)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        string result = new(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());

        // Add spaces every 5 characters
        // TODO  make spacing random
        return string.Join(" ",
            Enumerable.Range(0, (result.Length + 4) / 5)
                .Select(i => result.Substring(i * 5, Math.Min(5, result.Length - i * 5))));
    }
}
