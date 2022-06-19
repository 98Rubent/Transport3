using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class Vehiculo
    {
        [Key]
        [Display(Name = "Vehiculo")]
        public int VehiculoId { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Placa { get; set; }
        
        [Required]
        [Range(0, 9999, ErrorMessage = "{0} permite de {1} a 4 digitos")]
        public int Modelo { get; set; }

        [StringLength(20, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Linea { get; set; }

        [Required]
        [Display(Name = "¿Esta en mantenimiento?")]
        public bool Mantenimiento { get; set; }
        
        [Required]
        [Display(Name = "¿Esta en ruta?")]
        public bool Ruta { get; set; }
        //Propiedades de navegacion
        public int TipoVehiculoID { get; set; }
        public TipoVehiculo TipoVehiculo { get; set; }

    }
}