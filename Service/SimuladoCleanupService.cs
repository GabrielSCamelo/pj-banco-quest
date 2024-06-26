using pj_banco_quest.Data;

namespace pj_banco_quest.Service
{
    public class SimuladoCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SimuladoCleanupService> _logger;

        public SimuladoCleanupService(IServiceProvider serviceProvider, ILogger<SimuladoCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanUpExpiredSimulados();
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Executa uma vez ao dia
            }
        }

        private async Task CleanUpExpiredSimulados()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ContextDb>();

                // Busca simulados expirados
                var simuladosExpirados = context.Simulados.Where(s => s.DataExpiracao < DateTime.Now).ToList();

                if (simuladosExpirados.Any())
                {
                    _logger.LogInformation($"Removendo {simuladosExpirados.Count} simulados expirados.");
                    context.Simulados.RemoveRange(simuladosExpirados);
                    await context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogInformation("Nenhum simulado expirado encontrado para remoção.");
                }
            }
        }
    }
}