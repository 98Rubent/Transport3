using System.Collections.Generic;
using Transport.Models.Tablas;

namespace Transport.Models.ViewModels
{
    public class PlantaDatosIndice
    {
        public IEnumerable<Planta> Plantas { get; set; }
        public IEnumerable<ProductoAsignado> ProductosAsignados { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}
