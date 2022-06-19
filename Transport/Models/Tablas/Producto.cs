using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.Models.Tablas
{
    public class Producto
    {
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        //public string CompanyNo { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required]
        public int Existencias { get; set; }

        [Required]
        [Display(Name = "Unidad de medida")]
        [StringLength(3, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string UnidadMedida { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Display(Name = "Costo por unidad")]
        public decimal CostoUnidad { get; set; }

        //propiedades de navegacion
        public ICollection<Directorio> Directorios { get; set; }
        public ICollection<ProductoAsignado> ProductosAsignados { get; set; }

    }
}