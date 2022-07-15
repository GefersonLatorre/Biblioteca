using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Biblioteca.Models.ViewModels
{
    public class LibroVM
    {
        [Required]
        [StringLength(100)]
        [Display(Name ="Titulo")]
        public string Titulo { get; set; }
        [Required]
        [Display(Name = "Año")]
        public int Anio { get; set; }
        [StringLength(100)]
        [Display(Name = "Genero")]
        public string Genero { get; set; }
        [Display(Name = "Paginas")]
        public int Paginas { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Autor")]
        public string Autor { get; set; }
        public int Id { get; set; }
    }
}