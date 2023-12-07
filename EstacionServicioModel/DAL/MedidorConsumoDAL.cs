using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EstacionServicioModel.DTO;

namespace EstacionServicioModel.DAL
{
    public class MedidorConsumoDAL : IMedidorConsumoDAL
    {
        private MedidorConsumoDAL()
        {

        }

        private static IMedidorConsumoDAL instancia;

        public static IMedidorConsumoDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorConsumoDAL();
                MedidorConsumo mc1 = new MedidorConsumo(1, "2020-12-24-10-35-30");
                MedidorConsumo mc2 = new MedidorConsumo(2, "2020-12-24-10-35-30");
                medidores.Add(mc1);
                medidores.Add(mc2);
            }
            return instancia;
        }

        private static List<MedidorConsumo> medidores = new List<MedidorConsumo>();

        public List<MedidorConsumo> ObtenerMedidores()
        {
            return medidores;
        }
    }
}
