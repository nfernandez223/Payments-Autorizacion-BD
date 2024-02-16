using Application.Interfaces;
using Domain.Entities;
using InfraEstructure.Persistence;

namespace InfraEstructure.Services
{
    public class ContextMgmt: IContextMgmt
    {
        private readonly AppDbContext _dbContext;

        public ContextMgmt(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateSolicitud(Solicitud solicitud)
        {
            _dbContext.Update(solicitud);
            _dbContext.SaveChanges();
        }

        public List<Solicitud> ObtenerSolicitudesPendientesMasDe5Minutos()
        {
            DateTime hace5Min = DateTime.Now.AddMinutes(-5);

            var solicitudes = _dbContext.Solicitud
                .Where(s => s.Estado == "Pendiente" && s.Fecha < hace5Min)
                .ToList();

            return solicitudes;
        }        
        public List<Solicitud> ObtenerSolicitudesAutorizadas()
        {
            var solicitudes = _dbContext.Solicitud
                .Where(s => s.Estado == "Autorizada")
                .ToList();

            return solicitudes;
        }
    }
}
