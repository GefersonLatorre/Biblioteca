using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models.ViewModels
{
    public class ListaLibroVM
    {
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public string Genero { get; set; }
        public int Paginas { get; set; }
        public string Autor { get; set; }
        public int Id { get; set; }
    }
}