using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.Models.Tablas
{
    public class SolicitudTransporte
    {
        [Key]
        [Display(Name = "Solicitud de transporte")]
        public int SolicitudTransporteID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de entrega")]
        public DateTime FechaEntrega { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Display(Name = "Unidad de medida")]
        [StringLength(3, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string UnidadMedida { get; set; }

        [Required]
        [Display(Name = "¿Quien recibe?")]
        [StringLength(100, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Receptor { get; set; }

        [Required]
        [Range(0, 999999999999, ErrorMessage = "{0} permite de {1} a 12 digitos")]
        public int Contacto { get; set; }

        [Required]
        public bool Pagado { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        //Propiedad de navegacion
        public int DestinoID { get; set; }
        public Directorio Destino { get; set; }
        public int ProductoID { get; set; }
        public Producto Producto { get; set; }
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }




    }
}
