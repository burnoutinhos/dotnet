using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private static readonly ActivitySource ActivitySource = new("GpsMottuAPI");
        private static readonly Meter Meter = new("GpsMottuAPI");
        private static readonly Counter<long> RequestCounter = Meter.CreateCounter<long>("custom_requests_total");

        [HttpGet("health")]
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            using var activity = ActivitySource.StartActivity("HealthCheck");
            activity?.SetTag("operation", "health-check");
            
            RequestCounter.Add(1, new KeyValuePair<string, object?>("endpoint", "health"));

            var healthStatus = new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
            };

            return Ok(healthStatus);
        }

        [HttpGet("metrics")]
        [AllowAnonymous]
        public IActionResult GetMetrics()
        {
            using var activity = ActivitySource.StartActivity("GetMetrics");
            activity?.SetTag("operation", "get-metrics");

            RequestCounter.Add(1, new KeyValuePair<string, object?>("endpoint", "metrics"));

            var metrics = new
            {
                Timestamp = DateTime.UtcNow,
                RequestCount = "Veja os logs do console para métricas detalhadas",
                Message = "Métricas sendo exportadas via OpenTelemetry"
            };

            return Ok(metrics);
        }

        [HttpPost("trace-test")]
        [AllowAnonymous]
        public async Task<IActionResult> TraceTest([FromBody] object data)
        {
            using var activity = ActivitySource.StartActivity("TraceTest");
            activity?.SetTag("operation", "trace-test");
            activity?.SetTag("data.received", data?.ToString() ?? "null");

            RequestCounter.Add(1, new KeyValuePair<string, object?>("endpoint", "trace-test"));

            // Simula uma operação assíncrona
            await Task.Delay(100);

            activity?.SetTag("processing.completed", true);

            return Ok(new
            {
                Message = "Trace test executado com sucesso",
                TraceId = Activity.Current?.TraceId.ToString(),
                SpanId = Activity.Current?.SpanId.ToString(),
                Timestamp = DateTime.UtcNow
            });
        }
    }
}