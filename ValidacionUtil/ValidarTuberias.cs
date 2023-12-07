using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarTuberias
    {
        public bool Validar(string mensaje, int cantidadTuberias)
        {
            string[] mensajeArray = mensaje.Split('|');
            if (mensajeArray.Length == cantidadTuberias + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}
