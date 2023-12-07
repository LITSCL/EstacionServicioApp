using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarEstado
    {
        public bool Validar(string estado)
        {
            switch (estado)
            {
                case "-1":
                    return true;
                    break;
                case "0":
                    return true;
                    break;
                case "1":
                    return true;
                    break;
                case "2":
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }
    }
}
