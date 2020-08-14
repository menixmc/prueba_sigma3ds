using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Data;
using System.ComponentModel;
using Data.Models;

namespace Negocio
{
    public class Business
    {
        Respuesta rta = new Respuesta();

        /// <summary>
        /// Metodo dinamico para consumir la api
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private dynamic ConsumirApi(string url)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
            myWebRequest.Credentials = CredentialCache.DefaultCredentials;
            myWebRequest.Proxy = null;
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            Stream myStream = myHttpWebResponse.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myStream);
            string Datos = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());
            dynamic data = JsonConvert.DeserializeObject(Datos);
            return data;
        }

        /// <summary>
        /// Metodo que consume la api y retorna las ciudades dependiendo del departamento
        /// </summary>
        /// <param name="departamento"></param>
        /// <param name="rta"></param>
        /// <returns></returns>
        public List<string> GetCiudadesByDepartamento(string departamento, ref Respuesta rta)
        {
            List<string> ciudades = new List<string>();
            try
            {
                dynamic informacion = ConsumirApi("https://sigma-studios.s3-us-west-2.amazonaws.com/test/colombia.json");
                Ciudades listadeCiudades = JsonConvert.DeserializeObject<Ciudades>(JsonConvert.SerializeObject(informacion));

                switch (departamento)
                {
                    case "Amazonas":
                        ciudades.AddRange(listadeCiudades.Amazonas);
                        break;
                    case "Atlántico":
                        ciudades.AddRange(listadeCiudades.Atlántico);
                        break;
                    case "Caquetá":
                        ciudades.AddRange(listadeCiudades.Caquetá);
                        break;
                    case "Cesar":
                        ciudades.AddRange(listadeCiudades.Cesar);
                        break;
                    case "Chocó":
                        ciudades.AddRange(listadeCiudades.Chocó);
                        break;
                    case "Córdoba":
                        ciudades.AddRange(listadeCiudades.Córdoba);
                        break;
                    case "Guainía":
                        ciudades.AddRange(listadeCiudades.Guainía);
                        break;
                    case "Guaviare":
                        ciudades.AddRange(listadeCiudades.Guaviare);
                        break;
                    case "Huila":
                        ciudades.AddRange(listadeCiudades.Huila);
                        break;
                    case "La Guajira":
                        ciudades.AddRange(listadeCiudades.LaGuajira);
                        break;
                    case "Putumayo":
                        ciudades.AddRange(listadeCiudades.Putumayo);
                        break;
                    case "Quindío":
                        ciudades.AddRange(listadeCiudades.Quindío);
                        break;
                    case "San Andrés y Providencia":
                        ciudades.AddRange(listadeCiudades.SanAndrésyProvidencia);
                        break;
                    case "Sucre":
                        ciudades.AddRange(listadeCiudades.Sucre);
                        break;
                    case "Tolima":
                        ciudades.AddRange(listadeCiudades.Tolima);
                        break;
                    case "Vaupés":
                        ciudades.AddRange(listadeCiudades.Vaupés);
                        break;
                    case "Vichada":
                        ciudades.AddRange(listadeCiudades.Vichada);
                        break;
                }

                rta.respuesta = true;
                rta.descripcion = "Ciudades listadas correctamente";

                return ciudades;
            }
            catch(Exception ex)
            {
                rta.respuesta = false;
                rta.descripcion = "Hubo un problema listando las ciudades: "+ ex.Message;

                return ciudades;
            }
        }

        /// <summary>
        /// Metodo statico para obtener la descripcion de los atributos de la clase numerica
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionFiltro(Departamentos value)
        {
            System.Reflection.FieldInfo oFieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])oFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Metodo void para guardar el contacto en la base de datos
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="rta"></param>
        public void GuardarContacto(contacts obj, ref Respuesta rta) 
        {
            using (admin_sigmatestEntities db = new admin_sigmatestEntities())
            {
                try
                {
                    db.contacts.Add(obj);
                    db.SaveChanges();

                    rta.respuesta = true;
                    rta.descripcion = "Tu información ha sido recibida satisfactoriamente";

                }
                catch(Exception ex)
                {
                    rta.respuesta = true;
                    rta.descripcion = "Hubo un problema guardando los datos: "+ex.Message;
                }
            }

        }
    }
}
