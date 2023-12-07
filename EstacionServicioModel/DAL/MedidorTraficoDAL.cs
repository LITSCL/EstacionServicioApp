using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EstacionServicioModel.DTO;

namespace EstacionServicioModel.DAL
{
    public class MedidorTraficoDAL : IMedidorTraficoDAL
    {
        private MedidorTraficoDAL()
        {

        }

        private static IMedidorTraficoDAL instancia;

        public static IMedidorTraficoDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorTraficoDAL();
                MedidorTrafico mt1 = new MedidorTrafico(1, "2020-12-24-10-35-30");
                MedidorTrafico mt2 = new MedidorTrafico(2, "2020-12-24-10-35-30");
                medidores.Add(mt1);
                medidores.Add(mt2);
            }
            return instancia;
        }

        private static List<MedidorTrafico> medidores = new List<MedidorTrafico>();

        public List<MedidorTrafico> ObtenerMedidores()
        {
            return medidores;
        }
    }
}
