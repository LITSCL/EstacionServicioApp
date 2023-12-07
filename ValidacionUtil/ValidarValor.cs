using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacionUtil
{
    public class ValidarValor
    {
        public bool Validar(string valor)
        {
            try
            {
                int valorInt = Convert.ToInt32(valor);

                if (valorInt > -1 && valorInt < 1001)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
