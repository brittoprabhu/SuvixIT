using Microsoft.EntityFrameworkCore;
using SuvixIT.Api.Models;

namespace SuvixIT.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<DemoRequest> DemoRequests => Set<DemoRequest>();
    public DbSet<EmailConfiguration> EmailConfigurations => Set<EmailConfiguration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Subscription>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<EmailConfiguration>()
            .HasData(new EmailConfiguration
            {
                Id = 1,
                SmtpHost = string.Empty,
                SmtpPort = 25,
                Username = string.Empty,
                Password = string.Empty,
                SenderAddress = string.Empty,
                RecipientAddress = string.Empty,
                UseSsl = true
            });
    }
}
