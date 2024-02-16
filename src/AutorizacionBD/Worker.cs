using Application.Interfaces;
using System.Threading;

namespace AutorizacionBD
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageService _messageService;
        private readonly IContextMgmt _contextMgmt;

        public Worker(ILogger<Worker> logger, IMessageService messageSenderService, IContextMgmt contextMgmt)
        {
            _messageService = messageSenderService;
            _logger = logger;
            _contextMgmt = contextMgmt;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //Verifica solicitudes de mas de 5 min sin autorizar y actualiza a reversa
                    _logger.LogInformation("Verificacion solicitudes pendientes");
                    var solicitudespen = _contextMgmt.ObtenerSolicitudesPendientesMasDe5Minutos();

                    if (solicitudespen.Count > 0)
                    {
                        foreach (var solpendiente in solicitudespen)
                        {
                            solpendiente.Estado = "Reversa";
                            solpendiente.Observacion = "Accion de Reversa, no se recibio autorizacion";
                            _contextMgmt.UpdateSolicitud(solpendiente);
                        }
                    }

                    //Verifica solicitudes autorizadas y publica el aprobado. 
                    _logger.LogInformation("Verificacion solicitudes aprobadas");
                    var solicitudesaut = _contextMgmt.ObtenerSolicitudesAutorizadas();

                    if (solicitudesaut.Count > 0)
                    {
                        foreach (var solauto in solicitudesaut)
                        {
                            solauto.Estado = "Aprobada";
                            solauto.Observacion = "Autorizada";
                            _contextMgmt.UpdateSolicitud(solauto);
                            _logger.LogInformation("Publicar solicitud aprobada {IdSolicitud}", solauto.IdSolicitud);
                            _messageService.SendMessage(solauto);
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error Metodo Additional: {message}", ex.Message);
                }
            }
        }
    }
}