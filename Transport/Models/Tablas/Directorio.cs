using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class Directorio
    {
        [Key]
        [Display(Name = "Directorio")]
        public int DirectorioId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Direccion { get; set; }

        [StringLength(500, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Indicaciones { get; set; }

        [Required]
        [Display(Name ="Telefono de contacto")]
        [Range(0, 99999999, ErrorMessage = "{0} permite de {1} a 9 digitos")]
        public int Contacto { get; set; }
        
        [EmailAddress]
        public string Correo { get; set; }

        //Propiedades de navegacion
        public int TipoLugarID { get; set; }
        public TipoLugar TipoLugar { get; set; }


    }
}
