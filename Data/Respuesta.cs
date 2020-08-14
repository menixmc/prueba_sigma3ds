using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Respuesta
    {
        private bool _respuesta { get; set; }
        public bool respuesta
        {
            get { return _respuesta; }
            set { _respuesta = value; }
        }

        private string _descripcion { get; set; }
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

    }
}
