using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarMensajeUPDATE
    {
        public bool Validar(string mensaje)
        {
            if (mensaje.ToLower() == "update")
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
