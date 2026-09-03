// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ARNITA;

public class ResponseWriter : IResponseWriter
{
    private readonly IOptions<GeneralOptions> _generalOptions;
    private readonly ILogger<ResponseWriter> _logger;
    private const string TimestampFormat = "yyyyMMdd_HHmmss";
    private const string ResponseFilePrefix = "response_";

    public ResponseWriter(IOptions<GeneralOptions> generalOptions, ILogger<ResponseWriter> logger)
    {
        _generalOptions = generalOptions;
        _logger = logger;
    }

    public async Task WriteResponseAsync(string content, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            _logger.LogWarning("Response content is empty; skipping file write.");
            return;
        }

        try
        {
            var outputDirectory = _generalOptions.Value.OutputDirectory;

            if (string.IsNullOrWhiteSpace(outputDirectory))
                throw new InvalidOperationException("Output directory is not configured.");

            Directory.CreateDirectory(outputDirectory);
            _logger.LogDebug("Output directory created/verified: {Directory}", outputDirectory);

            var timestamp = DateTime.Now.ToString(TimestampFormat);
            var filePath = Path.Combine(outputDirectory, $"{ResponseFilePrefix}{timestamp}.json");

            await File.WriteAllTextAsync(filePath, content, cancellationToken);

            _logger.LogInformation("Response successfully written to {FilePath}", filePath);
            Console.WriteLine($"Success! Written to {filePath}");
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "IOException occurred while writing response file.");
            throw;
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied while writing to output directory.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while writing response file.");
            throw;
        }
    }
}
