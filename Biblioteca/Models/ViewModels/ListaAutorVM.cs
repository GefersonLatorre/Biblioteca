using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models.ViewModels
{
    public class ListaAutorVM
    {
        public string Nombre { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Lugar_Nacimiento { get; set; }
        public string Correo { get; set; }
        public int Numero_Libros { get; set; }
        public int Id { get; set; }
    }
}