using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport.Models.Tablas
{
    public class Cliente
    {
        [Key]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Apellidos { get; set; }
        
        [Range(0, 999999999999, ErrorMessage = "{0} permite de {1} a 12 digitos")]
        public int Telefono { get; set; }

        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string Direccion { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime Nacimiento { get; set; }

        [Required]
        [Display(Name = "Genero")]
        public bool Masculino { get; set; }
        
        [Required]
        [StringLength(13, ErrorMessage = "{0} tiene un maximo permitido de {1} caracteres.")]
        public string DPI { get; set; }
        
        [Range(0, 999999999, ErrorMessage = "{0} permite de {1} a 9 digitos")]
        public int NIT { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Inicio { get; set; }

        [Display(Name = "Numero de pedidos")]
        public int NumeroPedidos { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Inversion { get; set; }

        //propiedad de navegacion
        public SolicitudTransporte SolicitudTransporte { get; set; }

    }
}