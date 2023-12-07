using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class SepararMensaje
    {
        public string[] Separar(string mensaje)
        {
            string[] mensajeArray = mensaje.Split('|');
            return mensajeArray;
        }
    }
}
