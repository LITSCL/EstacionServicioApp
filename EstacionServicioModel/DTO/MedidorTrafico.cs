using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionServicioModel.DTO
{
    public class MedidorTrafico : Medidor
    {
        public MedidorTrafico(int id, string fechaInstalacion)
        {
            this.Id = id;
            this.FechaInstalacion = fechaInstalacion;
        }

        public int ObtenerCantidadVehiculos()
        {
            return -1;
        }
    }
}
