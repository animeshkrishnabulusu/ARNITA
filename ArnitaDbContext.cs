// SPDX-FileCopyrightText: 2026 Animeshkrishna Bulusu <code@baksignal.com>
// SPDX-License-Identifier: GPL-3.0-or-later

using Microsoft.EntityFrameworkCore;

namespace ARNITA;

public class ArnitaDbContext(DbContextOptions<ArnitaDbContext> options) : DbContext(options)
{
    public DbSet<TokenUsage> TokenUsage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TokenUsage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.QueryTimestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
        });
    }
}
