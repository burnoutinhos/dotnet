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

        /// <summary>
        /// Verifica o status de saúde da aplicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/telemetry/health
        ///
        /// </remarks>
        /// <returns>Status de saúde da aplicação.</returns>
        /// <response code="200">Retorna o status de saúde.</response>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Retorna informações sobre métricas da aplicação.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/telemetry/metrics
        ///
        /// </remarks>
        /// <returns>Informações sobre métricas.</returns>
        /// <response code="200">Retorna informações sobre métricas exportadas via OpenTelemetry.</response>
        [HttpGet("metrics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        /// <summary>
        /// Executa um teste de rastreamento distribuído.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/telemetry/trace-test
        ///     {
        ///        "testData": "Exemplo de dados para teste de trace"
        ///     }
        ///
        /// </remarks>
        /// <param name="data">Dados para teste de rastreamento.</param>
        /// <returns>Informações do trace executado.</returns>
        /// <response code="200">Retorna informações do trace incluindo TraceId e SpanId.</response>
        [HttpPost("trace-test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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