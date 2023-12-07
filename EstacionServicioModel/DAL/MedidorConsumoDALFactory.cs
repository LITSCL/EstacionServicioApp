using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionServicioModel.DAL
{
    public class MedidorConsumoDALFactory
    {
        public static IMedidorConsumoDAL CreateDal()
        {
            return MedidorConsumoDAL.GetInstancia();
        }
    }
}
