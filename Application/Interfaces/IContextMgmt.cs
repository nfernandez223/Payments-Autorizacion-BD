using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContextMgmt
    {
        void UpdateSolicitud(Solicitud solicitud);
        List<Solicitud> ObtenerSolicitudesPendientesMasDe5Minutos();
        List<Solicitud> ObtenerSolicitudesAutorizadas();
    }
}
