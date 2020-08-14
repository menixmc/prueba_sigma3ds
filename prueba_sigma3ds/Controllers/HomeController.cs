using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
using Data;
using Data.Models;

namespace prueba_sigma3ds.Controllers
{
    public class HomeController : Controller
    {
     
        // Creado por: Alexis quiñones castillo

         readonly Business cnegocio = new Business();
         Respuesta rta = new Respuesta();


        /// <summary>
        /// Metodo que carga la vista inicial
        /// </summary>
        /// <param name="rta"></param>
        /// <returns></returns>
        public ActionResult Index(int? rta)
        {
            ViewBag.departamentos = from Departamentos d in Enum.GetValues(typeof(Departamentos))
                                 select new SelectListItem
                                 {
                                     Value = ((int)d).ToString(),
                                     Text = Business.GetDescriptionFiltro(d)
                                 };
            ViewBag.rta = rta;

            return View();
        }

        /// <summary>
        /// Metodo jsonresult para obtener las ciudades dependiendo del departamento
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public JsonResult GetCiudades(string departamento)
        {
            try
            {
                List<string> Ciudades = cnegocio.GetCiudadesByDepartamento(departamento, ref rta);
                if (rta.respuesta)
                {
                    return Json(new { rta = 0, ciudades = Ciudades }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { rta = 1, Msg = rta.descripcion }, JsonRequestBehavior.AllowGet);
                }

            }catch(Exception ex)
            {
                return Json(new { rta = 1, Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Metodo para guardar un contacto
        /// </summary>
        /// <param name="dpto"></param>
        /// <param name="ciudad"></param>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <returns></returns>
        public JsonResult GuardarContacto(string dpto, string ciudad, string nombre, string correo)
        {
            contacts contacto = new contacts();
            contacto.name = nombre;
            contacto.state = dpto;
            contacto.city = ciudad;
            contacto.email = correo;

            try
            {
                cnegocio.GuardarContacto(contacto, ref rta);
                if (rta.respuesta)
                {
                    return Json(new { rta = 0, Mgs = rta.descripcion }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { rta = 1, Mgs = rta.descripcion }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                rta.descripcion = "Hubo un problema guardando los datos: " + ex.Message;
                return Json(new { rta = 1, Mgs = rta.descripcion }, JsonRequestBehavior.AllowGet);
            }
        } 

    }
}