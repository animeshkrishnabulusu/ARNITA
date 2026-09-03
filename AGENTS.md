# ARNITA

# Purpose

A small coding agent for learning agentic application development.

# General Instructions

- Prefer small, incremental changes.
- Do not introduce new dependencies unless necessary.
- Follow the existing project structure and coding conventions.
- Before making significant architectural changes, explain the proposed change.
- Do not modify unrelated files.
- Prefer readable, maintainable code over clever abstractions.
- Add or update tests when behavior changes.
- Run relevant tests after code changes when possible.

# Project Context

- Language: C#
- Runtime: .NET
- This is primarily a learning project.
- Keep implementations simple unless additional complexity has a clear benefit.

# Commands

## review

When the prompt starts with `review`:

- Review the project or requested area for:
  - correctness
  - maintainability
  - architecture
  - naming
  - error handling
  - unnecessary complexity
  - testing
  - applicable C#/.NET best practices

- Do not modify source code unless explicitly asked.

- Append the review to `refactor.md`.

- Add a heading containing the current date and time.

Example:

    ## 2026-08-18 19:30 — General Review

- If only `review` is supplied, perform a general project review.

- If text follows `review`, restrict the review primarily to that topic.

Examples:

    review
    review exception handling
    review dependency injection
    review LlmClient
    review tests

# Documentation Responses

When a request produces analysis, explanation, design discussion,
review findings, research notes, or other useful non-code output:

- Write the complete useful response to `notes.md`.
- Append rather than overwrite existing content.
- Add a date/time heading for each entry.
- Still provide a short summary in the interactive Crush response.

Do not write routine conversational responses, confirmations,
tool output, or trivial status messages to `notes.md`.

# Persistent Notes

Persist substantial non-code findings as follows:

- Code reviews and refactoring findings -> `refactor.md`
- Architecture/design discussions -> `notes.md`
- Explanations worth retaining -> `notes.md`
- Temporary observations -> do not persist
- Normal conversation -> do not persist

Always append entries with a date/time heading.
Never overwrite previous entries.