using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class Planta
    {
        [Key]
        [Display(Name = "Planta")]
        public int PlantaId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Es de procesamiento?")]
        public bool Procesamiento { get; set; }

        //Propiedades de navegacion
        public int DepartamentoID { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<ProductoAsignado> ProductosAsignados { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }



    }
}
