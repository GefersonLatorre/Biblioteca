using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteca.Models.ViewModels
{
    public class AutorVM
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime Fecha_Nacimiento { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Lugar de nacimiento")]
        public string Lugar_Nacimiento { get; set; }
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El campo Correo electronico no es una dirección de correo electrónico válida.")]
        public string Correo { get; set; }
        public int Numero_Libros { get; set; }
        public int Id { get; set; }
    }
}