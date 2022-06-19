using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class TipoLugar
    {
        [Key]
        [Display(Name = "Tipo de lugar")]
        public int TipoLugarID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Lugar { get; set; }

        //Propiedades de navegacion
        public Directorio Directorio { get; set; }
    }
}