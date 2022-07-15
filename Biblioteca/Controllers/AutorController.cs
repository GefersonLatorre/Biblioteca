using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;
using Biblioteca.Models.ViewModels;

namespace Biblioteca.Controllers
{
    public class AutorController : Controller
    {

        public ActionResult Index()
        {
            List<ListaAutorVM> lst;
            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                lst = (from a in db.Autor
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
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(AutorVM model)
        {
            try
            {
                List<ListaAutorVM> lst;
                bool valido = true;
                using (BibliotecaEntities db = new BibliotecaEntities())
                {
                    lst = (from a in db.Autor
                           select new ListaAutorVM
                           {
                               Nombre = a.Nombre,
                           }).ToList();
                }
                foreach (var item in lst)
                {
                    if (model.Nombre == item.Nombre)
                    {
                        ModelState.AddModelError("Nombre", "El Autor ya se encuentra registrado");
                        valido = false;
                    }
                }
            
                if (ModelState.IsValid && valido)
                {
                    using (BibliotecaEntities db = new BibliotecaEntities())
                    {
                        var autor = new Autor();
                        autor.Nombre = model.Nombre;
                        autor.Fecha_Nacimiento = model.Fecha_Nacimiento;
                        autor.Lugar_Nacimiento = model.Lugar_Nacimiento;
                        autor.Correo = model.Correo;
                        autor.Numero_Libros = model.Numero_Libros;

                        db.Autor.Add(autor);
                        db.SaveChanges();
                    }
                    return Redirect("~/Autor/");
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
            AutorVM model = new AutorVM();
            using (BibliotecaEntities db = new BibliotecaEntities())
            {
                var autor = db.Autor.Find(Id);
                model.Nombre = autor.Nombre;
                model.Fecha_Nacimiento = autor.Fecha_Nacimiento;
                model.Lugar_Nacimiento = autor.Lugar_Nacimiento;
                model.Correo = autor.Correo;
                model.Numero_Libros = autor.Numero_Libros;
                model.Id = autor.Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(AutorVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BibliotecaEntities db = new BibliotecaEntities())
                    {
                        var autor = db.Autor.Find(model.Id);
                        autor.Nombre = model.Nombre;
                        autor.Fecha_Nacimiento = model.Fecha_Nacimiento;
                        autor.Lugar_Nacimiento = model.Lugar_Nacimiento;
                        autor.Correo = model.Correo;
                        autor.Numero_Libros = model.Numero_Libros;
                        autor.Id = model.Id;

                        db.Entry(autor).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Autor/");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}