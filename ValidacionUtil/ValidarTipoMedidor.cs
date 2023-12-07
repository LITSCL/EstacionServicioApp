using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarTipoMedidor
    {
        public bool Validar(string tipo)
        {
            if (tipo.ToLower() == "consumo" || tipo.ToLower() == "trafico")
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
