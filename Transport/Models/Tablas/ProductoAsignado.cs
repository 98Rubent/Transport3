using System.ComponentModel.DataAnnotations;

namespace Transport.Models.Tablas
{
    public class ProductoAsignado
    {

        [Display(Name = "Producto")]
        public int ProductoID { get; set; }
        [Display(Name = "Planta")]
        public int PlantaID { get; set; }

        //propiedades de navegacion
        public Producto Producto { get; set; }
        public Planta Planta { get; set; }
    }
}