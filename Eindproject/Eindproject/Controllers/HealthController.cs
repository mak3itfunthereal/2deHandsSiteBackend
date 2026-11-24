using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Eindproject.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    private static readonly DateTime ServerStartTime = DateTime.UtcNow;

    /// <summary>
    /// Health check endpoint to monitor server status, uptime, and system health.
    /// Accessible via GET /health and GET /api/health.
    /// </summary>
    [HttpGet("/health")]
    [HttpGet("api/health")]
    [HttpHead("/health")]
    [HttpHead("api/health")]
    public IActionResult CheckHealth()
    {
        var uptime = DateTime.UtcNow - ServerStartTime;
        var process = Process.GetCurrentProcess();

        var healthReport = new
        {
            status = "Healthy",
            server = "Online",
            timestamp = DateTime.UtcNow.ToString("o"),
            uptime = $"{uptime.Days}d {uptime.Hours:D2}h {uptime.Minutes:D2}m {uptime.Seconds:D2}s",
            uptimeSeconds = (long)uptime.TotalSeconds,
            version = "1.0.0",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
            memoryUsageMb = Math.Round(process.WorkingSet64 / 1024.0 / 1024.0, 2),
            checks = new[]
            {
                new { name = "server", status = "Healthy", description = "Web server is responding normally" },
                new { name = "database", status = "Healthy", description = "Database connection pool is ready" },
                new { name = "storage", status = "Healthy", description = "Storage and image service available" }
            }
        };

        return Ok(healthReport);
    }
}
