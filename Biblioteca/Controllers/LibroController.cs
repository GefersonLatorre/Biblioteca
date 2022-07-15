using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;
using Biblioteca.Models.ViewModels;

namespace Biblioteca.Controllers
{
    public class LibroController : Controller
    {

        public ActionResult Index()
        {
            List<ListaLibroVM> lst;
            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                lst = (from l in db.Libro
                       select new ListaLibroVM
                       {
                           Titulo = l.Titulo,
                           Anio = l.Anio,
                           Genero = l.Genero,
                           Paginas = l.Paginas,
                           Autor = l.Autor,
                           Id = l.Id
                       }).ToList();
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(LibroVM model)
        {
            try
            {
                List<ListaLibroVM> lstLibro;
                bool tituloValido = true;
                List<ListaAutorVM> lstAutor;
                bool autorValido = false;
                ListaAutorVM autorItem = new ListaAutorVM();
                bool numeroLibrosValido = true;

                using (BibliotecaEntities db = new BibliotecaEntities())
                {
                    lstLibro = (from l in db.Libro
                           select new ListaLibroVM
                           {
                               Titulo = l.Titulo,
                           }).ToList();
                }
                foreach (var item in lstLibro)
                {
                    if (model.Titulo == item.Titulo)
                    {
                        ModelState.AddModelError("Titulo", "El Libro ya se encuentra registrado");
                        tituloValido = false;
                    }
                }
                
                using (BibliotecaEntities db = new BibliotecaEntities())
                {
                    lstAutor = (from a in db.Autor
                           select new ListaAutorVM
                           {
                               Nombre = a.Nombre,
                               Fecha_Nacimiento = a.Fecha_Nacimiento,
                               Lugar_Nacimiento = a.Lugar_Nacimiento,
                               Correo = a.Correo,
                               Numero_Libros = a.Numero_Libros,
                               Id = a.Id
                           }).ToList();
                }
                foreach (var item in lstAutor)
                {
                    if (model.Autor == item.Nombre)
                    {
                        autorItem = item;
                        autorValido = true;
                        break;
                    }
                }
                if (!autorValido)
                {
                    ModelState.AddModelError("Autor", "El Autor no se encuentra registrado");
                }

                if (autorItem.Numero_Libros == 3)
                {
                    ModelState.AddModelError("Autor", "El Autor ya tiene registrado el maximo permitido de 3 libros");
                    numeroLibrosValido = false;
                }

                if (ModelState.IsValid && tituloValido && autorValido && numeroLibrosValido)
                {
                    using (BibliotecaEntities db = new BibliotecaEntities())
                    {
                        var libro = new Libro();
                        libro.Titulo = model.Titulo;
                        libro.Anio = model.Anio;
                        libro.Genero = model.Genero;
                        libro.Paginas = model.Paginas;
                        libro.Autor = model.Autor;

                        db.Libro.Add(libro);
                        db.SaveChanges();
                    }

                    using (BibliotecaEntities db = new BibliotecaEntities())
                    {
                        var autor = db.Autor.Find(autorItem.Id);
                        autor.Nombre = autorItem.Nombre;
                        autor.Fecha_Nacimiento = autorItem.Fecha_Nacimiento;
                        autor.Lugar_Nacimiento = autorItem.Lugar_Nacimiento;
                        autor.Correo = autorItem.Correo;
                        autor.Numero_Libros = autorItem.Numero_Libros + 1;
                        autor.Id = autorItem.Id;

                        db.Entry(autor).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Libro/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }

        public ActionResult Editar(int Id)
        {
            LibroVM model = new LibroVM();
            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                var libro = db.Libro.Find(Id);
                model.Titulo = libro.Titulo;
                model.Anio = libro.Anio;
                model.Genero = libro.Genero;
                model.Paginas = libro.Paginas;
                model.Autor = libro.Autor;
                model.Id = libro.Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(LibroVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BibliotecaEntities db = new BibliotecaEntities())
                    {
                        var libro = db.Libro.Find(model.Id);
                        libro.Titulo = model.Titulo;
                        libro.Anio = model.Anio;
                        libro.Genero = model.Genero;
                        libro.Paginas = model.Paginas;
                        libro.Autor = model.Autor;
                        libro.Id = model.Id;

                        db.Entry(libro).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Libro/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Eliminar(int Id)
        {
            LibroVM model = new LibroVM();
            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                var libro = db.Libro.Find(Id);
                model.Titulo = libro.Titulo;
                model.Anio = libro.Anio;
                model.Genero = libro.Genero;
                model.Paginas = libro.Paginas;
                model.Autor = libro.Autor;
                model.Id = libro.Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Eliminar(LibroVM model)
        {
            List<ListaAutorVM> lstAutor;
            ListaAutorVM autorItem = new ListaAutorVM();

            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                lstAutor = (from a in db.Autor
                            select new ListaAutorVM
                            {
                                Nombre = a.Nombre,
                                Fecha_Nacimiento = a.Fecha_Nacimiento,
                                Lugar_Nacimiento = a.Lugar_Nacimiento,
                                Correo = a.Correo,
                                Numero_Libros = a.Numero_Libros,
                                Id = a.Id
                            }).ToList();
            }
            foreach (var item in lstAutor)
            {
                if (model.Autor == item.Nombre)
                {
                    autorItem = item;
                    break;
                }
            }

            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                var autor = db.Autor.Find(autorItem.Id);
                autor.Nombre = autorItem.Nombre;
                autor.Fecha_Nacimiento = autorItem.Fecha_Nacimiento;
                autor.Lugar_Nacimiento = autorItem.Lugar_Nacimiento;
                autor.Correo = autorItem.Correo;
                autor.Numero_Libros = autorItem.Numero_Libros - 1;
                autor.Id = autorItem.Id;

                db.Entry(autor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                var libro = db.Libro.Find(model.Id);
                db.Libro.Remove(libro);
                db.SaveChanges();
            }
            return Redirect("~/Libro/");
        }

    }
}