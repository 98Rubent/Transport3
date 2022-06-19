using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class TipoVehiculo
    {
        [Key]
        [Display(Name = "Tipo de vehiculo")]
        public int TipoVehiculoID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")] 
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Capacidad de carga")] 
        public int Capacidad { get; set; }

        [Required]
        [Display(Name = "Unidad de medida")]
        [StringLength(3, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string UnidadMedida { get; set; }

        //Propiedad de navegacion
        public Vehiculo Vehiculo { get; set; }
    }
}