using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ConvertirEstado
    {
        public string Convertir(string estado)
        {
            switch (estado)
            {
                case "-1":
                    return "Error de lectura";
                    break;
                case "0":
                    return "OK";
                    break;
                case "1":
                    return "Punto de carga lleno";
                    break;
                case "2":
                    return "Requiere mantención preventiva";
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
