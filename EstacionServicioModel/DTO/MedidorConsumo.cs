using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionServicioModel.DTO
{
    public class MedidorConsumo : Medidor
    {
        public MedidorConsumo(int id, string fechaInstalacion)
        {
            this.Id = id;
            this.FechaInstalacion = fechaInstalacion;
        }

        public int LeerEstado()
        {
            return -1;
        }

        public double ObtenerKwhConsumidos()
        {
            return 5.0;
        }
    }
}
