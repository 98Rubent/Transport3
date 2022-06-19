using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class Departamento
    {
        [Key]
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name ="Numero de departamento")]
        [Range(0, 22, ErrorMessage = "{0} permite de {1} a {2} digitos")]
        public int Numero { get; set; }
        
        //Propiedad de navegacion
        public Planta Planta { get; set; }
    }
}