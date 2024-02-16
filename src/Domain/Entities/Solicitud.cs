using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Solicitudes")]
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
        public double Monto { get; set; }
        public int TipoCliente { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Observacion { get; set; }
    }
}