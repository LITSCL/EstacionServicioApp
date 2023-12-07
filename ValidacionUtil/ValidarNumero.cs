using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarNumero
    {
        public bool Validar(string numero)
        {
            try
            {
                int numeroInt = Convert.ToInt32(numero);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
